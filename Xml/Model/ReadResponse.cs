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
*  Create Time : 2025-12-10 22:57:39                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XML.Model
{
    /// <summary>
    /// 读取响应
    /// </summary>
    [XmlRoot("ReadResponse", Namespace = OpcXmlHelper.Namespace)]
    public class ReadResponse:BaseResponse
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public ReadResponse()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 标签项列结果集
        /// </summary>
        [XmlArray("ItemResults", Namespace = OpcXmlHelper.Namespace), XmlArrayItem("ItemResult")]
        public List<OpcItemResult> ItemResults { get; set; } = new List<OpcItemResult>();
        #endregion

        #region 方法

        #endregion
    }
}