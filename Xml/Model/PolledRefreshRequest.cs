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
*  Create Time : 2025-12-10 22:33:03                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XML.Model
{
    /// <summary>
    /// 轮询订阅更新请求
    /// </summary>
    [XmlRoot("PolledRefresh", Namespace = OpcXmlHelper.Namespace)]
    public class PolledRefreshRequest:BaseRequest
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public PolledRefreshRequest()
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
        /// 是否清除服务器端缓存的更新数据
        /// </summary>
        [XmlElement(Namespace = OpcXmlHelper.Namespace)]
        public bool ReleaseItems { get; set; } = false;
        #endregion

        #region 方法

        #endregion
    }
}