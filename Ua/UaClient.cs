using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Opc.Ua;
using Opc.Ua.Bindings;
using Opc.Ua.Client;
using Opc.Ua.Security.Certificates;
using XiaoFeng.IO;
using XiaoFeng.Opc.Model;
using XiaoFeng.Threading;

/****************************************************************
*  Copyright © (2025) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2025-06-04 14:45:06                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.Opc
{
    /// <summary>
    /// Ua客户端
    /// </summary>
    [DisplayName("OpcUa客户端")]
    public class UaClient : Disposable
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public UaClient() { }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="configuration">应用配置</param>
        public UaClient(ApplicationConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 应用配置
        /// </summary>
        private ApplicationConfiguration _Configuration;
        /// <summary>
        /// 应用配置
        /// </summary>
        public ApplicationConfiguration Configuration
        {
            get => _Configuration;
            set
            {
                _Configuration = value;
                if (value.ApplicationName.IsNotNullOrEmpty()) _ApplicationName = value.ApplicationName;
            }
        }
        /// <summary>
        /// 服务器地址
        /// </summary>
        private string ServerAddress { get; set; }
        /// <summary>
        /// 应用名称
        /// </summary>
        private string _ApplicationName = "ELFOpcUaClient";
        /// <summary>
        /// 应用名称
        /// </summary>
        public string ApplicationName
        {
            get
            {
                if (this.Configuration != null)
                {
                    return Configuration.ApplicationName.Multivariate(this._ApplicationName);
                }
                return this._ApplicationName;
            }
            set
            {
                if (Configuration != null)
                {
                    Configuration.ApplicationName = value;
                }
                this._ApplicationName = value;
            }
        }
        /// <summary>
        /// 会话
        /// </summary>
        private ISession SessionClient { get; set; }
        /// <summary>
        /// 重连时长 单位为毫秒
        /// </summary>
        public int ReconnectionPeriod { get; set; } = 10000;
        /// <summary>
        /// 用户标识
        /// </summary>
        public UserIdentity UserIdentity { get; set; } = new UserIdentity();
        /// <summary>
        /// 连接状态
        /// </summary>
        public Boolean IsConnected => this.SessionClient != null ? this.SessionClient.Connected : false;
        /// <summary>
        /// 证书路径
        /// </summary>
        private string CertificatesRootPath => "Certificates";
        /// <summary>
        /// 节点配置
        /// </summary>
        private ConfiguredEndpoint Endpoint { get; set; }
        /// <summary>
        /// 取消令牌
        /// </summary>
        private CancellationTokenSource CancellationTokenSource { get; set; } = new CancellationTokenSource();
        /// <summary>
        /// 异步锁
        /// </summary>
        private readonly AsyncLock _lock = new AsyncLock();
        /// <summary>
        /// 重连事件
        /// </summary>
        private SessionReconnectHandler ReconnectHandler { get; set; }
        /// <summary>
        /// 订阅节点数据
        /// </summary>
        private ConcurrentDictionary<string, SubscriptionData> Subscriptions { get; set; } = new ConcurrentDictionary<string, SubscriptionData>();
        /// <summary>
        /// 自动接受不可信的服务器证书
        /// </summary>
        public bool AutoAcceptUntrustedCertificates { get; set; } = true;
        /// <summary>
        /// 会话超时时长 单位为毫秒
        /// </summary>
        public uint SessionTimeout { get; set; } = 60000;
        /// <summary>
        /// 会话名称
        /// </summary>
        private string _SessionName = string.Empty;
        /// <summary>
        /// 会话名称
        /// </summary>
        public string SessionName
        {
            get
            {
                if (this._SessionName.IsNullOrEmpty())
                    this._SessionName = this.ApplicationName;
                return this._SessionName;
            }
            set
            {
                this._SessionName = value;
            }
        }
        /// <summary>
        /// 保持连接
        /// </summary>
        public bool KeepAlive { get; set; } = true;
        #endregion

        #region 事件
        /// <summary>
        /// 连接事件
        /// </summary>
        public event ConnectEventHandler ConnectEvent;
        #endregion

        #region 方法

        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        public async Task InitializeAsync()
        {
            if (this.Configuration == null)
            {
                var path = CertificatesRootPath.GetBasePath();
                FileHelper.DeleteDirectory(path);
                var certificates = FileHelper.CreateDirectory(path);
                Enum.GetNames(typeof(CertificatePath)).Each(a =>
                {
                    certificates.CreateSubdirectory(a);
                });
                var hostName = Dns.GetHostName();

                var certificateValidator = new CertificateValidator();
                certificateValidator.CertificateValidation += (sender, e) =>
                {
                    if (ServiceResult.IsGood(e.Error))
                        e.Accept = true;
                    else if (e.Error.StatusCode.Code == StatusCodes.BadCertificateUntrusted)
                        e.Accept = true;
                    else
                        throw new OpcException(String.Format("Failed to validate certificate with error code {0}: {1}", e.Error.Code, e.Error.AdditionalInfo));
                };

                this.Configuration = new ApplicationConfiguration
                {
                    ApplicationName = this.ApplicationName,
                    ApplicationType = ApplicationType.Client,
                    ApplicationUri = $"urn:{this.ApplicationName}",
                    ProductUri = this.ApplicationName,
                    DisableHiResClock = true,
                    CertificateValidator = certificateValidator,
                    TransportQuotas = new TransportQuotas
                    {
                        OperationTimeout = 6000000,
                        MaxStringLength = int.MaxValue,
                        MaxByteStringLength = int.MaxValue,
                        MaxArrayLength = 65535,
                        MaxMessageSize = 419430400,
                        MaxBufferSize = 65535,
                        ChannelLifetime = -1,
                        SecurityTokenLifetime = TcpMessageLimits.DefaultSecurityTokenLifeTime
                    },
                    TransportConfigurations = new TransportConfigurationCollection(),
                    ClientConfiguration = new ClientConfiguration
                    {
                        DefaultSessionTimeout = -1,
                        MinSubscriptionLifetime = -1
                    },
                    TraceConfiguration = new TraceConfiguration
                    {
                        DeleteOnLoad = true
                    },
                    ServerConfiguration = new ServerConfiguration
                    {
                        MaxSubscriptionCount = 100000,
                        MaxMessageQueueSize = 1000000,
                        MaxNotificationQueueSize = 1000000,
                        MaxPublishRequestCount = 10000000,
                    },
                    SecurityConfiguration = new SecurityConfiguration
                    {
                        AutoAcceptUntrustedCertificates = AutoAcceptUntrustedCertificates,
                        RejectSHA1SignedCertificates = false,
                        MinimumCertificateKeySize = 1024,
                        SuppressNonceValidationErrors = true,

                        ApplicationCertificate = new CertificateIdentifier
                        {
                            StoreType = CertificateStoreType.Directory,
                            StorePath = $"{CertificatesRootPath}/{CertificatePath.Application}".GetBasePath(),
                            SubjectName = $"CN={this.ApplicationName}, DC={hostName}"
                        },
                        TrustedIssuerCertificates = new CertificateTrustList
                        {
                            StoreType = CertificateStoreType.Directory,
                            StorePath = $"{CertificatesRootPath}/{CertificatePath.TrustedIssuer}".GetBasePath()
                        },
                        TrustedPeerCertificates = new CertificateTrustList
                        {
                            StoreType = CertificateStoreType.Directory,
                            StorePath = $"{CertificatesRootPath}/{CertificatePath.TrustedPeer}".GetBasePath()
                        },
                        RejectedCertificateStore = new CertificateStoreIdentifier
                        {
                            StoreType = CertificateStoreType.Directory,
                            StorePath = $"{CertificatesRootPath}/{CertificatePath.Rejected}".GetBasePath()
                        }
                    }
                };

            }
            //创建证书
            this.CreateApplicationCertificate(12);

            await this.Configuration.Validate(ApplicationType.Client).ConfigureAwait(false);
        }
        #endregion

        #region 连接服务器
        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>返回任务</returns>
        public async Task ConnectAsync(string serverUrl, CancellationToken cancellationToken = default)
        {
            if (serverUrl.IsNullOrEmpty()) throw new OpcException("请输入OPC服务器地址.");

            this.ServerAddress = serverUrl;

            this.SessionClient = await CreateSessionAsync(cancellationToken).ConfigureAwait(false);
        }
        #endregion

        #region 生成客户端证书
        /// <summary>
        /// 生成客户端证书
        /// </summary>
        /// <param name="months">有效期</param>
        private void CreateApplicationCertificate(ushort months)
        {
            var SecurityConfiguration = this.Configuration.SecurityConfiguration;

            var builder = CertificateBuilder.Create(SecurityConfiguration.ApplicationCertificate.SubjectName);
            builder.SetNotBefore(DateTime.Now);
            builder.SetNotAfter(DateTime.Now.AddMonths(months));
            builder.SetRSAKeySize(SecurityConfiguration.MinimumCertificateKeySize);

            var cert = builder.CreateForRSA();

            var data = cert.Export(X509ContentType.Pfx);
            File.WriteAllBytes(SecurityConfiguration.ApplicationCertificate.StorePath.Combine($"{this.ApplicationName}.pfx"), data);
        }
        #endregion

        #region 创建会话
        /// <summary>
        /// 创建会话
        /// </summary>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>返回会话</returns>
        /// <exception cref="OpcException">异常</exception>
        public async Task<ISession> CreateSessionAsync(CancellationToken cancellationToken = default)
        {
            await InitializeAsync().ConfigureAwait(false);
            await this.DisconnectAsync(cancellationToken).ConfigureAwait(false);

            var token = this.CreateLinkedTokenSource(cancellationToken);

            var endpointDescription = CoreClientUtils.SelectEndpoint(this.Configuration, this.ServerAddress, false);

            var endpointConfig = EndpointConfiguration.Create(this.Configuration);

            this.Endpoint = new ConfiguredEndpoint(null, endpointDescription, endpointConfig);

            var session = await Session.Create(this.Configuration, this.Endpoint, false, false, this.SessionName, this.SessionTimeout, this.UserIdentity, new string[] { }, token.Token).ConfigureAwait(false);

            if (this.KeepAlive)
                session.KeepAlive += this.KeepAliveEventHandler;

            if (session == null || !session.Connected)
            {
                this.ConnectEvent?.Invoke(new OpcConnectEventArgs("创建OPC会话失败."));
            }
            else
            {
                this.ConnectEvent?.Invoke(new OpcConnectEventArgs(session));
            }

            return session;
        }
        #endregion

        #region 保持激活连接
        /// <summary>
        /// 保持激活连接
        /// </summary>
        /// <param name="session">会话</param>
        /// <param name="e">事件</param>
        public void KeepAliveEventHandler(ISession session, KeepAliveEventArgs e)
        {
            try
            {
                if (!Object.ReferenceEquals(session, this.SessionClient))
                {
                    return;
                }
                if (ServiceResult.IsBad(e.Status))
                {
                    if (this.ReconnectionPeriod <= 0) return;

                    if (this.ReconnectHandler == null)
                    {
                        this.ReconnectHandler = new SessionReconnectHandler(true);

                        this.ReconnectHandler.BeginReconnect(this.SessionClient, this.ReconnectionPeriod, ReconnectComplete);

                    }
                    return;
                }
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region 重连完成
        /// <summary>
        /// 重连完成
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">事件</param>
        private void ReconnectComplete(object sender, EventArgs e)
        {
            try
            {
                if (!ReferenceEquals(sender, this.ReconnectHandler)) return;

                if (this.ReconnectHandler.Session != null)
                    this.SessionClient = this.ReconnectHandler.Session;

                this.ReconnectHandler.Dispose();
                this.ReconnectHandler = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 重新连接
        /// <summary>
        /// 重新连接
        /// </summary>
        /// <param name="cancellationToken">取消标识令牌</param>
        /// <returns>返回任务</returns>
        public async Task ReconnectAsync(CancellationToken cancellationToken = default)
        {
            await this.SessionClient.ReconnectAsync(this.CreateLinkedTokenSource(cancellationToken).Token).ConfigureAwait(false);
        }
        #endregion

        #region 断开连接
        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>返回任务</returns>
        public async Task DisconnectAsync(CancellationToken cancellationToken = default)
        {
            if (!this.IsConnected) return;

            this.CancellationTokenSource.Cancel();

            if (this.SessionClient.Subscriptions != null && this.SessionClient.SubscriptionCount == 0)
                this.SessionClient.Subscriptions.Each(async s =>
                {
                    await s.DeleteAsync(true, cancellationToken).ConfigureAwait(false);
                });

            this.Subscriptions.Clear();

            await this.SessionClient.CloseAsync(cancellationToken).ConfigureAwait(false);
            if (this.SessionClient.Disposed) return;
            this.SessionClient.Dispose();
            this.SessionClient = null;
        }
        #endregion

        #region 获取节点树
        /// <summary>
        /// 获取节点树
        /// </summary>
        /// <param name="valueId">节点ID</param>
        /// <param name="recursive">是否递归</param>
        /// <param name="token">取消令牌</param>
        /// <returns>返回树节点集合</returns>
        public async Task<List<NodeValue>> GetNodeTreeAsync(NodeValueId valueId, bool recursive = false, CancellationToken token = default)
        {
            if (valueId == null) return new List<NodeValue>();

            var browser = new Browser()
            {
                Session = this.SessionClient,
                BrowseDirection = BrowseDirection.Forward,
                //NodeClassMask = (int)NodeClass.Object | (int)NodeClass.Variable |(int)NodeClass.Method |(int)NodeClass.View,
                ReferenceTypeId = ReferenceTypeIds.HierarchicalReferences
            };
            var result = browser.Browse((NodeId)valueId);
            if (result == null || result.Count == 0) return new List<NodeValue>();

            var list = new List<NodeValue>();
            result.Each(async r =>
            {
                //if (r.NodeClass == NodeClass.ObjectType) return;
                if (list.Exists(a => (NodeId)a.NodeId == r.NodeId)) return;
                var node = new NodeValue
                {
                    BrowseName = r.BrowseName.Name,
                    DisplayName = r.DisplayName.Text,
                    NodeClass = r.NodeClass,

                    NodeId = new NodeValueId()
                    {
                        Identifier = r.NodeId.Identifier,
                        IdentifierType = r.NodeId.IdType,
                        NamespaceIndex = r.NodeId.NamespaceIndex,
                    }
                };
                list.Add(node);
                if (recursive && node.NodeClass != NodeClass.Variable)
                {
                    node.ChildNodes = await this.GetNodeTreeAsync(node.NodeId, recursive, token).ConfigureAwait(false);
                }
            });
            return await Task.FromResult(list);
        }
        #endregion

        #region 读取节点值
        /// <summary>
        /// 读取节点值
        /// </summary>
        /// <param name="valueId">节点ID</param>
        /// <param name="cancellationToken">取消标识令牌</param>
        /// <returns>返回读取节点值</returns>
        public async Task<DataValue> ReadDataValueAsync(NodeValueId valueId, CancellationToken cancellationToken = default)
        {
            return await this.SessionClient.ReadValueAsync((NodeId)valueId, this.CreateLinkedTokenSource(cancellationToken).Token).ConfigureAwait(false);
        }
        /// <summary>
        /// 读取节点值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="nodeValueId">节点ID</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="cancellationToken">取消标识令牌</param>
        /// <returns>返回读取节点值</returns>
        public async Task<T> ReadValueAsync<T>(NodeValueId nodeValueId, T defaultValue, CancellationToken cancellationToken = default)
        {
            return (await this.ReadDataValueAsync(nodeValueId, cancellationToken).ConfigureAwait(false)).GetValue<T>(defaultValue);
        }
        /// <summary>
        /// 读取节点值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="nodeValueId">节点ID</param>
        /// <param name="cancellationToken">取消标识令牌</param>
        /// <returns>返回读取节点值</returns>
        public async Task<T> ReadValueAsync<T>(NodeValueId nodeValueId, CancellationToken cancellationToken = default)
        {
            return (await this.ReadDataValueAsync(nodeValueId, cancellationToken).ConfigureAwait(false)).GetValueOrDefault<T>();
        }
        #endregion

        #region 获取节点
        /// <summary>
        /// 获取节点
        /// </summary>
        /// <param name="valueId">值</param>
        /// <param name="cancellationToken">取消标识令牌</param>
        /// <returns>返回读取节点</returns>
        public async Task<Node> ReadNodeAsync(NodeValueId valueId, CancellationToken cancellationToken = default)
        {
            return await this.SessionClient.ReadNodeAsync((NodeId)valueId, this.CreateLinkedTokenSource(cancellationToken).Token).ConfigureAwait(false);
        }
        #endregion

        #region 读取数据
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="namespaceIndex">命名空间索引</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>返回读取的数据</returns>
        public async Task<DataValue> ReadAsync(string address, ushort namespaceIndex, CancellationToken cancellationToken = default)
        {
            var list = await this.ReadAsync(new List<NodeValueId> { new NodeValueId(address, namespaceIndex) }, cancellationToken).ConfigureAwait(false);
            if (list == null || list.Count == 0)
            {
                return await Task.FromResult(new DataValue(StatusCodes.BadNoData));
            }
            return await Task.FromResult(list.FirstOrDefault());
        }
        /// <summary>
        /// 读取值数据
        /// </summary>
        /// <param name="address">地址集合</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>返回读取值数据集</returns>
        public async Task<List<DataValue>> ReadAsync(List<string> address, CancellationToken cancellationToken)
        {
            if (address == null || address.Count == 0) return await Task.FromResult(new List<DataValue>());
            var collection = new ReadValueIdCollection();
            address.Each(a =>
            {
                collection.Add(new ReadValueId
                {
                    AttributeId = Attributes.Value,
                    NodeId = new NodeId(a),
                });
            });
            return await this.ReadAsync(collection, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// 读取值数据
        /// </summary>
        /// <param name="valueId">节点ID</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>返回读取值数据集</returns>
        public async Task<List<DataValue>> ReadAsync(NodeValueId valueId, CancellationToken cancellationToken = default)
        {
            return await this.ReadAsync(new List<NodeValueId> { valueId }, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// 读取值数据
        /// </summary>
        /// <param name="nodeIds">节点集合</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>返回读取值数据集</returns>
        public async Task<List<DataValue>> ReadAsync(List<NodeValueId> nodeIds, CancellationToken cancellationToken = default)
        {
            if (nodeIds == null || nodeIds.Count == 0) return await Task.FromResult(new List<DataValue>());

            var collection = new ReadValueIdCollection();
            nodeIds.Each(node =>
            {
                collection.Add(new ReadValueId
                {
                    AttributeId = Attributes.Value,
                    NodeId = (NodeId)node
                });
            });
            return await ReadAsync(collection, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// 读取值数据
        /// </summary>
        /// <param name="collection">地址集合</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>返回读取值数据集</returns>
        public async Task<List<DataValue>> ReadAsync(ReadValueIdCollection collection, CancellationToken cancellationToken = default)
        {
            if (collection == null || collection.Count == 0) return await Task.FromResult(new List<DataValue>());

            var response = await this.SessionClient.ReadAsync(null, 0, TimestampsToReturn.Both, collection, this.CreateLinkedTokenSource(cancellationToken).Token).ConfigureAwait(false);

            ClientBase.ValidateResponse(response.Results, collection);
            ClientBase.ValidateDiagnosticInfos(response.DiagnosticInfos, collection);

            if (response.Results == null || response.Results.Count == 0)
            {
                return await Task.FromResult(new List<DataValue> { new DataValue(StatusCodes.BadNoData) });
            }
            return await Task.FromResult(response.Results);
        }

        #endregion

        #region 写入节点值
        /// <summary>
        /// 写入节点值
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <param name="namespaceIndex">空间索引</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>返回写入节点值及写入状态</returns>
        public async Task<WriteNode> WriteAsync(string address, object value, ushort namespaceIndex = 2, CancellationToken cancellationToken = default)
        {
            return await this.WriteAsync(new WriteNode(address, namespaceIndex, value)).ConfigureAwait(false);
        }
        /// <summary>
        /// 写入节点值
        /// </summary>
        /// <param name="writeNode">节点值</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>返回写入节点值及写入状态</returns>
        public async Task<WriteNode> WriteAsync(WriteNode writeNode, CancellationToken cancellationToken = default)
        {
            var writeValues = await this.WriteAsync(new List<WriteNode> { writeNode }, cancellationToken).ConfigureAwait(false);
            return writeValues.FirstOrDefault();
        }
        /// <summary>
        /// 写入节点值
        /// </summary>
        /// <param name="writeNodes">节点值集合</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>返回写入节点值及写入状态</returns>
        public async Task<List<WriteNode>> WriteAsync(List<WriteNode> writeNodes, CancellationToken cancellationToken = default)
        {
            if (writeNodes == null || writeNodes.Count == 0) return new List<WriteNode>();

            var values = new WriteValueCollection();
            values.AddRange(writeNodes.Select(a => new WriteValue
            {
                AttributeId = Attributes.Value,
                NodeId = (NodeId)a.NodeValueId,
                Value = new DataValue()
                {
                    Value = a.Value,
                }
            }));
            var response = await this.SessionClient.WriteAsync(null, values, this.CreateLinkedTokenSource(cancellationToken).Token).ConfigureAwait(false);

            for (var i = 0; i < response.Results.Count; i++)
            {
                writeNodes[i].StatusCode = response.Results[i].Code;
            }
            return await Task.FromResult(writeNodes);
        }
        #endregion

        #region 读取历史数据
        /// <summary>
        /// 读取历史数据
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="count">数量</param>
        /// <param name="containBound">是否包含边界</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>返回历史记录数据集</returns>
        public async Task<List<DataValue>> HistoryReadAsync(string address, DateTime? start, DateTime? end, uint count = 1, bool containBound = false, CancellationToken cancellationToken = default)
        {
            return await this.HistoryReadAsync(new NodeValueId(address), start, end, count, containBound, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// 读取历史数据
        /// </summary>
        /// <param name="valueId">节点Id</param>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="count">数量</param>
        /// <param name="containBound">是否包含边界</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>返回历史记录数据集</returns>
        public async Task<List<DataValue>> HistoryReadAsync(NodeValueId valueId, DateTime? start = null, DateTime? end = null, uint count = 1, bool containBound = false, CancellationToken cancellationToken = default)
        {
            var readValues = new HistoryReadValueIdCollection
            {
                new HistoryReadValueId()
                {
                    NodeId = (NodeId)valueId
                }
            };

            var details = new ReadRawModifiedDetails
            {
                NumValuesPerNode = count,
                IsReadModified = false,
                ReturnBounds = containBound
            };
            if (start.HasValue)
                details.StartTime = start.Value;
            if (end.HasValue)
                details.EndTime = end.Value;

            var response = await this.SessionClient.HistoryReadAsync(null, new ExtensionObject(details), TimestampsToReturn.Both, false, readValues, this.CreateLinkedTokenSource(cancellationToken).Token).ConfigureAwait(false);
            ClientBase.ValidateResponse(response.Results, readValues);
            ClientBase.ValidateDiagnosticInfos(response.DiagnosticInfos, readValues);
            if (response.Results == null || response.Results.Count == 0 || StatusCode.IsBad(response.Results[0].StatusCode))
            {
                return await Task.FromResult(new List<DataValue> { new DataValue(StatusCodes.BadNoData) });
            }
            var values = ExtensionObject.ToEncodeable(response.Results[0].HistoryData) as HistoryData;
            return values.DataValues;
        }
        #endregion

        #region 删除存在的节点
        /// <summary>
        /// 删除存在的节点
        /// </summary>
        /// <param name="valueId">节点</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>返回删除已存在的节点及删除状态</returns>
        public async Task<DeleteNode> DeleteExsistNodeAsync(NodeValueId valueId, CancellationToken cancellationToken = default)
        {
            return (await this.DeleteExsistNodeAsync(new List<NodeValueId> { valueId }, cancellationToken).ConfigureAwait(false)).FirstOrDefault();
        }
        /// <summary>
        /// 删除存在的节点
        /// </summary>
        /// <param name="addresss">地址集合</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>返回删除已存在的节点及删除状态</returns>
        public async Task<List<DeleteNode>> DeleteExsistNodeAsync(List<string> addresss, CancellationToken cancellationToken = default)
        {
            if (addresss == null || addresss.Count == 0) return new List<DeleteNode>();
            return await this.DeleteExsistNodeAsync(addresss.Select(a => new NodeValueId(a)).ToList(), cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// 删除存在的节点
        /// </summary>
        /// <param name="nodeIds">节点集合</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>返回删除已存在的节点及删除状态</returns>
        public async Task<List<DeleteNode>> DeleteExsistNodeAsync(List<NodeValueId> nodeIds, CancellationToken cancellationToken = default)
        {
            if (nodeIds == null || nodeIds.Count == 0) return new List<DeleteNode>();
            var deletes = new DeleteNodesItemCollection();
            deletes.AddRange(nodeIds.Select(a => new DeleteNodesItem
            {
                NodeId = (NodeId)a
            }));

            var response = await this.SessionClient.DeleteNodesAsync(null, deletes, this.CreateLinkedTokenSource(cancellationToken).Token).ConfigureAwait(false);

            ClientBase.ValidateResponse(response.Results, deletes);
            ClientBase.ValidateDiagnosticInfos(response.DiagnosticInfos, deletes);

            var values = new List<DeleteNode>();
            for (var i = 0; i < response.Results.Count; i++)
            {
                values.Add(new DeleteNode
                {
                    NodeValueId = nodeIds[i],
                    StatusCode = response.Results[0]
                });
            }
            return values;
        }
        #endregion

        #region 添加新节点
        /// <summary>
        /// 添加新节点
        /// </summary>
        /// <param name="item">新节点</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>返回添加新节点后的结果</returns>
        public async Task<AddNodesResult> AddNewNodeAsync(AddNodesItem item, CancellationToken cancellationToken = default)
        {
            return (await this.AddNewNodeAsync(new AddNodesItemCollection() { item }, cancellationToken).ConfigureAwait(false)).FirstOrDefault();
        }
        /// <summary>
        /// 添加新节点
        /// </summary>
        /// <param name="addNodesItems">新节点集合</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>返回添加新节点后的结果集合</returns>
        public async Task<AddNodesResultCollection> AddNewNodeAsync(AddNodesItemCollection addNodesItems, CancellationToken cancellationToken = default)
        {
            if (addNodesItems == null || addNodesItems.Count == 0) new AddNodesResultCollection();

            var response = await this.SessionClient.AddNodesAsync(null, addNodesItems, this.CreateLinkedTokenSource(cancellationToken).Token).ConfigureAwait(false);

            ClientBase.ValidateResponse(response.Results, addNodesItems);
            ClientBase.ValidateDiagnosticInfos(response.DiagnosticInfos, addNodesItems);

            return response.Results;
        }
        #endregion

        #region 调用服务器方法
        /// <summary>
        /// 调用服务器方法
        /// </summary>
        /// <param name="parentNodeValueId">父节点</param>
        /// <param name="nodeValueId">方法节点</param>
        /// <param name="variants">传递的参数集合</param>
        /// <param name="cancellationToken"></param>
        /// <returns>返回调用方法后的结果集合</returns>
        public async Task<CallMethodResult> CallMethodAsync(NodeValueId parentNodeValueId, NodeValueId nodeValueId, VariantCollection variants, CancellationToken cancellationToken = default)
        {
            var methods = new CallMethodRequestCollection()
            {
                new CallMethodRequest
                {
                     ObjectId=(NodeId)parentNodeValueId,
                     MethodId=(NodeId)nodeValueId,
                     InputArguments=variants
                }
            };
            return (await this.CallMethodAsync(methods, cancellationToken).ConfigureAwait(false)).FirstOrDefault();
        }
        /// <summary>
        /// 调用服务器方法
        /// </summary>
        /// <param name="callMethods">调用方法集合</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>返回调用方法后的结果集合</returns>
        public async Task<CallMethodResultCollection> CallMethodAsync(CallMethodRequestCollection callMethods, CancellationToken cancellationToken = default)
        {
            var response = await this.SessionClient.CallAsync(null, callMethods, this.CreateLinkedTokenSource(cancellationToken).Token).ConfigureAwait(false);

            return response.Results;
        }
        #endregion

        #region 读取节点所有属性
        /// <summary>
        /// 读取节点所有属性
        /// </summary>
        /// <param name="nodeValueId">节点</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>返回节点属性集合</returns>
        [Obsolete("暂未读取属性,日后再作测试.")]
        public async Task<ReferenceDescriptionCollection> ReadNodeAttributesAsync(NodeValueId nodeValueId, CancellationToken cancellationToken = default)
        {
            var nodeToBrowse = new BrowseDescription
            {
                NodeId = (NodeId)nodeValueId,
                BrowseDirection = BrowseDirection.Forward,
                //ReferenceTypeId= ReferenceTypeIds.HasProperty,
                IncludeSubtypes = true,
                //NodeClassMask=(int)NodeClass.Variable,
                ResultMask = (uint)BrowseResultMask.All
            };

            var nodesToBrowse = new BrowseDescriptionCollection { nodeToBrowse };

            var response = await this.SessionClient.BrowseAsync(null, null, 100, nodesToBrowse, this.CreateLinkedTokenSource(cancellationToken).Token).ConfigureAwait(false);

            ClientBase.ValidateResponse(response.Results, nodesToBrowse);
            ClientBase.ValidateDiagnosticInfos(response.DiagnosticInfos, nodesToBrowse);

            var properties = new ReferenceDescriptionCollection();
            if (response.Results == null || response.Results.Count == 0)
                return properties;
            foreach (var node in response.Results)
            {
                properties.AddRange(node.References);
            }

            return properties;
        }
        #endregion

        #region 添加订阅
        /// <summary>
        /// 添加订阅
        /// </summary>
        /// <param name="subscriptionName">订阅名称</param>
        /// <param name="address">节点地址</param>
        /// <param name="monitor">节点变化回调</param>
        /// <param name="period">发布间隔 单位毫秒</param>
        /// <returns>返回添加订阅状态 <see langword="true"/> 为成功 <see langword="false"/> 为失败</returns>
        public async Task<bool> AddSubscriptionAsync(string subscriptionName, string address, MonitoredItemNotificationEventHandler monitor, uint period = 0)
        {
            return await this.AddSubscriptionAsync(subscriptionName, new NodeValueId(address), monitor, period).ConfigureAwait(false);
        }
        /// <summary>
        /// 添加订阅
        /// </summary>
        /// <param name="subscriptionName">订阅名称</param>
        /// <param name="nodeId">节点</param>
        /// <param name="monitor">节点变化回调</param>
        /// <param name="period">发布间隔 单位毫秒</param>
        /// <returns>返回添加订阅状态 <see langword="true"/> 为成功 <see langword="false"/> 为失败</returns>
        public async Task<bool> AddSubscriptionAsync(string subscriptionName, NodeValueId nodeId, MonitoredItemNotificationEventHandler monitor, uint period = 0)
        {
            return await this.AddSubscriptionAsync(subscriptionName, new NodeIdCollection
            {
                (NodeId)nodeId
            }, monitor, period).ConfigureAwait(false);
        }
        /// <summary>
        /// 添加订阅
        /// </summary>
        /// <param name="subscriptionName">订阅名称</param>
        /// <param name="nodeIds">节点集合</param>
        /// <param name="monitor">节点变化回调</param>
        /// <param name="period">发布间隔 单位毫秒</param>
        /// <returns></returns>
        public async Task<bool> AddSubscriptionAsync(string subscriptionName, List<NodeValueId> nodeIds, MonitoredItemNotificationEventHandler monitor, uint period = 0)
        {
            if (nodeIds == null || nodeIds.Count == 0) return false;
            var values = new NodeIdCollection();
            values.AddRange(nodeIds.Select(a => (NodeId)a));
            return await this.AddSubscriptionAsync(subscriptionName, values, monitor, period).ConfigureAwait(false);
        }
        /// <summary>
        /// 添加订阅
        /// </summary>
        /// <param name="subscriptionName">订阅名称</param>
        /// <param name="nodeIds">节点集合</param>
        /// <param name="monitor">节点变化回调</param>
        /// <param name="period">发布间隔 单位毫秒</param>
        /// <returns></returns>
        public async Task<bool> AddSubscriptionAsync(string subscriptionName, NodeIdCollection nodeIds, MonitoredItemNotificationEventHandler monitor, uint period = 0)
        {
            var subscription = new Subscription(this.SessionClient.DefaultSubscription)
            {
                PublishingEnabled = true,
                PublishingInterval = (int)period,
                KeepAliveCount = ushort.MaxValue,
                LifetimeCount = 2400,
                MaxNotificationsPerPublish = ushort.MaxValue,
                Priority = 100,
                DisplayName = subscriptionName
            };
            nodeIds.Each(n =>
            {
                var monitoredItem = new MonitoredItem(subscription.DefaultItem)
                {
                    DisplayName = n.Identifier.ToString(),
                    StartNodeId = n,
                    AttributeId = Attributes.Value,
                    SamplingInterval = 1000
                };
                monitoredItem.Notification += monitor;
                subscription.AddItem(monitoredItem);
            });
            var flag = this.SessionClient.AddSubscription(subscription);
            subscription.Create();
            subscription.ApplyChanges();
            using (await _lock.EnterAsync().ConfigureAwait(false))
            {
                if (this.Subscriptions.ContainsKey(subscriptionName))
                {
                    if (this.Subscriptions.TryRemove(subscriptionName, out var subscriptionData))
                    {
                        await this.SessionClient.RemoveSubscriptionAsync(subscriptionData.Subscription).ConfigureAwait(false);
                    }
                }

                this.Subscriptions.TryAdd(subscriptionName, new SubscriptionData(subscription, monitor));
            }
            return flag;
        }
        #endregion

        #region 移除订阅
        /// <summary>
        /// 移除订阅
        /// </summary>
        /// <param name="subscriptionName">订阅名称</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>返回移除订阅状态 <see langword="true"/> 为成功 <see langword="false"/> 为失败</returns>
        public async Task<bool> RemoveSubscriptionAsync(string subscriptionName, CancellationToken cancellationToken = default)
        {
            this.Subscriptions.TryRemove(subscriptionName, out var subscriptionData);
            return await this.SessionClient.RemoveSubscriptionAsync(subscriptionData.Subscription, this.CreateLinkedTokenSource(cancellationToken).Token).ConfigureAwait(false);
        }
        /// <summary>
        /// 移除所有订阅
        /// </summary>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>返回移除订阅状态 <see langword="true"/> 为成功 <see langword="false"/> 为失败</returns>
        public async Task<bool> RemoveAllSubscriptionAsync(CancellationToken cancellationToken = default)
        {
            var flag = await this.SessionClient.RemoveSubscriptionsAsync(this.Subscriptions.Values.Select(a => a.Subscription), this.CreateLinkedTokenSource(cancellationToken).Token).ConfigureAwait(false);
            this.Subscriptions.Clear();
            return flag;
        }
        #endregion

        #region 设置订阅状态
        /// <summary>
        /// 设置订阅状态
        /// </summary>
        /// <param name="subscriptionName">访问名称</param>
        /// <param name="enabled">状态</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>返回设置订阅状态 <see langword="true"/> 为成功 <see langword="false"/> 为失败</returns>
        public async Task<bool> SetSubscriptionStatusAsync(string subscriptionName, bool enabled, CancellationToken cancellationToken = default)
        {
            if (this.Subscriptions.TryGetValue(subscriptionName, out var subscriptionData))
            {
                await subscriptionData.Subscription.SetPublishingModeAsync(enabled, this.CreateLinkedTokenSource(cancellationToken).Token).ConfigureAwait(false);
                return true;
            }
            return false;
        }
        #endregion

        #region 添加订阅项
        /// <summary>
        /// 添加订阅项
        /// </summary>
        /// <param name="subscriptionName">订阅名称</param>
        /// <param name="valueId">节点</param>
        /// <returns>返回添加订阅项状态 <see langword="true"/> 为成功 <see langword="false"/> 为失败</returns>
        public bool AddSubscriptionItem(string subscriptionName, NodeValueId valueId)
        {
            return this.AddSubscriptionItem(subscriptionName, new List<NodeValueId> { valueId });
        }
        /// <summary>
        /// 添加订阅项
        /// </summary>
        /// <param name="subscriptionName">订阅名称</param>
        /// <param name="valueIds">节点集合</param>
        /// <returns>返回添加订阅项状态 <see langword="true"/> 为成功 <see langword="false"/> 为失败</returns>
        public bool AddSubscriptionItem(string subscriptionName, List<NodeValueId> valueIds)
        {
            if (valueIds == null || valueIds.Count == 0) return false;
            if (this.Subscriptions.TryGetValue(subscriptionName, out var subscriptionData))
            {
                valueIds.Each(v =>
                {
                    var item = new MonitoredItem
                    {
                        DisplayName = v.Identifier.ToString(),
                        StartNodeId = (NodeId)v,
                        AttributeId = Attributes.Value,
                        SamplingInterval = 1000
                    };
                    item.Notification += subscriptionData.Notification;

                    subscriptionData.Subscription.AddItem(item);
                });
                subscriptionData.Subscription.ApplyChanges();
                return true;
            }
            return false;
        }
        #endregion

        #region 移除订阅项
        /// <summary>
        /// 移除订阅项
        /// </summary>
        /// <param name="subscriptionName">订阅名称</param>
        /// <param name="nodeValueId">节点</param>
        /// <returns>返回移除订阅项状态 <see langword="true"/> 为成功 <see langword="false"/> 为失败</returns>
        public bool RemoveSubscriptionItem(string subscriptionName, NodeValueId nodeValueId)
        {
            return this.RemoveSubscriptionItem(subscriptionName, new List<NodeValueId> { nodeValueId });
        }
        /// <summary>
        /// 移除订阅项
        /// </summary>
        /// <param name="subscriptionName">订阅名称</param>
        /// <param name="valueIds">节点集合</param>
        /// <returns>返回移除订阅项状态 <see langword="true"/> 为成功 <see langword="false"/> 为失败</returns>
        public bool RemoveSubscriptionItem(string subscriptionName, List<NodeValueId> valueIds)
        {
            if (valueIds == null || valueIds.Count == 0) return false;
            if (this.Subscriptions.TryGetValue(subscriptionName, out var subscriptionData))
            {
                var items = new List<MonitoredItem>();
                valueIds.Each(v =>
                {
                    var item = subscriptionData.Subscription.MonitoredItems.FirstOrDefault(a => a.StartNodeId == (NodeId)v);
                    if (item != null)
                        items.Add(item);
                });
                subscriptionData.Subscription.RemoveItems(items);
                subscriptionData.Subscription.ApplyChanges();
                return true;
            }
            return false;
        }
        #endregion

        #region 设置订阅项状态
        /// <summary>
        /// 设置订阅项状态
        /// </summary>
        /// <param name="subscriptionName">订阅名称</param>
        /// <param name="valueId">节点</param>
        /// <param name="mode">模式</param>
        /// <returns>返回操作状态</returns>
        public async Task<StatusCode> SetSubscriptionItemStatusAsync(string subscriptionName, NodeValueId valueId, MonitoringMode mode)
        {
            if (this.Subscriptions.TryGetValue(subscriptionName, out var subscriptionData))
            {
                var item = subscriptionData.Subscription.MonitoredItems.FirstOrDefault(a => a.StartNodeId == (NodeId)valueId);
                if (item == null) return StatusCodes.GoodNoData;
                return await this.SetSubscriptionItemStatusAsync(subscriptionName, item.ClientHandle, mode).ConfigureAwait(false);
            }
            return StatusCodes.GoodNoData;
        }
        /// <summary>
        /// 设置订阅项状态
        /// </summary>
        /// <param name="subscriptionName">订阅名称</param>
        /// <param name="clientHandler">项处理Id</param>
        /// <param name="mode">模式</param>
        /// <returns></returns>
        public async Task<StatusCode> SetSubscriptionItemStatusAsync(string subscriptionName, uint clientHandler, MonitoringMode mode)
        {
            var result = await this.SetSubscriptionItemStatusAsync(subscriptionName, new List<uint> { clientHandler }, mode).ConfigureAwait(false);
            if (result == null || result.Count == 0) return StatusCodes.GoodNoData;
            return result.First().Value;
        }
        /// <summary>
        /// 设置订阅项状态
        /// </summary>
        /// <param name="subscriptionName">订阅名称</param>
        /// <param name="clientHandles">项处理Id</param>
        /// <param name="mode">模式</param>
        /// <returns></returns>
        public async Task<Dictionary<MonitoredItem, StatusCode>> SetSubscriptionItemStatusAsync(string subscriptionName, List<uint> clientHandles, MonitoringMode mode)
        {
            if (this.Subscriptions.TryGetValue(subscriptionName, out var subscriptionData))
            {
                var items = new List<MonitoredItem>();
                clientHandles.Each(c =>
                {
                    var item = subscriptionData.Subscription.FindItemByClientHandle(c);
                    if (item != null) items.Add(item);
                });
                if (items.Count == 0) return new Dictionary<MonitoredItem, StatusCode>();

                var ids = new UInt32Collection();
                ids.AddRange(items.Select(a => a.ClientHandle));

                var result = await subscriptionData.Subscription.SetMonitoringModeAsync(mode, items).ConfigureAwait(false);
                if (result == null || result.Count == 0) return new Dictionary<MonitoredItem, StatusCode>();

                var results = new Dictionary<MonitoredItem, StatusCode>();
                for (var i = 0; i < items.Count; i++)
                    results.Add(items[i], result[i].StatusCode);
            }
            return new Dictionary<MonitoredItem, StatusCode>(); ;
        }
        #endregion

        #region 设置OPC客户端的日志输出
        /// <summary>
        /// 设置OPC客户端的日志输出
        /// </summary>
        /// <param name="filePath">完整的文件路径</param>
        /// <param name="deleteExisting">是否删除原文件</param>
        public void SetLogPathName(string filePath, bool deleteExisting = true)
        {
            Utils.SetTraceLog(filePath.GetBasePath(), deleteExisting);
            Utils.SetTraceMask(515);
        }
        #endregion

        #region 设置匿名用户
        /// <summary>
        /// 设置匿名用户
        /// </summary>
        public void SetAnonymousUser()
        {
            this.UserIdentity = new UserIdentity(new AnonymousIdentityToken());
        }
        #endregion

        #region 设置用户凭证
        /// <summary>
        /// 设置用户凭证
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        public void SetUser(string account, string password)
        {
            this.UserIdentity = new UserIdentity(account, password);
        }
        #endregion

        #region 设置用户证书
        /// <summary>
        /// 设置用户证书
        /// </summary>
        /// <param name="certificate">证书</param>
        public void SetUser(X509Certificate2 certificate)
        {
            this.UserIdentity = new UserIdentity(certificate);
        }
        #endregion

        #region 获取服务器连接节点
        /// <summary>
        /// 获取服务器连接节点
        /// </summary>
        /// <param name="serverUrl">服务器地址</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>返回服务器连接节点</returns>
        public async Task<EndpointDescriptionCollection> GetEndpointsAsync(string serverUrl, CancellationToken cancellationToken = default)
        {
            serverUrl = serverUrl.Multivariate(this.ServerAddress);

            if (serverUrl.IsNullOrEmpty()) throw new OpcException("请输入OPC服务器地址.");
            if (this.Configuration == null) await this.InitializeAsync().ConfigureAwait(false);
            using (var client = DiscoveryClient.Create(this.Configuration, new Uri(serverUrl)))
            {
                var endpoints = await client.GetEndpointsAsync(null, this.CreateLinkedTokenSource(cancellationToken).Token).ConfigureAwait(false);
                return endpoints;
            }
        }
        #endregion

        #region 发现本地服务
        /// <summary>
        /// 发现本地服务
        /// </summary>
        /// <param name="serverUrl">服务器地址 如 opc.tcp://localhost:53530</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>返回服务器数据</returns>
        /// <exception cref="OpcException">服务器地址有问题抛出异常</exception>
        public async Task<ApplicationDescriptionCollection> DiscoverServersAsync(string serverUrl, CancellationToken cancellationToken = default)
        {
            if (serverUrl.IsNullOrEmpty()) throw new OpcException("请输入OPC服务器地址.");
            if (this.Configuration == null) await this.InitializeAsync().ConfigureAwait(false);

            var endpointConfiguration = EndpointConfiguration.Create(this.Configuration);
            endpointConfiguration.OperationTimeout = 5000;

            using (var client = DiscoveryClient.Create(new Uri(serverUrl), endpointConfiguration))
            {
                var servers = await client.FindServersAsync(null, this.CreateLinkedTokenSource(cancellationToken).Token).ConfigureAwait(false);
                return servers;
            }
        }
        #endregion

        #region 发现网络服务
        /// <summary>
        /// 发现网络服务
        /// </summary>
        /// <param name="serverUrl">服务器地址 如 opc.tcp://localhost:53530</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>返回服务器数据</returns>
        public async Task<ServerOnNetworkCollection> DiscoverNetworkServersAsync(string serverUrl, CancellationToken cancellationToken = default)
        {
            if (serverUrl.IsNullOrEmpty()) throw new OpcException("请输入OPC服务器地址.");
            if (this.Configuration == null) await this.InitializeAsync().ConfigureAwait(false);

            var endpointConfiguration = EndpointConfiguration.Create(this.Configuration);
            endpointConfiguration.OperationTimeout = 5000;

            using (var client = DiscoveryClient.Create(new Uri(serverUrl), endpointConfiguration))
            {
                var servers = await client.FindServersOnNetworkAsync(null, 0, 100,null, this.CreateLinkedTokenSource(cancellationToken).Token).ConfigureAwait(false);
                return servers.Servers;
            }
        }
        #endregion

        #region 获取节点可用编码
        /// <summary>
        /// 获取节点可用编码
        /// </summary>
        /// <param name="nodeValueId">节点</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        public async Task<ReferenceDescriptionCollection> ReadAvailableEncodingsAsync(NodeValueId nodeValueId, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() =>
            {
                var value = this.SessionClient.ReadAvailableEncodings((NodeId)nodeValueId);
                return value;
            },this.CreateLinkedTokenSource(cancellationToken).Token).ConfigureAwait(false);
        }
        #endregion

        #region  获取指定节点的所有引用
        /// <summary>
        /// 获取指定节点的所有引用
        /// </summary>
        /// <param name="nodeId">节点</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        public async Task<ReferenceDescriptionCollection> FetchReferencesAsync(NodeValueId nodeId, CancellationToken cancellationToken = default)
        {
            return await this.SessionClient.FetchReferencesAsync((NodeId)nodeId, this.CreateLinkedTokenSource(cancellationToken).Token).ConfigureAwait(false);
        }
        #endregion

        #region 创建链接取消令牌源
        /// <summary>
        /// 创建链接取消令牌源
        /// </summary>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>返回生成的链接取消令牌源</returns>
        private CancellationTokenSource CreateLinkedTokenSource(params CancellationToken[] cancellationTokens)
        {
            var tokens = new List<CancellationToken>
            {
                this.CancellationTokenSource.Token
            };
            tokens.AddRange(cancellationTokens.Where(token => token != CancellationToken.None && !token.IsCancellationRequested));

            return CancellationTokenSource.CreateLinkedTokenSource(tokens.ToArray());
        }
        #endregion

        #region 析构器
        /// <summary>
        /// 析构器
        /// </summary>
        ~UaClient()
        {
            Dispose(false);
        }
        /// <summary>
        /// 释放
        /// </summary>
        public override void Dispose()
        {
            base.Dispose(true);
        }
        /// <summary>
        /// 释放
        /// </summary>
        /// <param name="disposing">释放状态</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing, () =>
            {
                if (this.IsConnected)
                {
                    this.DisconnectAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                }
                if (this.ReconnectHandler != null)
                {
                    this.ReconnectHandler.Dispose();
                }
            });
        }
        #endregion

        #endregion
    }
}