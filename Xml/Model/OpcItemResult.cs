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
*  Create Time : 2025-12-10 21:57:18                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XML.Model
{
    /// <summary>
    /// 标签项结果
    /// </summary>
    [XmlType(Namespace = OpcXmlHelper.Namespace)]
    public class OpcItemResult
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public OpcItemResult()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 路径
        /// </summary>
        [XmlElement(Namespace = OpcXmlHelper.Namespace)]
        public string ItemPath { get; set; } = string.Empty;
        /// <summary>
        /// 名称
        /// </summary>
        [XmlElement(Namespace = OpcXmlHelper.Namespace)]
        public string ItemName { get; set; } = string.Empty;
        /// <summary>
        /// 值
        /// </summary>
        [XmlElement(Namespace = OpcXmlHelper.Namespace)]
        public object Value { get; set; }
        /// <summary>
        /// 品质
        /// </summary>
        [XmlElement(Namespace = OpcXmlHelper.Namespace)]
        public string Quality { get; set; } = string.Empty;
        /// <summary>
        /// 时间
        /// </summary>
        [XmlElement(Namespace = OpcXmlHelper.Namespace)]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// 响应码
        /// </summary>
        [XmlElement(Namespace = OpcXmlHelper.Namespace)]
        public int ResultCode { get; set; }
        #endregion

        #region 方法

        #endregion
    }
}