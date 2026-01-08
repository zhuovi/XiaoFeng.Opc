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
*  Create Time : 2026-01-09 00:42:53                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 写请求
    /// </summary>
    [XmlRoot("Write", Namespace = XmlDaHelper.Namesapce)]
    public class WriteRequest
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public WriteRequest()
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
        public WriteRequestItemList ItemList { get; set; }
        /// <summary>
        /// 回复时返回值
        /// </summary>
        [XmlAttribute("ReturnValuesOnReply")]
        public bool ReturnValuesOnReply { get; set; }
        #endregion

        #region 方法

        #endregion
    }
}