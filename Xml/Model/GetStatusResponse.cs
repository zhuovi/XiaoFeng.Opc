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
*  Create Time : 2025-12-10 22:53:10                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XML.Model
{
    /// <summary>
    /// 获取服务状态响应
    /// </summary>
    [XmlRoot("GetStatusResponse", Namespace = OpcXmlHelper.Namespace)]
    public class GetStatusResponse:BaseResponse
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public GetStatusResponse()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 服务状态
        /// </summary>
        [XmlElement(Namespace = OpcXmlHelper.Namespace)]
        public string ServerState { get; set; } = string.Empty;
        /// <summary>
        /// 服务信息
        /// </summary>
        [XmlElement(Namespace = OpcXmlHelper.Namespace)]
        public string VendorInfo { get; set; } = string.Empty;
        /// <summary>
        /// 产品版本
        /// </summary>
        [XmlElement(Namespace = OpcXmlHelper.Namespace)]
        public string ProductVersion { get; set; } = string.Empty;
        #endregion

        #region 方法

        #endregion
    }
}