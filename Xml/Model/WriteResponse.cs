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
*  Create Time : 2025-12-10 23:02:25                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XML.Model
{
    /// <summary>
    /// 写项响应
    /// </summary>
    [XmlRoot("WriteResponse", Namespace = OpcXmlHelper.Namespace)]
    public class WriteResponse
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public WriteResponse()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 标签项响应集
        /// </summary>
        [XmlArray("ItemResults", Namespace = OpcXmlHelper.Namespace), XmlArrayItem("ItemResult")]
        public List<OpcItemResult> ItemResults { get; set; } = new List<OpcItemResult>();
        #endregion

        #region 方法

        #endregion
    }
}