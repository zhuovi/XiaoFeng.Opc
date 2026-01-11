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
*  Create Time : 2026-01-07 23:06:29                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 获取请求状态
    /// </summary>
    [XmlRoot("GetStatus",Namespace = XmlDaHelper.Namesapce)]
    public class GetServerStatusRequest
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public GetServerStatusRequest()
        {

        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="localeId">区域ID</param>
        /// <param name="clientRequestHandle">客户端请求句柄</param>
        public GetServerStatusRequest(string localeId,string clientRequestHandle)
        {
            this.LocaleID = localeId;
            this.ClientRequestHandle = clientRequestHandle;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 区域ID
        /// </summary>
        [XmlAttribute("LocaleID")]
        public string LocaleID { get; set; } = "en-US";
        /// <summary>
        /// 客户端请求句柄
        /// </summary>
        [XmlAttribute("ClientRequestHandle")]
        public string ClientRequestHandle { get; set; } = Guid.NewGuid().ToString("N");
        #endregion

        #region 方法

        #endregion
    }
}