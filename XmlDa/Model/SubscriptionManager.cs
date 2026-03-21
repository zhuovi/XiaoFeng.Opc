using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XiaoFeng.Log;
using XiaoFeng.OPC.XML.Model;
using XiaoFeng.Threading;

/****************************************************************
*  Copyright © (2026) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2026-01-14 00:23:15                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 订阅管理器
    /// </summary>
    public class SubscriptionManager
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public SubscriptionManager()
        {
            
        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="daClient">客户端</param>
        public SubscriptionManager(XmlDaClient daClient,NotificationEventHadler notification = null)
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
        public ICollection<Subscription> Subscriptions => this.SubscriptionCollection?.Values;
        /// <summary>
        /// 轮询状态
        /// </summary>
        private bool Alive => !(this.CancellationTokenSource == null || this.CancellationTokenSource.IsCancellationRequested);
        /// <summary>
        /// 客户端
        /// </summary>
        private XmlDaClient DaClient { get; set; }
        /// <summary>
        /// 订阅数据
        /// </summary>
        private ConcurrentDictionary<string, Subscription> SubscriptionCollection = new ConcurrentDictionary<string, Subscription>();
        /// <summary>
        /// 取消标识
        /// </summary>
        private CancellationTokenSource CancellationTokenSource;
        /// <summary>
        /// 更新速率
        /// </summary>
        private int? _UpdateRate;
        /// <summary>
        /// 更新速率
        /// </summary>
        private int? UpdateRate
        {
            get => this._UpdateRate;
            set => this._UpdateRate = value < 3000 ? 3000 : value;
        }
        /// <summary>
        /// 回调事件
        /// </summary>
        public event NotificationEventHadler Notifiation;
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
        public void AddSubscription(Subscription subscription, Action<SubscriptionManager, Subscription> ExistisFunction = null)
        {
            if (this.SubscriptionCollection == null) this.SubscriptionCollection = new ConcurrentDictionary<string, Subscription>();
            this.WriteLog($"Start adding subscription.");
            if (this.TryGetValue(subscription.Id, out Subscription sub))
            {
                this.WriteLog($"Subscription[subscriptionId={subscription.Id}] already exists");
                ExistisFunction?.Invoke(this, sub);
            }
            else
                if (this.SubscriptionCollection.TryAdd(subscription.Id, subscription))
            {
                this.WriteLog($"Add the subscription to the subscription manager[subscriptionId={subscription.Id}].");
                if (this.UpdateRate.HasValue)
                {
                    var _OldRate = this.UpdateRate.Value;
                    this.UpdateRate = Math.Min(this.UpdateRate.GetValueOrDefault(), subscription.UpdateRate);
                    this.WriteLog($"The original subscription rate was {_OldRate}, and the updated rate is {this.UpdateRate}.");
                }
                else
                {
                    this.UpdateRate = subscription.UpdateRate;
                    this.WriteLog($"Subscription rate was {this.UpdateRate}.");
                }
                this.WriteLog($"Add subscription success.");
            }
            if (!this.Alive) this.StartAsync().ConfigureAwait(false);
        }
        #endregion

        #region 移除订阅
        /// <summary>
        /// 移除订阅
        /// </summary>
        /// <param name="subscriptionId">订阅Id</param>
        public async Task<bool> RemoveSubscriptionAsync(string subscriptionId)
        {
            if (this.IsEmpty) return false;

            this.WriteLog($"Start remove subscription[subscriptionId={subscriptionId}].");

            if (this.SubscriptionCollection.TryRemove(subscriptionId, out var subscription))
            {
                var val = this.SubscriptionCollection.Min(a => a.Value.UpdateRate);
                this.WriteLog($"The original subscription rate was {this.UpdateRate}, and the updated rate is {val}.");
                this.UpdateRate = val;
                if (this.SubscriptionCollection.Count == 0) await this.StopAsync().ConfigureAwait(false);

                this.WriteLog($"Remove subscription succcess[subscriptionId={subscriptionId}].");
                return true;
            }
            if (this.Count == 0) await this.StopAsync().ConfigureAwait(false);
            return false;
        }
        #endregion

        #region 清空订阅
        /// <summary>
        /// 清空订阅
        /// </summary>
        public async Task ClearAsync()
        {
            this.WriteLog($"Clear subscriptions.");
            if (this.IsEmpty) return;
            this.SubscriptionCollection.Clear();
            await this.StopAsync().ConfigureAwait(false);
        }
        #endregion

        #region 获取订阅
        /// <summary>
        /// 获取订阅
        /// </summary>
        /// <param name="subscriptionId">订阅ID</param>
        /// <param name="subscription">订阅</param>
        /// <returns></returns>
        public bool TryGetValue(string subscriptionId, out Subscription subscription)
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
                return true;
            }
            return false;
        }
        #endregion

        #region 启动轮询订阅
        /// <summary>
        /// 启动轮询订阅
        /// </summary>
        /// <returns></returns>
        private async Task StartAsync()
        {
            this.CancellationTokenSource = new CancellationTokenSource();
            this.CancellationTokenSource.Token.Register(() =>
            {
                this.WriteLog($"Stop subscriptions polled refresh.");
            });
            this.WriteLog($"Start subscriptions polled refresh.");
            await Task.Factory.StartNew(async () =>
             {
                 while (!this.CancellationTokenSource.IsCancellationRequested)
                 {
                     if (this.IsEmpty)
                     {
                         await this.StopAsync().ConfigureAwait(false);
                     }
                     /*
                      * 正式
                      */
                     var subscriptionIds = new List<string>(this.SubscriptionCollection.Where(a => a.Value.Enable).Select(a => a.Key));
                     var response = await this.DaClient.SubscriptionPolledRefreshAsync(new List<string>(subscriptionIds)).ConfigureAwait(false);

                     if (response.Status == ResponseStatus.Success)
                     {
                         if (response.Data.RItemList == null)
                         {
                             //查询失败
                             this.WriteLog($"Query failed.");
                         }
                         else
                         {
                             this.WriteLog($"The subscription value has changed.");
                             this.WriteLog(response.Data.ToJson());
                             foreach (var item in response.Data.RItemList)
                             {
                                 if (this.TryGetValue(item.SubscriptionHandle, out var sub))
                                 {
                                     this.SubscriptionCollection[item.SubscriptionHandle].LastTime = DateTime.Now;
                                     Task.Factory.StartNew(() =>
                                     {
                                         sub.Notification?.Invoke(sub, item.Items);
                                         this.Notifiation?.Invoke(this, sub, item.Items);
                                     }).ForgetTaskSafe();
                                 }
                                 else
                                 {
                                     this.Notifiation?.Invoke(this, null, item.Items);
                                 }
                             }
                         }
                     }
                     else
                     {
                         //查询失败
                         this.WriteLog($"Query failed.");
                     }
                     /*
                      * 测试
                      */
                     /*
                     this.WriteLog($"The subscription value has changed.");
                     this.SubscriptionCollection.Keys.Each(key =>
                     {
                         if (this.TryGetValue(key, out var subscription))
                         {
                             if (!subscription.Enable) return;

                             var values = new List<ItemValue> { new ItemValue() { ItemName = "a", Value = "aaa" }, new ItemValue() { ItemName = "aa", Value = "aaaa" } };
                             Task.Factory.StartNew(() =>
                             {
                                 this.Notifiation?.Invoke(this, subscription, values);
                                 subscription.Notification?.Invoke(subscription, values);
                             }).ForgetTaskSafe();
                         }
                     });
                     */
                     await Task.Delay(this.UpdateRate.GetValueOrDefault(), this.CancellationTokenSource.Token).ConfigureAwait(false);
                 }
                 await this.StopAsync().ConfigureAwait(false);
             }, this.CancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default).ConfigureAwait(false);
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
                this.Log?.Debug(message);
        }
        #endregion

        #region 取消轮询订阅
        /// <summary>
        /// 取消轮询订阅
        /// </summary>
        public async Task StopAsync()
        {
            this.WriteLog($"Stop subscriptions polled refresh.");
            await Task.Run(() =>
            {
                this.CancellationTokenSource?.Cancel();
                this.CancellationTokenSource?.Dispose();
            }).ConfigureAwait(false);
        }
        #endregion

        #endregion
    }
}