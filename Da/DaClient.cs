using Opc;
using Opc.Da;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XiaoFeng.OPC.DA;
using XiaoFeng.OPC.DA.Model;

/****************************************************************
*  Copyright © (2025) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2025-11-03 21:45:35                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.DA
{
    /// <summary>
    /// Da 客户端
    /// </summary>
    public class DaClient
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public DaClient()
        {

        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="serverName">服务器名称</param>
        /// <param name="serverAddress">服务器地址</param>
        public DaClient(string serverName, string serverAddress)
        {
            ServerName = serverName;
            ServerAddress = serverAddress;
        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="serverHost">服务器主机</param>
        public DaClient(ServerHost serverHost)
        {
            ServerName = serverHost.Name;
            ServerAddress = serverHost.Url.HostName;
        }

        #endregion

        #region 属性
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServerName { get; set; }
        /// <summary>
        /// 服务器地址
        /// </summary>
        public string ServerAddress { get; set; } = "localhost";
        /// <summary>
        /// 服务
        /// </summary>
        public Opc.Da.Server OpcDaServer { get; private set; }
        /// <summary>
        /// 取消指令源
        /// </summary>
        private CancellationTokenSource CancellationTokenSource { get; set; } = new CancellationTokenSource();
        /// <summary>
        /// 连接状态
        /// </summary>
        public bool IsConnected => OpcDaServer.IsConnected;
        /// <summary>
        /// 服务器关闭事件
        /// </summary>
        public event ServerShutdownEventHandler OnServerShutdown;
        /// <summary>
        /// 连接事件
        /// </summary>
        public event ConnectEventHandler OnConnected;
        /// <summary>
        /// 订阅组
        /// </summary>
        private ConcurrentDictionary<string, SubscriptionData> Subscriptions { get; set; } = new ConcurrentDictionary<string, SubscriptionData>();
        /// <summary>
        /// 订阅事件
        /// </summary>
        public event DataChangedEventHandler OnDataChanged;
        /// <summary>
        /// 连接数据
        /// </summary>
        public ConnectData ConnectData { get; set; }
        #endregion

        #region 方法

        public void Initialize()
        {

            //OpcCom.Da.Subscription
        }

        #region 连接服务
        /// <summary>
        /// 连接服务
        /// </summary>
        /// <param name="serverName">服务名称</param>
        /// <param name="serverAddress">服务地址</param>
        /// <param name="cancellationToken">取消指令</param>
        /// <returns></returns>
        public async Task<bool> ConnectAsync(string serverName, string serverAddress, CancellationToken cancellationToken = default)
        {
            var servers = await DiscoverServersAsync(serverAddress).ConfigureAwait(false);
            if (servers == null || servers.Count == 0) return false;
            var server = servers.Find(a => a.Name == serverName);
            if (server == null) return false;
            if (this.OpcDaServer != null && this.IsConnected) this.Disconnect();
            this.OpcDaServer = this.CreateOpcServer(server);
            this.OpcDaServer.Connect(this.ConnectData);
            if (this.OnConnected != null)
                this.OnConnected.Invoke(this);
            return this.IsConnected;
        }
        /// <summary>
        /// 连接服务
        /// </summary>
        /// <param name="serverHost">服务</param>
        /// <returns></returns>
        public async Task<bool> ConnectAsync(ServerHost serverHost)
        {
            return await ConnectAsync(serverHost.Name, serverHost.Url.HostName).ConfigureAwait(false);
        }
        #endregion

        #region 创建订阅
        /// <summary>
        /// 创建订阅
        /// </summary>
        /// <param name="group">组</param>
        /// <param name="dataChanged">事件</param>
        /// <returns></returns>
        public ISubscription CreateSubscription(Group group, DataChangedEventHandler dataChanged = null)
        {
            if (group == null) return null;
            if (this.Subscriptions.TryGetValue(group.Name, out var sub)) return sub.Subscription;

            var state = new SubscriptionState()
            {
                Name = group.Name,
                Active = true,
                UpdateRate = group.UpdateRate,
                Deadband = group.Deadband,
                ClientHandle = group.ClientHandle,
                ServerHandle = null
            };
            var subscription = this.OpcDaServer.CreateSubscription(state);
            var items = group.Items.Select(a => new Item
            {
                ItemName = a,
                ClientHandle = state.ClientHandle,
                ServerHandle = state.ServerHandle,
                MaxAgeSpecified=true,
                MaxAge=0,
                ActiveSpecified=true,
                Active=true
            }).ToArray();
            subscription.AddItems(items);
            //subscription.Refresh();
            if (this.OnDataChanged != null)
                subscription.DataChanged += OnDataChanged;
            var data = new SubscriptionData((Subscription)subscription, dataChanged);
            this.Subscriptions.TryAdd(group.Name, data);
            return subscription;
        }
        #endregion

        #region 取消订阅
        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <returns></returns>
        public Boolean CancelSubscription(string groupName)
        {
            if (this.Subscriptions.TryGetValue(groupName, out var sub))
            {
                this.OpcDaServer.CancelSubscription(sub.Subscription);
                sub.Subscription.Dispose();
                this.Subscriptions.TryRemove(groupName, out _);
                return true;
            }
            else return false;
        }
        #endregion

        #region 添加订阅项
        /// <summary>
        /// 添加订阅项
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <param name="items">订阅项</param>
        /// <returns></returns>
        public Boolean AddSubscriptionItems(string groupName, IEnumerable<string> items)
        {
            if (this.Subscriptions.TryGetValue(groupName, out var sub))
            {
                var subscription = sub.Subscription;
                subscription.AddItems(items.Select(a => new Item
                {
                    ItemName = a,
                    ClientHandle = subscription.ClientHandle,
                    ServerHandle = subscription.ServerHandle
                }).ToArray());
                subscription.Refresh();
                return true;
            }
            else
            {
                return this.CreateSubscription(new Group(groupName, 1000, new List<string>(items))) != null;
            }
        }
        #endregion

        #region 移除订阅项
        /// <summary>
        /// 移除订阅项
        /// </summary>
        /// <param name="group">组</param>
        /// <returns></returns>
        public Boolean RemoveSubscriptionItems(Group group)
        {
            return this.RemoveSubscriptionItems(group.Name, group.Items);
        }
        /// <summary>
        /// 移除订阅项
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <param name="items">项</param>
        /// <returns></returns>
        public Boolean RemoveSubscriptionItems(string groupName, IEnumerable<string> items)
        {
            if (this.Subscriptions.TryGetValue(groupName, out var sub))
            {
                var subscription = sub.Subscription;

                var _items = subscription.Items.Where(a => items.Contains(a.ItemName, StringComparer.OrdinalIgnoreCase)).Select(a => new ItemIdentifier
                {
                    ItemName = a.ItemName,
                    ItemPath = a.ItemPath,
                    ClientHandle = a.ClientHandle,
                    ServerHandle = a.ServerHandle
                }).ToArray();

                var length = subscription.RemoveItems(_items).Length;
                subscription.Refresh();
                return length > 0;
            }
            else
                return false;
        }
        #endregion

        #region 启用停用订阅
        /// <summary>
        /// 启用停用订阅
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <param name="enable">启用状态 true 启用 false 停用</param>
        /// <returns></returns>
        public Boolean EnableSubscription(string groupName, Boolean enable = false)
        {
            if (this.Subscriptions.TryGetValue(groupName, out var sub))
            {
                sub.Subscription.SetEnabled(enable);
                sub.Subscription.Refresh();
            }
            return false;
        }
        #endregion

        #region 读取订阅项
        /// <summary>
        /// 读取订阅项
        /// </summary>
        /// <param name="group">组</param>
        /// <returns></returns>
        public ItemValueResult[] Read(Group group)
        {
            return this.Read(group.Name, group.Items);
        }
        /// <summary>
        /// 读取订阅项
        /// </summary>
        /// <param name="groupName">组名称</param>
        /// <param name="items">项</param>
        /// <returns></returns>
        public ItemValueResult[] Read(string groupName, IEnumerable<string> items = null)
        {
            if (this.Subscriptions.TryGetValue(groupName, out var sub))
            {
                var subscription = sub.Subscription;
                if (items == null || items.Count() == 0)
                {
                    return this.OpcDaServer.Read(subscription.Items);
                }
                else
                {
                    return this.OpcDaServer.Read(items.Select(a => new Item { ItemName = a, ClientHandle = subscription.ClientHandle, ServerHandle = subscription.ServerHandle }).ToArray());
                }
            }
            return null;
        }
        #endregion

        #region 写入订阅项
        /// <summary>
        /// 写入订阅项
        /// </summary>
        /// <param name="groupName">组名称</param>
        /// <param name="items">项集合</param>
        /// <returns></returns>
        public Boolean Write(string groupName, IEnumerable<ItemValue> items)
        {
            if (items == null || items.Count() == 0) return false;
            if (this.Subscriptions.TryGetValue(groupName, out var sub))
            {
                var subscription = sub.Subscription;
                items.Each(a =>
                {
                    a.ClientHandle = subscription.ClientHandle;
                    a.ServerHandle = subscription.ServerHandle;
                    a.Quality = Quality.Good;
                });
                return this.OpcDaServer.Write(items.ToArray()).Length > 0;
            }
            return false;
        }
        /// <summary>
        /// 写入订阅项
        /// </summary>
        /// <param name="groupName">组名称</param>
        /// <param name="items">项集合</param>
        /// <returns></returns>
        public Boolean Write(string groupName, IEnumerable<ItemDataValue> items)
        {
            if (items == null || items.Count() == 0) return false;
            if (this.Subscriptions.TryGetValue(groupName, out var sub))
            {
                var subscription = sub.Subscription;
                var _items = items.Select(a => new ItemValue
                {
                    ItemName = a.Name,
                    ClientHandle = subscription.ClientHandle,
                    ServerHandle = subscription.ServerHandle,
                    Quality = Quality.Good,
                    Value = a.Value
                }).ToArray();

                return this.OpcDaServer.Write(_items).Length > 0;
            }
            return false;
        }
        #endregion

        #region 浏览节点
        /// <summary>
        /// 浏览节点
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="filter">浏览筛选器</param>
        /// <returns></returns>
        public BrowseElement[] Brower(string node = "", browseFilter filter = browseFilter.all)
        {
            var itemId = node.IsNullOrEmpty() ? new ItemIdentifier() : new ItemIdentifier(node);
            return this.OpcDaServer.Browse(itemId, new BrowseFilters { BrowseFilter = filter }, out var position);
        }
        #endregion

        #region 断开连接
        /// <summary>
        /// 断开连接
        /// </summary>
        public void Disconnect()
        {
            if (this.OpcDaServer != null)
            {
                this.OpcDaServer.Disconnect();
            }
        }
        #endregion

        #region 设置服务
        /// <summary>
        /// 设置服务
        /// </summary>
        /// <param name="server">服务</param>
        /// <returns></returns>
        public DaClient WithServer(ServerHost server)
        {
            this.ServerName = server.Name;
            this.ServerAddress = server.Url.HostName;
            return this;
        }
        #endregion

        #region 设置服务名称
        /// <summary>
        /// 设置服务名称
        /// </summary>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        public DaClient WithServerName(string name)
        {
            this.ServerName = name;
            return this;
        }
        #endregion

        #region 设置服务地址
        /// <summary>
        /// 设置服务地址
        /// </summary>
        /// <param name="serverAddress">服务地址</param>
        /// <returns></returns>
        public DaClient WithServerAddress(string serverAddress)
        {
            this.ServerAddress = serverAddress;
            return this;
        }
        #endregion

        #region 发现服务
        /// <summary>
        /// 发现服务
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <param name="cancellationToken">取消指令</param>
        /// <returns></returns>
        public async Task<List<Model.ServerHost>> DiscoverServersAsync(string ip, CancellationToken cancellationToken = default)
        {
            if (ip.IsNullOrEmpty()) return null;
            return await Task.Factory.StartNew(() =>
            {
                using (var discovery = new OpcCom.ServerEnumerator())
                {
                    var enumerate = discovery.GetAvailableServers(Specification.COM_DA_20, ip, null);
                    return new List<Model.ServerHost>(enumerate.Select(i => new Model.ServerHost(i.Name, i.Url)));
                }
            }, CreateLinkedTokenSource(cancellationToken).Token);
        }
        #endregion

        #region 创建服务
        /// <summary>
        /// 创建服务
        /// </summary>
        /// <param name="url">地址</param>
        /// <returns></returns>
        public Opc.Da.Server CreateOpcServer(string url)
        {
            return CreateOpcServer(new URL(url));
        }
        /// <summary>
        /// 创建服务
        /// </summary>
        /// <param name="host">服务</param>
        /// <returns></returns>
        public Opc.Da.Server CreateOpcServer(ServerHost host)
        {
            return CreateOpcServer(host.Url);
        }
        /// <summary>
        /// 创建服务
        /// </summary>
        /// <param name="url">地址</param>
        /// <returns></returns>
        public Opc.Da.Server CreateOpcServer(URL url)
        {
            var server = new Opc.Da.Server(new OpcCom.Factory(), url);
            if (this.OnServerShutdown != null)
                server.ServerShutdown += this.OnServerShutdown;
            return server;
        }
        /// <summary>
        /// 创建服务并连接
        /// </summary>
        /// <param name="url">地址</param>
        /// <returns></returns>
        public Opc.Da.Server CreateOpcServerAndConnect(string url)
        {
            var server = CreateOpcServer(url);
            server.Connect(this.ConnectData);
            return server;
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

        #endregion
    }
}