using System;
using System.Collections.Generic;
using System.Text;

/****************************************************************
*  Copyright © (2025) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2025-12-10 22:36:14                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XML.Model
{
    /// <summary>
    /// 订阅通知数据(服务器回调或轮询返回的更新)
    /// </summary>
    public class SubscriptionNotification
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public SubscriptionNotification()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 服务器分配的订阅 ID(核心标识)
        /// </summary>
        public string SubscriptionID { get; set; } = string.Empty;
        /// <summary>
        /// 更新的标签数据
        /// </summary>
        public List<OpcItemResult> UpdatedItems { get; set; } = new List<OpcItemResult>();
        /// <summary>
        /// 通知时间
        /// </summary>
        public DateTime NotificationTime { get; set; } = DateTime.Now;
        #endregion

        #region 方法

        #endregion
    }
}