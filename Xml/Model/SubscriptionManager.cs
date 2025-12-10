using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/****************************************************************
*  Copyright © (2025) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2025-12-10 23:18:31                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XML.Model
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
        #endregion

        #region 属性
        /// <summary>
        /// 订阅集合
        /// </summary>
        private readonly Dictionary<string, SubscriptionInfo> _subscriptions = new Dictionary<string, SubscriptionInfo>();
        /// <summary>
        /// 锁
        /// </summary>
        private readonly object _lockObj = new object();
        #endregion

        #region 方法
        /// <summary>
        /// 添加订阅
        /// </summary>
        public void AddSubscription(SubscriptionInfo subscription)
        {
            if (string.IsNullOrEmpty(subscription.SubscriptionID))
                throw new ArgumentException("订阅 ID 不能为空", nameof(subscription.SubscriptionID));

            lock (_lockObj)
            {
                if (_subscriptions.ContainsKey(subscription.SubscriptionID))
                    throw new OpcException($"订阅 ID {subscription.SubscriptionID} 已存在");

                _subscriptions[subscription.SubscriptionID] = subscription;
            }
        }

        /// <summary>
        /// 获取订阅
        /// </summary>
        public SubscriptionInfo GetSubscription(string subscriptionId)
        {
            lock (_lockObj)
            {
                _subscriptions.TryGetValue(subscriptionId, out var subscription);
                return subscription;
            }
        }

        /// <summary>
        /// 更新订阅
        /// </summary>
        public void UpdateSubscription(SubscriptionInfo updatedSubscription)
        {
            if (string.IsNullOrEmpty(updatedSubscription.SubscriptionID))
                throw new ArgumentException("订阅 ID 不能为空", nameof(updatedSubscription.SubscriptionID));

            lock (_lockObj)
            {
                if (!_subscriptions.ContainsKey(updatedSubscription.SubscriptionID))
                    throw new OpcException($"订阅 ID {updatedSubscription.SubscriptionID} 不存在");

                _subscriptions[updatedSubscription.SubscriptionID] = updatedSubscription;
            }
        }

        /// <summary>
        /// 移除订阅
        /// </summary>
        public bool RemoveSubscription(string subscriptionId)
        {
            lock (_lockObj)
            {
                return _subscriptions.Remove(subscriptionId);
            }
        }

        /// <summary>
        /// 获取所有订阅
        /// </summary>
        public List<SubscriptionInfo> GetAllSubscriptions()
        {
            lock (_lockObj)
            {
                return _subscriptions.Values.ToList();
            }
        }

        /// <summary>
        /// 检查过期订阅（超过 KeepAliveTime 未刷新）
        /// </summary>
        public List<string> GetExpiredSubscriptionIds()
        {
            lock (_lockObj)
            {
                return _subscriptions.Values
                    .Where(s => s.KeepAliveTime > 0 &&
                               DateTime.Now - s.LastRefreshTime > TimeSpan.FromSeconds(s.KeepAliveTime))
                    .Select(s => s.SubscriptionID)
                    .ToList();
            }
        }

        /// <summary>
        /// 刷新订阅的最后活动时间
        /// </summary>
        public void RefreshSubscriptionActivity(string subscriptionId)
        {
            lock (_lockObj)
            {
                if (_subscriptions.TryGetValue(subscriptionId, out var subscription))
                {
                    subscription.LastRefreshTime = DateTime.Now;
                }
            }
        }
        #endregion
    }
}