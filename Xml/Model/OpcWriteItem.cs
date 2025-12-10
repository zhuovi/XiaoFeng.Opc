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
*  Create Time : 2025-12-10 23:00:55                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XML.Model
{
    /// <summary>
    /// 标签写入值项
    /// </summary>
    [XmlType(Namespace = OpcXmlHelper.Namespace)]
    public class OpcWriteItem:OpcItem
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public OpcWriteItem()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 值
        /// </summary>
        [XmlElement(Namespace = OpcXmlHelper.Namespace)]
        public object Value { get; set; }
        #endregion

        #region 方法

        #endregion
    }
}