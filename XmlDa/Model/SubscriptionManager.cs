using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

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
        #endregion

        #region 属性
        /// <summary>
        /// 订阅数据
        /// </summary>
        private ConcurrentDictionary<string, Subscription> SubscriptionCollection = new ConcurrentDictionary<string, Subscription>();
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
            if(this.SubscriptionCollection.TryRemove(subscriptionId, out var _)){

                return true;
            }
            return false;
        }
        /// <summary>
        /// 获取订阅
        /// </summary>
        /// <param name="id">订阅ID</param>
        /// <param name="subscription">订阅</param>
        /// <returns></returns>
        public bool TryGet(string id, out Subscription subscription)
        {
            if (this.SubscriptionCollection.TryGetValue(id, out var sub))
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
        /// <param name="id">订阅ID</param>
        /// <returns></returns>
        public bool ContainsKey(string id)
        {
            return this.SubscriptionCollection.ContainsKey(id);
        }
        #endregion
    }
}