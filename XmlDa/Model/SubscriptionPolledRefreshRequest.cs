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
*  Create Time : 2026-01-09 01:10:37                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 订阅轮询刷新请求
    /// </summary>
    [XmlRoot("SubscriptionPolledRefresh", Namespace = XmlDaHelper.Namesapce)]
    public class SubscriptionPolledRefreshRequest
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public SubscriptionPolledRefreshRequest()
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
        /// 服务器子句柄
        /// </summary>
        [XmlElement("ServerSubHandles")]
        public List<string> ServerSubHandles { get; set; }
        /// <summary>
        /// 保持时间
        /// </summary>
        [XmlAttribute("HoldTime")]
        public DateTime HoldTime { get; set; }
        /// <summary>
        /// 等待时间
        /// </summary>
        [XmlAttribute("WaitTime")]
        public int WaitTime { get; set; }
        /// <summary>
        /// 返回所有项目
        /// </summary>
        [XmlAttribute("ReturnAllItems")]
        public bool ReturnAllItems { get; set; }
        #endregion

        #region 方法

        #endregion
    }
}