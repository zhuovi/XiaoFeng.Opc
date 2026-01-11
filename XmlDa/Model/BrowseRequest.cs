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
*  Create Time : 2026-01-09 08:35:30                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 浏览请求
    /// </summary>
    [XmlRoot("Browse", Namespace = XmlDaHelper.Namesapce)]
    public class BrowseRequest
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public BrowseRequest()
        {

        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="itemName">项目名称</param>
        /// <param name="itemPath">项目路径</param>
        public BrowseRequest(string itemName, string itemPath = "")
        {
            this.ItemName = itemName;
            this.ItemPath = itemPath;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 属性名称
        /// </summary>
        [XmlArrayItem("PropertyNames")]
        public List<QualifiedName> PropertyNames { get; set; }
        /// <summary>
        /// 地域ID
        /// </summary>
        [XmlAttribute("LocaleID")]
        public string LocaleID { get; set; }
        /// <summary>
        /// 客户端请求句柄
        /// </summary>
        [XmlAttribute("ClientRequestHandle")]
        public string ClientRequestHandle { get; set; }
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
        /// 续传点
        /// </summary>
        [XmlAttribute("ContinuationPoint")]
        public string ContinuationPoint { get; set; }
        /// <summary>
        /// 最大返回节点
        /// </summary>
        [XmlAttribute("MaxElementsReturned")]
        public int MaxElementsReturned { get; set; }
        /// <summary>
        /// 浏览筛选器
        /// </summary>
        [XmlAttribute("BrowseFilter")]
        [XmlConverter(typeof(StringEnumConverter))]
        public BrowseFilter BrowseFilter { get; set; } = BrowseFilter.all;
        /// <summary>
        /// 元素名称筛选器
        /// </summary>
        [XmlAttribute("ElementNameFilter")]
        public string ElementNameFilter { get; set; }
        /// <summary>
        /// 厂家筛选器
        /// </summary>
        [XmlAttribute("VendorFilter")]
        public string VendorFilter { get; set; }
        /// <summary>
        /// 返回所有属性
        /// </summary>
        [XmlAttribute("ReturnAllProperties")]
        public bool ReturnAllProperties { get; set; } = true;
        /// <summary>
        /// 返回所有属性值
        /// </summary>
        [XmlAttribute("ReturnPropertyValues")]
        public bool ReturnPropertyValues { get; set; } = true;
        /// <summary>
        /// 返回错误信息
        /// </summary>
        [XmlAttribute("ReturnErrorText")]
        public bool ReturnErrorText { get; set; } = true;
        #endregion

        #region 方法

        #region 析构器
        /// <summary>
        /// 析构器
        /// </summary>
        ~BrowseRequest()
        {

        }
        #endregion

        #endregion
    }
}