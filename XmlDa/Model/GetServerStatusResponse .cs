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
*  Create Time : 2026-01-08 10:53:12                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 获取服务器状态响应
    /// </summary>
    [XmlRoot("GetStatusResponse", Namespace = XmlDaHelper.Namesapce)]
    public class GetServerStatusResponse
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public GetServerStatusResponse()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 服务器状态
        /// </summary>
        [XmlElement("Status")]
        public ServerStatus ServerStatus { get; set; }
        /// <summary>
        /// 状态结果
        /// </summary>
        public GetStatusResult GetStatusResult { get; set; }
        /// <summary>
        /// 客户端请求句柄
        /// </summary>
        public string ClientRequestHandle { get; set; }
        /// <summary>
        /// 服务端请求句柄
        /// </summary>
        public string ServerRequestHandle { get; set; }
        #endregion

        #region 方法

        #region 析构器
        /// <summary>
        /// 析构器
        /// </summary>
        ~GetServerStatusResponse()
        {

        }
        #endregion

        #endregion
    }
}