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
*  Create Time : 2026-01-09 01:24:10                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 订阅取消请求
    /// </summary>
    [XmlRoot("SubscriptionCancel", Namespace = XmlDaHelper.Namesapce)]
    public class SubscriptionCancelRequest
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public SubscriptionCancelRequest()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 服务器子句柄
        /// </summary>
        [XmlAttribute("ServerSubHandle")]
        public string ServerSubHandle { get; set; }
        /// <summary>
        /// 客户端请求句柄
        /// </summary>
        [XmlAttribute("ClientRequestHandle")]
        public string ClientRequestHandle { get; set; }
        #endregion

        #region 方法

        #endregion
    }
}