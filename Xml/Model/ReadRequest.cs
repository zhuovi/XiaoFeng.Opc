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
*  Create Time : 2025-12-10 22:55:10                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XML.Model
{
    /// <summary>
    /// 读取请求
    /// </summary>
    [XmlRoot("Read", Namespace = OpcXmlHelper.Namespace)]
    public class ReadRequest:BaseRequest
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public ReadRequest()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 是否返回标签项时间
        /// </summary>
        [XmlElement(Namespace = OpcXmlHelper.Namespace)]
        public bool ReturnItemTime { get; set; } = true;
        /// <summary>
        /// 是否返回标签项质量
        /// </summary>
        [XmlElement(Namespace = OpcXmlHelper.Namespace)]
        public bool ReturnItemQuality { get; set; } = true;
        /// <summary>
        /// 标签列表
        /// </summary>
        [XmlArray("Items", Namespace = OpcXmlHelper.Namespace), XmlArrayItem("Item")]
        public List<OpcItem> Items { get; set; } = new List<OpcItem>();
        #endregion

        #region 方法

        #endregion
    }
}