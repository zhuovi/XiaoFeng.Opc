using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

/****************************************************************
*  Copyright © (2026) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2026-01-09 01:20:23                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 订阅轮询刷新响应
    /// </summary>
    [XmlRoot("SubscriptionPolledRefreshResponse", Namespace = XmlDaHelper.Namesapce)]
    public class SubscriptionPolledRefreshResponse
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public SubscriptionPolledRefreshResponse()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 读结果
        /// </summary>
        public ReplyBase SubscriptionPolledRefreshResult { get; set; }
        /// <summary>
        /// 项列表
        /// </summary>
        public SubscribePolledRefreshReplyItemList RItemList { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        [XmlArrayItem("Errors")]
        public List<OPCError> Errors { get; set; }
        /// <summary>
        /// 无效的服务器子句柄
        /// </summary>
        public List<string> InvalidServerSubHandles { get; set; }
        /// <summary>
        /// 数据缓冲区溢出
        /// </summary>
        [XmlAttribute("DataBufferOverflow")]
        public bool DataBufferOverflow { get; set; }
        #endregion

        #region 方法

        #endregion
    }
}