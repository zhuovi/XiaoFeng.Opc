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
*  Create Time : 2026-01-09 01:00:03                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 订阅请求
    /// </summary>
    [XmlRoot("Subscribe", Namespace = XmlDaHelper.Namesapce)]
    public class SubscribeRequest
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public SubscribeRequest()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 读配置
        /// </summary>
        [XmlElement("Options")]
        public RequestOptions Options { get; set; }
        /// <summary>
        /// 项列表
        /// </summary>
        [XmlElement("ItemList")]
        public SubscribeRequestItemList ItemList { get; set; }
        /// <summary>
        /// 回复时返回值
        /// </summary>
        [XmlAttribute("ReturnValuesOnReply")]
        public bool ReturnValuesOnReply { get; set; }
        /// <summary>
        /// 订阅率
        /// </summary>
        [XmlAttribute("SubscriptionPingRate")]
        public int SubscriptionPingRate { get; set; }
        #endregion

        #region 方法

        #endregion
    }
}