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
*  Create Time : 2026-01-09 00:29:30                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// OPC 错误
    /// </summary>
    public class OPCError
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public OPCError()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// ID
        /// </summary>
        [XmlAttribute("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        [XmlElement("Text")]
        public string Text { get; set; }
        #endregion

        #region 方法

        #endregion
    }
}