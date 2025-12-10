using System;
using System.Collections.Generic;
using System.Text;

/****************************************************************
*  Copyright © (2025) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2025-12-10 22:37:20                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XML.Model
{
    /// <summary>
    /// 订阅信息
    /// </summary>
    public class SubscriptionInfo
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public SubscriptionInfo()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 服务器分配的订阅 ID(核心标识)
        /// </summary>
        public string SubscriptionID { get; set; } = string.Empty;
        /// <summary>
        /// 更新率(毫秒)
        /// </summary>
        public int UpdateRate { get; set; }
        /// <summary>
        /// 保持活动时间(秒，0 表示永久)
        /// </summary>
        public int KeepAliveTime { get; set; }
        /// <summary>
        /// 订阅的标签列表
        /// </summary>
        public List<OpcItem> Items { get; set; } = new List<OpcItem>();
        /// <summary>
        /// 回调地址
        /// </summary>
        public string CallbackURL { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastRefreshTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 是否是轮询模式
        /// </summary>
        public bool IsPollingMode { get; set; }
        #endregion

        #region 方法

        #endregion
    }
}