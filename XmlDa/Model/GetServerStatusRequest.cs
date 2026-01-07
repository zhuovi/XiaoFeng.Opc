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
    /// GetServerStatusRequest 类说明
    /// </summary>
    [XmlRoot("GetServerStatus",Namespace = "http://opcfoundation.org/XMLDA/1.0/")]
    public class GetServerStatusRequest
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public GetServerStatusRequest()
        {

        }
        #endregion

        #region 属性
        public string LocaleID { get; set; } = "en-US";
        public string ClientRequestHandle { get; set; } = Guid.NewGuid().ToString("N");
        #endregion

        #region 方法

        #endregion
    }
}