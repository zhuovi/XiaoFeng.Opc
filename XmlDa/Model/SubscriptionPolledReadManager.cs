using Opc.Ua;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XiaoFeng.Log;

/****************************************************************
*  Copyright © (2026) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2026-03-19 16:35:59                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 订阅轮询读管理器
    /// </summary>
    public class SubscriptionPolledReadManager
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public SubscriptionPolledReadManager()
        {

        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="daClient">客户端</param>
        public SubscriptionPolledReadManager(XmlDaClient daClient, NotificationPolledReadEventHadler notification = null)
        {
            this.DaClient = daClient;
            if (notification != null)
                this.Notifiation += notification;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 订阅数量
        /// </summary>
        public int Count => (int)this.SubscriptionCollection?.Count;
        /// <summary>
        /// 是否为空
        /// </summary>
        public bool IsEmpty => this.SubscriptionCollection == null || this.SubscriptionCollection.Count == 0;
        /// <summary>
        /// 订阅列表
        /// </summary>
        public ICollection<SubscriptionPolledRead> Subscriptions => this.SubscriptionCollection?.Values;
        /// <summary>
        /// 客户端
        /// </summary>
        private XmlDaClient DaClient { get; set; }
        /// <summary>
        /// 订阅数据
        /// </summary>
        private ConcurrentDictionary<string, SubscriptionPolledRead> SubscriptionCollection = new ConcurrentDictionary<string, SubscriptionPolledRead>();
        /// <summary>
        /// 回调事件
        /// </summary>
        public event NotificationPolledReadEventHadler Notifiation;
        /// <summary>
        /// 日志
        /// </summary>
        public ILog Log { get; set; }
        /// <summary>
        /// 是否输出日志
        /// </summary>
        public bool IsConsoleLog { get; set; }
        #endregion

        #region 方法

        #region 添加订阅
        /// <summary>
        /// 添加订阅
        /// </summary>
        /// <param name="subscription">订阅</param>
        /// <param name="ExistisFunction">订阅存在回调</param>
        public void AddSubscription(SubscriptionPolledRead subscription, Action<SubscriptionPolledReadManager, SubscriptionPolledRead> ExistisFunction = null)
        {
            if (this.SubscriptionCollection == null) this.SubscriptionCollection = new ConcurrentDictionary<string, SubscriptionPolledRead>();
            this.WriteLog($"Start adding subscription.");
            if (this.TryGetValue(subscription.Id, out SubscriptionPolledRead sub))
            {
                this.WriteLog($"Subscription[subscriptionId={subscription.Id}] already exists");
                ExistisFunction?.Invoke(this, sub);
            }
            else
                if (this.SubscriptionCollection.TryAdd(subscription.Id, subscription))
                {
                    this.WriteLog($"Add the subscription to the subscription manager[subscriptionId={subscription.Id}].");
                    this.WriteLog($"Add subscription success.");
                }
        }
        /// <summary>
        /// 添加订阅
        /// </summary>
        /// <param name="subscriptionId">订阅ID</param>
        /// <param name="rate">速率</param>
        /// <param name="returnAllItems">是否返回所有项,true则返回所有项,false只返回有变动的项</param>
        /// <param name="notification">回调</param>
        /// <param name="items">订阅项</param>
        public void AddSubscription(string subscriptionId, int rate, bool returnAllItems, NotificationPolledReadEventHadler notification, params ItemIdentifier[] items)
        {
            this.WriteLog($"Start add subscription.");
            var sub = new SubscriptionPolledRead();
            sub.Id = subscriptionId;
            sub.Items = items.ToList();
            sub.UpdateRate = rate;
            sub.IsDebug = this.IsConsoleLog;
            sub.ReturnAllItems = returnAllItems;
            sub.Notification = (subscription, itemValues) =>
            {
                notification?.Invoke(this, subscription, itemValues);
            };
            sub.Callback = async (job, subscription) =>
            {
                this.WriteLog($"Start read subscription[subscriptionId={subscriptionId}].");
                var result = await this.DaClient.ReadAsync(subscription.Items).ConfigureAwait(false);
                if (result == null || result.Status != ResponseStatus.Success) return;
                var itemVals = result.Data.RItemList.Items;
                this.WriteLog($"return items:{itemVals.ToJson()}");
                if (subscription.ReturnAllItems)
                {
                    this.WriteLog($"return all items.");
                    new Task(v =>
                    {
                        var val = v as List<ItemValue>;
                        notification?.Invoke(this, subscription, itemVals);
                        this.Notifiation?.Invoke(this, sub, itemVals);
                    }, itemVals).Start();
                    
                }
                else
                {
                    this.WriteLog($"return changed items.");
                    var list = new List<ItemValue>();
                    if (subscription.Nodes == null) subscription.Nodes = new ConcurrentDictionary<string, ItemValue>();
                    if (subscription.Nodes.Count == 0)
                    {
                        itemVals.Each(item =>
                        {
                            list.Add(item);
                            subscription.Nodes.TryAdd(item.ItemName, item);
                        });
                    }
                    else
                    {
                        itemVals.Each(item =>
                        {
                            if (subscription.Nodes.TryGetValue(item.ItemName, out var itemValue))
                            {
                                if (itemValue.Value.Value.EqualsIgnoreCase(item.Value.Value)) return;
                                subscription.Nodes[item.ItemName] = itemValue;
                            }
                            else
                            {
                                subscription.Nodes.TryAdd(item.ItemName, item);
                            }
                            list.Add(item);
                        });
                    }
                    if (list.Count > 0)
                    {
                        new Task(v =>
                        {
                            var val = v as List<ItemValue>;
                            notification?.Invoke(this, subscription, itemVals);
                            this.Notifiation?.Invoke(this, sub, itemVals);
                        }, list).Start();
                    }
                }
                sub.LastTime = DateTime.Now;
            };
            sub.Start();
            this.AddSubscription(sub);
            this.WriteLog($"Add subscription success.");
        }
        #endregion

        #region 移除订阅
        /// <summary>
        /// 移除订阅
        /// </summary>
        /// <param name="subscriptionId">订阅Id</param>
        public bool RemoveSubscription(string subscriptionId)
        {
            if (this.IsEmpty) return false;

            this.WriteLog($"Start remove subscription[subscriptionId={subscriptionId}].");

            if (this.SubscriptionCollection.TryRemove(subscriptionId, out var subscription))
            {
                this.WriteLog($"Remove subscription succcess[subscriptionId={subscriptionId}].");
                subscription.Stop();
                return true;
            }
            return false;
        }
        #endregion

        #region 清空订阅
        /// <summary>
        /// 清空订阅
        /// </summary>
        public void Clear()
        {
            this.WriteLog($"Clear subscriptions.");
            if (this.IsEmpty) return;
            this.SubscriptionCollection.Values.Each(r =>
            {
                r.Stop();
            });
            this.SubscriptionCollection.Clear();
        }
        #endregion

        #region 获取订阅
        /// <summary>
        /// 获取订阅
        /// </summary>
        /// <param name="subscriptionId">订阅ID</param>
        /// <param name="subscription">订阅</param>
        /// <returns></returns>
        public bool TryGetValue(string subscriptionId, out SubscriptionPolledRead subscription)
        {
            if (this.IsEmpty)
            {
                subscription = null;
                return false;
            }

            if (this.SubscriptionCollection.TryGetValue(subscriptionId, out var sub))
            {
                subscription = sub;
                return true;
            }
            subscription = null;
            return false;
        }
        #endregion

        #region 是否存在订阅
        /// <summary>
        /// 是否存在订阅
        /// </summary>
        /// <param name="subscriptionId">订阅ID</param>
        /// <returns></returns>
        public bool ContainsKey(string subscriptionId)
        {
            if (this.IsEmpty) return false;

            return this.SubscriptionCollection.ContainsKey(subscriptionId);
        }
        #endregion

        #region 启用或禁用订阅
        /// <summary>
        /// 启用或禁用订阅
        /// </summary>
        /// <param name="subscriptionId">订阅ID</param>
        /// <param name="enabled">状态 true启用 false 禁用</param>
        /// <returns></returns>
        public async Task<bool> EnableAsync(string subscriptionId, bool enabled)
        {
            if (this.TryGetValue(subscriptionId, out var sub))
            {
                if (sub.Enable == enabled) return false;
                this.WriteLog($"{(enabled ? "Enable" : "Disable")} subscription[subscriptionId={subscriptionId}].");
                this.SubscriptionCollection[subscriptionId].Enable = enabled;
                if (enabled)
                    sub.Resume();
                else sub.Pause();
                return true;
            }
            return false;
        }
        #endregion

        #region 输出日志
        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="message">日志</param>
        private void WriteLog(string message)
        {
            if (this.IsConsoleLog)
                LogHelper.Debug(message);
        }
        #endregion

        #endregion
    }
}