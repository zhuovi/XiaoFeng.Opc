using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using XiaoFeng.OPC.XML;

/****************************************************************
*  Copyright © (2026) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2026-01-08 23:59:48                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 项目值
    /// </summary>
    public class ItemValue
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public ItemValue()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 诊断信息
        /// </summary>
        public string DiagnosticInfo { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        [XmlElement("Value")]
        public OpcValue Value { get; set; }
        /// <summary>
        /// 质量
        /// </summary>
        public OPCQuality Quality { get; set; }
        /// <summary>
        /// 值类型限定符
        /// </summary>
        [XmlAttribute("ValueTypeQualifier")]
        public string ValueTypeQualifier { get; set; }
        /// <summary>
        /// 项目路径
        /// </summary>
        [XmlAttribute("ItemPath")]
        public string ItemPath { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [XmlAttribute("ItemName")]
        public string ItemName { get; set; }
        /// <summary>
        /// 客户端项目句柄
        /// </summary>
        [XmlAttribute("ClientItemHandle")]
        public string ClientItemHandle { get; set; }
        /// <summary>
        /// 结果ID
        /// </summary>
        [XmlAttribute("ResultID")]
        public string ResultID { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        [XmlAttribute("Timestamp")]
        public DateTime Timestamp { get; set; } = DateTime.Now;
        #endregion

        #region 方法

        #endregion
    }
}