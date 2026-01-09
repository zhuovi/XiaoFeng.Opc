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
*  Create Time : 2026-01-09 08:32:37                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 浏览节点
    /// </summary>
    public class BrowseElement
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public BrowseElement()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 属性
        /// </summary>
        [XmlArrayItem("Properties")]
        public List<ItemProperty> Properties { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [XmlAttribute("Name")]
        public string Name { get; set; }
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
        /// 是否是项目
        /// </summary>
        [XmlAttribute("IsItem")]
        public bool IsItem { get; set; }
        /// <summary>
        /// 是否有子集
        /// </summary>
        [XmlAttribute("HasChildren")]
        public bool HasChildren { get; set; }
        #endregion

        #region 方法

        #region 析构器
        /// <summary>
        /// 析构器
        /// </summary>
        ~BrowseElement()
        {

        }
        #endregion

        #endregion
    }
}