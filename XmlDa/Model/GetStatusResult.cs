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
*  Create Time : 2026-01-08 17:13:31                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 状态结果
    /// </summary>
    public class GetStatusResult
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public GetStatusResult()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 接收时间
        /// </summary>
        [XmlAttribute("RcvTime")]
        public DateTime RcvTime { get; set; }
        /// <summary>
        /// 答复时间
        /// </summary>
        [XmlAttribute("ReplyTime")]
        public DateTime ReplyTime { get; set; }
        /// <summary>
        /// 修订后区域ID
        /// </summary>
        [XmlAttribute("RevisedLocaleID")]
        public string RevisedLocaleID { get; set; }
        /// <summary>
        /// 客户端请求句柄
        /// </summary>
        [XmlAttribute("ClientRequestHandle")]
        public string ClientRequestHandle { get; set; }
        /// <summary>
        /// 服务器状态
        /// </summary>
        [XmlAttribute("ServerState")]
        public string ServerState { get; set; }
        #endregion

        #region 方法

        #region 析构器
        /// <summary>
        /// 析构器
        /// </summary>
        ~GetStatusResult()
        {

        }
        #endregion

        #endregion
    }
}