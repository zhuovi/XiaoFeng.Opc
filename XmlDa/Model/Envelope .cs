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
*  Create Time : 2026-01-07 23:00:05                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 基础模型
    /// </summary>
    [XmlRoot("Envelope",Namespace = XmlDaHelper.SoapNamespace)]
    public class Envelope<T> where T:class
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public Envelope()
        {
            this.Namespaces = new XmlSerializerNamespaces();
            this.Namespaces.Add("soap", XmlDaHelper.SoapNamespace);
            this.Namespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            this.Namespaces.Add("xsd", "http://www.w3.org/2001/XMLSchema");
        }
        #endregion

        #region 属性
        /// <summary>
        /// 命名空间
        /// </summary>
        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces Namespaces { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        [XmlElement("Body",Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public SoapBody<T> Body { get; set; }
        #endregion

        #region 方法

        #endregion
    }
}