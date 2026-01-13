using System;
using System.Collections.Generic;
using System.Text;
using XiaoFeng.OPC.XmlDa.Model;

/****************************************************************
*  Copyright © (2026) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2026-01-07 20:31:14                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa
{
    /// <summary>
    /// 帮助类
    /// </summary>
    public class XmlDaHelper
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public XmlDaHelper()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// Soap 命名空间
        /// </summary>
        public const string SoapNamespace = "http://schemas.xmlsoap.org/soap/envelope/";
        /// <summary>
        /// 请求体命名空间
        /// </summary>
        public const string Namesapce = "http://opcfoundation.org/webservices/XMLDA/{0}/";

        #endregion

        #region 方法

        #region 获取 Soap 请求 Action
        /// <summary>
        /// 获取 Soap 请求 Action
        /// </summary>
        /// <param name="soapAction">请求类型</param>
        /// <param name="soapVersion">协议版本</param>
        /// <returns></returns>
        public static string GetSoapAction(SoapAction soapAction, OpcXmlVersion soapVersion= OpcXmlVersion.XmlDa10)
        {
            return $"http://opcfoundation.org/webservices/XMLDA/{(double)soapVersion/10:F1}/{soapAction}";
        }
        #endregion


        #endregion
    }
}