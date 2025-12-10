using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

/****************************************************************
*  Copyright © (2025) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2025-12-10 22:23:49                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XML.Model
{
    /// <summary>
    /// 修改订阅请求
    /// </summary>
    [XmlRoot("ModifySubscription", Namespace = OpcXmlHelper.Namespace)]
    public class ModifySubscriptionRequest:BaseRequest
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public ModifySubscriptionRequest()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 服务器分配的订阅 ID(核心标识)
        /// </summary>
        [XmlElement(Namespace = OpcXmlHelper.Namespace)]
        public string SubscriptionID { get; set; } = string.Empty;
        /// <summary>
        /// 订阅更新率(毫秒)
        /// </summary>
        [XmlElement(Namespace = OpcXmlHelper.Namespace)]
        public int RequestedUpdateRate { get; set; }
        /// <summary>
        /// 保持活动时间(秒，0 表示永久)
        /// </summary>
        [XmlElement(Namespace = OpcXmlHelper.Namespace)]
        public int KeepAliveTime { get; set; }
        /// <summary>
        /// 订阅的标签列表
        /// </summary>
        [XmlArray("Items", Namespace = OpcXmlHelper.Namespace), XmlArrayItem("Item")]
        public List<OpcItem> Items { get; set; } = new List<OpcItem>();
        #endregion

        #region 方法

        #endregion
    }
}