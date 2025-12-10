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
*  Create Time : 2025-12-10 21:53:17                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XML.Model
{
    /// <summary>
    /// 标签项类
    /// </summary>
    [XmlType(Namespace = OpcXmlHelper.Namespace)]
    public class OpcItem
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public OpcItem()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 路径
        /// </summary>
        [XmlAttribute]
        public string ItemPath { get; set; } = string.Empty;
        /// <summary>
        /// 名称
        /// </summary>
        [XmlAttribute]
        public string ItemName { get; set; } = string.Empty;
        /// <summary>
        /// 激活状态
        /// </summary>
        [XmlAttribute]
        public bool Active { get; set; } = true;
        #endregion

        #region 方法

        #endregion
    }
}