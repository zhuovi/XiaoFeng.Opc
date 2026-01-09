using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using XiaoFeng.Xml;

/****************************************************************
*  Copyright © (2026) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2026-01-09 08:28:59                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 项目属性
    /// </summary>
    public class ItemProperty
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public ItemProperty()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 值
        /// </summary>
        [XmlCData]
        public string Value { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [XmlAttribute("Name")]
        public string Name { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        [XmlAttribute("Description")]
        public string Description { get; set; }
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
        /// 结果ID
        /// </summary>
        [XmlAttribute("ResultID")]
        public QualifiedName ResultID { get; set; }
        #endregion

        #region 方法

        #region 析构器
        /// <summary>
        /// 析构器
        /// </summary>
        ~ItemProperty()
        {

        }
        #endregion

        #endregion
    }
}