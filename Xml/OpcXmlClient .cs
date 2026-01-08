using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XiaoFeng.OPC.XML;
using XiaoFeng.OPC.XML.Model;

/****************************************************************
*  Copyright © (2025) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2025-12-28 21:12:33                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.Xml
{
    /// <summary>
    /// OPC XML-DA 客户端类库（支持基础操作和订阅功能）
    /// </summary>
    public class OpcXmlClient : IDisposable
    {
        #region 配置参数
        private readonly string _serviceUrl;
        private readonly string _opcNamespace;
        private readonly NetworkCredential _credentials;
        private readonly bool _useCallbackMode;
        private readonly int _callbackPort;
        #endregion

        #region 内部组件
        private readonly HttpClient _httpClient;
        private readonly SubscriptionManager _subscriptionManager;
        private readonly CallbackHttpServer _callbackServer;
        //private PeriodicTimer _pollingTimer;
        private Task _pollingTask;
        private bool _isPolling;
        private bool _disposed;
        #endregion

        #region 公共事件
        /// <summary>
        /// 订阅通知接收事件（回调或轮询获取到更新时触发）
        /// </summary>
        public event EventHandler<SubscriptionNotification> SubscriptionNotificationReceived;
        #endregion

        #region 构造函数
        /// <summary>
        /// 初始化 OPC XML 客户端
        /// </summary>
        /// <param name="serviceUrl">OPC XML 服务器地址（如 http://192.168.1.100:8080/OPCXMLService）</param>
        /// <param name="opcNamespace">OPC 命名空间（默认使用标准命名空间）</param>
        /// <param name="credentials">身份验证凭据（可选）</param>
        /// <param name="useCallbackMode">是否使用回调模式（否则为轮询模式）</param>
        /// <param name="callbackPort">回调模式的本地端口（默认 8081）</param>
        public OpcXmlClient(
            string serviceUrl,
            string opcNamespace = OpcXmlHelper.Namespace,
            NetworkCredential credentials = null,
            bool useCallbackMode = false,
            int callbackPort = 8081)
        {
            _serviceUrl = serviceUrl ?? throw new ArgumentNullException(nameof(serviceUrl));
            _opcNamespace = opcNamespace ?? throw new ArgumentNullException(nameof(opcNamespace));
            _credentials = credentials;
            _useCallbackMode = useCallbackMode;
            _callbackPort = callbackPort;

            // 初始化 HTTP 客户端
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));

            // 配置身份验证
            if (_credentials != null)
            {
                var authBytes = Encoding.ASCII.GetBytes($"{_credentials.UserName}:{_credentials.Password}");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Basic", Convert.ToBase64String(authBytes));
            }

            // 初始化订阅管理器
            _subscriptionManager = new SubscriptionManager();

            // 初始化回调服务器（若启用回调模式）
            if (_useCallbackMode)
            {
                _callbackServer = new CallbackHttpServer(_callbackPort, HandleSubscriptionNotificationAsync);
            }
        }
        #endregion

        #region 基础操作
        /// <summary>
        /// 查询 OPC 服务器状态
        /// </summary>
        public async Task<GetStatusResponse> GetStatusAsync(CancellationToken cancellationToken = default)
        {
            var request = new GetStatusRequest();
            return await SendOpcRequestAsync<GetStatusRequest, GetStatusResponse>(request, "GetStatus", cancellationToken);
        }

        /// <summary>
        /// 读取 OPC 标签数据
        /// </summary>
        /// <param name="items">要读取的标签列表</param>
        public async Task<ReadResponse> ReadAsync(IEnumerable<OpcItem> items, CancellationToken cancellationToken = default)
        {
            if (items == null || !items.Any())
                throw new ArgumentException("标签列表不能为空", nameof(items));

            var request = new ReadRequest
            {
                Items = items.ToList()
            };
            return await SendOpcRequestAsync<ReadRequest, ReadResponse>(request, "Read", cancellationToken);
        }

        /// <summary>
        /// 写入 OPC 标签数据
        /// </summary>
        /// <param name="writeItems">要写入的标签（包含值）</param>
        public async Task<WriteResponse> WriteAsync(IEnumerable<OpcWriteItem> writeItems, CancellationToken cancellationToken = default)
        {
            if (writeItems == null || !writeItems.Any())
                throw new ArgumentException("写入标签列表不能为空", nameof(writeItems));

            var request = new WriteRequest
            {
                Items = writeItems.ToList()
            };
            return await SendOpcRequestAsync<WriteRequest, WriteResponse>(request, "Write", cancellationToken);
        }
        #endregion

        #region 订阅操作
        /// <summary>
        /// 创建订阅
        /// </summary>
        /// <param name="items">订阅的标签列表</param>
        /// <param name="updateRate">更新率（毫秒）</param>
        /// <param name="keepAliveTime">保持活动时间（秒）</param>
        public async Task<CreateSubscriptionResponse> CreateSubscriptionAsync(
            IEnumerable<OpcItem> items,
            int updateRate = 1000,
            int keepAliveTime = 30,
            CancellationToken cancellationToken = default)
        {
            if (items == null || !items.Any())
                throw new ArgumentException("订阅标签列表不能为空", nameof(items));

            // 若为回调模式，确保回调服务器已启动
            if (_useCallbackMode && _callbackServer != null && !_callbackServer.CallbackUrl.Contains("localhost"))
                await _callbackServer.StartAsync();

            var request = new CreateSubscriptionRequest
            {
                RequestedUpdateRate = updateRate,
                KeepAliveTime = keepAliveTime,
                Items = items.ToList(),
                CallbackURL = _useCallbackMode ? _callbackServer?.CallbackUrl ?? string.Empty : string.Empty
            };

            var response = await SendOpcRequestAsync<CreateSubscriptionRequest, CreateSubscriptionResponse>(
                request, "CreateSubscription", cancellationToken);

            // 本地维护订阅信息
            _subscriptionManager.AddSubscription(new SubscriptionInfo
            {
                SubscriptionID = response.SubscriptionID,
                UpdateRate = response.RevisedUpdateRate,
                KeepAliveTime = keepAliveTime,
                Items = items.ToList(),
                CallbackURL = request.CallbackURL,
                IsPollingMode = !_useCallbackMode
            });

            // 若为轮询模式，自动启动轮询
            if (!_useCallbackMode && !_isPolling)
                await StartPollingAsync(cancellationToken);

            return response;
        }

        /// <summary>
        /// 修改订阅（更新率、标签列表等）
        /// </summary>
        public async Task<ModifySubscriptionResponse> ModifySubscriptionAsync(
            string subscriptionId,
            IEnumerable<OpcItem> items = null,
            int? updateRate = null,
            int? keepAliveTime = null,
            CancellationToken cancellationToken = default)
        {
            var subscription = _subscriptionManager.GetSubscription(subscriptionId)
                ?? throw new OpcException($"订阅 {subscriptionId} 不存在");

            var request = new ModifySubscriptionRequest
            {
                SubscriptionID = subscriptionId,
                RequestedUpdateRate = updateRate ?? subscription.UpdateRate,
                KeepAliveTime = keepAliveTime ?? subscription.KeepAliveTime,
                Items = items?.ToList() ?? subscription.Items
            };

            var response = await SendOpcRequestAsync<ModifySubscriptionRequest, ModifySubscriptionResponse>(
                request, "ModifySubscription", cancellationToken);

            // 更新本地订阅信息
            subscription.UpdateRate = response.RevisedUpdateRate;
            subscription.KeepAliveTime = request.KeepAliveTime;
            subscription.Items = request.Items;
            _subscriptionManager.UpdateSubscription(subscription);

            return response;
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        public async Task<CancelSubscriptionResponse> CancelSubscriptionAsync(
            string subscriptionId,
            CancellationToken cancellationToken = default)
        {
            if (!_subscriptionManager.GetAllSubscriptions().Any(s => s.SubscriptionID == subscriptionId))
                throw new OpcException($"订阅 {subscriptionId} 不存在");

            var request = new CancelSubscriptionRequest
            {
                SubscriptionID = subscriptionId
            };

            var response = await SendOpcRequestAsync<CancelSubscriptionRequest, CancelSubscriptionResponse>(
                request, "CancelSubscription", cancellationToken);

            // 移除本地订阅信息
            _subscriptionManager.RemoveSubscription(subscriptionId);

            // 若没有订阅了，停止轮询
            if (!_useCallbackMode && _subscriptionManager.GetAllSubscriptions().Count == 0)
                await StopPollingAsync();

            return response;
        }

        /// <summary>
        /// 手动轮询订阅更新（轮询模式下使用）
        /// </summary>
        public async Task<SubscriptionNotification> PollSubscriptionAsync(
            string subscriptionId,
            CancellationToken cancellationToken = default)
        {
            var subscription = _subscriptionManager.GetSubscription(subscriptionId)
                ?? throw new OpcException($"订阅 {subscriptionId} 不存在");

            var request = new PolledRefreshRequest
            {
                SubscriptionID = subscriptionId,
                ReleaseItems = false
            };

            var response = await SendOpcRequestAsync<PolledRefreshRequest, PolledRefreshResponse>(
                request, "PolledRefresh", cancellationToken);

            // 更新订阅活动时间
            _subscriptionManager.RefreshSubscriptionActivity(subscriptionId);

            var notification = new SubscriptionNotification
            {
                SubscriptionID = subscriptionId,
                UpdatedItems = response.ItemResults,
                NotificationTime = DateTime.Now
            };

            // 触发通知事件
            await HandleSubscriptionNotificationAsync(notification);

            return notification;
        }
        #endregion

        #region 轮询管理
        /// <summary>
        /// 启动自动轮询（轮询模式下自动调用，也可手动启动）
        /// </summary>
        public async Task StartPollingAsync(CancellationToken cancellationToken = default)
        {
            if (_useCallbackMode)
                throw new OpcException("回调模式下不支持手动启动轮询");

            if (_isPolling) return;

            _isPolling = true;
            // 取所有订阅中最小的更新率作为轮询间隔
            //var minUpdateRate = _subscriptionManager.GetAllSubscriptions()
            //    .MinOrDefault(s => s.UpdateRate)?.UpdateRate ?? 1000;
            //_pollingTimer = new PeriodicTimer(TimeSpan.FromMilliseconds(minUpdateRate));
            _pollingTask = RunPollingLoopAsync(cancellationToken);
            await Task.CompletedTask;
        }

        /// <summary>
        /// 停止自动轮询
        /// </summary>
        public async Task StopPollingAsync()
        {
            if (!_isPolling) return;

            _isPolling = false;
            //if (_pollingTimer != null)
            //    _pollingTimer.Cancel();
            if (_pollingTask != null)
                await _pollingTask;
        }

        /// <summary>
        /// 轮询循环（内部使用）
        /// </summary>
        private async Task RunPollingLoopAsync(CancellationToken cancellationToken)
        {
            while (_isPolling && !cancellationToken.IsCancellationRequested)
            {
                try
                {
                    if (await _pollingTimer!.WaitForNextTickAsync(cancellationToken))
                    {
                        // 批量轮询所有订阅
                        var subscriptions = _subscriptionManager.GetAllSubscriptions();
                        foreach (var subscription in subscriptions)
                        {
                            await PollSubscriptionAsync(subscription.SubscriptionID, cancellationToken);
                        }

                        // 检查过期订阅并自动取消
                        var expiredIds = _subscriptionManager.GetExpiredSubscriptionIds();
                        foreach (var expiredId in expiredIds)
                        {
                            Console.WriteLine($"订阅 {expiredId} 已过期，自动取消");
                            await CancelSubscriptionAsync(expiredId, cancellationToken);
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"轮询失败：{ex.Message}");
                }
            }
        }
        #endregion

        #region 内部辅助方法
        /// <summary>
        /// 发送 OPC XML 请求并获取响应
        /// </summary>
        private async Task<TResponse> SendOpcRequestAsync<TRequest, TResponse>(
            TRequest request,
            string actionName,
            CancellationToken cancellationToken)
            where TRequest : BaseRequest
            where TResponse : BaseResponse, new()
        {
            try
            {
                // 序列化请求为 SOAP 信封
                var soapXml = SoapHelper.SerializeToSoapEnvelope(request);

                // 构造 HTTP 请求内容
                var content = new StringContent(soapXml, Encoding.UTF8, "text/xml");
                content.Headers.ContentType = new MediaTypeHeaderValue("text/xml") { CharSet = "utf-8" };
                content.Headers.Add("SOAPAction", string.Format(OpcXmlConstants.SoapActionTemplate, actionName));

                // 发送 POST 请求
                var response = await _httpClient.PostAsync(_serviceUrl, content, cancellationToken);
                response.EnsureSuccessStatusCode();

                // 读取响应内容并反序列化
                var responseXml = await response.Content.ReadAsStringAsync(cancellationToken);
                return SoapHelper.DeserializeFromSoapEnvelope<TResponse>(responseXml);
            }
            catch (HttpRequestException ex)
            {
                throw new OpcXmlException($"HTTP 请求失败：{ex.Message}", ex);
            }
            catch (OpcXmlException)
            {
                throw; // 直接抛出已包装的 OPC 错误
            }
            catch (Exception ex)
            {
                throw new OpcXmlException($"发送 OPC 请求失败（操作：{actionName}）", ex);
            }
        }

        /// <summary>
        /// 处理订阅通知（触发事件）
        /// </summary>
        private Task HandleSubscriptionNotificationAsync(SubscriptionNotification notification)
        {
            SubscriptionNotificationReceived?.Invoke(this, notification);
            return Task.CompletedTask;
        }
        #endregion

        #region 资源释放
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                // 停止轮询
                if (_isPolling)
                    StopPollingAsync().Wait();

                // 停止回调服务器
                _callbackServer?.StopAsync().Wait();
                _callbackServer?.Dispose();

                // 释放 HTTP 客户端
                _httpClient.Dispose();

                // 释放轮询资源
                _pollingTimer?.Dispose();
                _pollingTask?.Dispose();
            }

            _disposed = true;
        }

        ~OpcXmlClient() => Dispose(false);
        #endregion
    }
}