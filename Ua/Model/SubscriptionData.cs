using Opc.Ua.Client;
using System;
using System.Collections.Generic;
using System.Text;

/****************************************************************
*  Copyright © (2025) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2025-06-25 14:39:44                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.Opc.Model
{
    /// <summary>
    /// 订阅数据
    /// </summary>
    public class SubscriptionData
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public SubscriptionData()
        {

        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="subscription">订阅</param>
        /// <param name="notification">回调事件</param>
        public SubscriptionData(Subscription subscription, MonitoredItemNotificationEventHandler notification)
        {
            Subscription = subscription;
            Notification = notification;
        }

        #endregion

        #region 属性
        /// <summary>
        /// 订阅
        /// </summary>
        public Subscription Subscription { get; set; }
        /// <summary>
        /// 订阅事件
        /// </summary>
        public MonitoredItemNotificationEventHandler Notification;
        #endregion

        #region 方法
        #region 析构器
        /// <summary>
        /// 析构器
        /// </summary>
        ~SubscriptionData()
        {

        }
        #endregion
        #endregion
    }
}