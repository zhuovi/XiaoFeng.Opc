using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

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
        public SubscriptionManager(XmlDaClient daClient)
        {
            this.DaClient = daClient;
        }
        #endregion

        #region 属性
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
        private CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();
        /// <summary>
        /// 更新速率
        /// </summary>
        private int UpdateRate { get; set; }
        /// <summary>
        /// 回调事件
        /// </summary>
        public event NotificationEventHadler Notifiation;
        #endregion

        #region 方法
        /// <summary>
        /// 添加订阅
        /// </summary>
        /// <param name="subscription">订阅</param>
        /// <param name="ExistisFunction">订阅存在回调</param>
        public void AddSubscription(Subscription subscription,Action<SubscriptionManager,Subscription> ExistisFunction = null)
        {
            if (this.SubscriptionCollection.TryGetValue(subscription.Id, out Subscription sub))
            {
                ExistisFunction?.Invoke(this, sub);
            }
            else
                this.SubscriptionCollection.TryAdd(subscription.Id, subscription);
        }
        /// <summary>
        /// 移除订阅
        /// </summary>
        /// <param name="subscriptionId">订阅Id</param>
        public bool RemoveSubscription(string subscriptionId)
        {
            if (this.SubscriptionCollection.TryRemove(subscriptionId, out var subscription))
            {
                subscription.Stop();
                return true;
            }
            return false;
        }
        /// <summary>
        /// 获取订阅
        /// </summary>
        /// <param name="subscriptionId">订阅ID</param>
        /// <param name="subscription">订阅</param>
        /// <returns></returns>
        public bool TryGet(string subscriptionId, out Subscription subscription)
        {
            if (this.SubscriptionCollection.TryGetValue(subscriptionId, out var sub))
            {
                subscription = sub;
                return true;
            }
            subscription = null;
            return false;
        }
        /// <summary>
        /// 是否存在订阅
        /// </summary>
        /// <param name="subscriptionId">订阅ID</param>
        /// <returns></returns>
        public bool ContainsKey(string subscriptionId)
        {
            return this.SubscriptionCollection.ContainsKey(subscriptionId);
        }
        #endregion
    }
}