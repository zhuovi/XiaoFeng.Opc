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
*  Create Time : 2025-12-10 21:32:12                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XML.Model
{
    /// <summary>
    /// 请求基础类
    /// </summary>
    public abstract class BaseRequest
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public BaseRequest()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 区域
        /// </summary>
        [XmlElement(Namespace = OpcXmlHelper.Namespace)]
        public string LocaleID { get; set; } = "en-US";
        /// <summary>
        /// 客户端请求句柄
        /// </summary>
        [XmlElement(Namespace = OpcXmlHelper.Namespace)]
        public string ClientRequestHandle { get; set; } = Guid.NewGuid().ToString("N");
        #endregion

        #region 方法

        #endregion
    }
}