using Opc.Da;
using System;
using System.Collections.Generic;
using System.Text;

/****************************************************************
*  Copyright © (2025) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2025-11-03 23:30:17                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.DA.Model
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
        public SubscriptionData(Subscription subscription, DataChangedEventHandler dataChanged)
        {
            Subscription = subscription;
            if (dataChanged != null)
                this.DataChanged += dataChanged;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 订阅信息
        /// </summary>
        public Subscription Subscription { get; set; }
        /// <summary>
        /// 事件
        /// </summary>
        public event DataChangedEventHandler DataChanged
        {
            add { Subscription.DataChanged += value; }
            remove { Subscription.DataChanged -= value; }
        }
        #endregion

        #region 方法

        #endregion
    }
}