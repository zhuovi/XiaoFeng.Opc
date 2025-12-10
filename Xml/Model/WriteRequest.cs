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
*  Create Time : 2025-12-10 22:59:06                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XML.Model
{
    /// <summary>
    /// 写请求
    /// </summary>
    [XmlRoot("Write", Namespace = OpcXmlHelper.Namespace)]
    public class WriteRequest:BaseRequest
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public WriteRequest()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 标签列表集
        /// </summary>
        [XmlArray("Items", Namespace = OpcXmlHelper.Namespace), XmlArrayItem("Item")]
        public List<OpcWriteItem> Items { get; set; } = new List<OpcWriteItem>();
        #endregion

        #region 方法

        #endregion
    }
}