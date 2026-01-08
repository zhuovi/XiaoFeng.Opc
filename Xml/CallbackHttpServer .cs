using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using XiaoFeng.OPC.XML.Model;

/****************************************************************
*  Copyright © (2025) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2025-12-28 21:10:43                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.Xml
{
    /// <summary>
    /// CallbackHttpServer 类说明
    /// </summary>
    internal class CallbackHttpServer : IDisposable
    {
        private readonly HttpListener _httpListener;
        private readonly string _callbackUrl;
        private readonly Func<SubscriptionNotification, Task> _notificationHandler;
        private Task _listenTask;
        private bool _isRunning;
        private bool _disposed;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="callbackPort">回调端口（如 8081）</param>
        /// <param name="notificationHandler">通知处理委托</param>
        public CallbackHttpServer(int callbackPort, Func<SubscriptionNotification, Task> notificationHandler)
        {
            _notificationHandler = notificationHandler ?? throw new ArgumentNullException(nameof(notificationHandler));
            _callbackUrl = $"http://localhost:{callbackPort}/OpcXml/Callback";

            // 初始化 HTTP 监听器
            _httpListener = new HttpListener();
            _httpListener.Prefixes.Add(_callbackUrl + "/"); // 注意末尾的 /
        }

        /// <summary>
        /// 回调 URL（提供给 OPC 服务器）
        /// </summary>
        public string CallbackUrl => _callbackUrl;

        /// <summary>
        /// 启动 HTTP 服务器
        /// </summary>
        public async Task StartAsync()
        {
            if (_isRunning) return;

            try
            {
                _httpListener.Start();
                _isRunning = true;
                _listenTask = ListenForRequestsAsync();
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new OpcException("启动回调服务器失败", ex);
            }
        }

        /// <summary>
        /// 停止 HTTP 服务器
        /// </summary>
        public async Task StopAsync()
        {
            if (!_isRunning) return;

            _isRunning = false;
            _httpListener.Stop();
            if (_listenTask != null)
                await _listenTask;
            _httpListener.Close();
        }

        /// <summary>
        /// 监听并处理 OPC 服务器的回调请求
        /// </summary>
        private async Task ListenForRequestsAsync()
        {
            while (_isRunning)
            {
                try
                {
                    var context = await _httpListener.GetContextAsync().ConfigureAwait(false);
                    _ = ProcessRequestAsync(context); // 异步处理，不阻塞监听
                }
                catch (HttpListenerException ex) when (ex.ErrorCode == 995) // 正常关闭时的异常
                {
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"回调服务器错误：{ex.Message}");
                }
            }
        }

        /// <summary>
        /// 处理单个回调请求
        /// </summary>
        private async Task ProcessRequestAsync(HttpListenerContext context)
        {
            var response = context.Response;
            try
            {
                if (context.Request.HttpMethod != "POST")
                {
                    response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                    return;
                }

                // 读取请求体（SOAP 格式的通知数据）
                using (var reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
                {
                    var soapXml = await reader.ReadToEndAsync();

                    // 解析为 PolledRefreshResponse（回调通知格式与轮询响应一致）
                    var refreshResponse = SoapHelper.DeserializeFromSoapEnvelope<PolledRefreshResponse>(soapXml);

                    // 构造通知数据并触发处理
                    var notification = new SubscriptionNotification
                    {
                        SubscriptionID = refreshResponse.SubscriptionID,
                        UpdatedItems = refreshResponse.ItemResults,
                        NotificationTime = DateTime.Now
                    };
                    await _notificationHandler(notification);

                    // 响应服务器（200 OK）
                    response.StatusCode = (int)HttpStatusCode.OK;
                    var responseBytes = Encoding.UTF8.GetBytes("Notification received");
                    response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var errorBytes = Encoding.UTF8.GetBytes($"Error: {ex.Message}");
                response.OutputStream.Write(errorBytes, 0, errorBytes.Length);
                Console.WriteLine($"处理回调请求失败：{ex.Message}");
            }
            finally
            {
                response.OutputStream.Close();
            }
        }

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
                _httpListener.TryDispose();
                _listenTask?.Dispose();
            }

            _disposed = true;
        }

        ~CallbackHttpServer() => Dispose(false);
    }
}