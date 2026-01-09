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
*  Create Time : 2026-01-09 08:56:21                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 获取属性请求
    /// </summary>
    [XmlRoot("GetProperties", Namespace = XmlDaHelper.Namesapce)]
    public class GetPropertiesRequest
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public GetPropertiesRequest()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 项目标识
        /// </summary>
        [XmlArrayItem("ItemIDs")]
        public List<ItemIdentifier> ItemIDs { get; set; }
        /// <summary>
        /// 所属属性名称
        /// </summary>
        [XmlArrayItem("PropertyNames")]
        public List<string> PropertyNames { get; set; }
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
        /// 返回所有属性
        /// </summary>
        [XmlAttribute("ReturnAllProperties")]
        public bool ReturnAllProperties { get; set; }
        /// <summary>
        /// 返回所有属性值
        /// </summary>
        [XmlAttribute("ReturnPropertyValues")]
        public bool ReturnPropertyValues { get; set; }
        /// <summary>
        /// 返回错误信息
        /// </summary>
        [XmlAttribute("ReturnErrorText")]
        public bool ReturnErrorText { get; set; }
        #endregion

        #region 方法

        #region 析构器
        /// <summary>
        /// 析构器
        /// </summary>
        ~GetPropertiesRequest()
        {

        }
        #endregion

        #endregion
    }
}