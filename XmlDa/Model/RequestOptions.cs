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
*  Create Time : 2026-01-08 19:11:55                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 读取配置
    /// </summary>
    public class RequestOptions
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public RequestOptions()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 返回错误文本
        /// </summary>
        [XmlAttribute("ReturnErrorText")]
        public bool ReturnErrorText { get; set; } = true;
        /// <summary>
        /// 返回诊断信息
        /// </summary>
        [XmlAttribute("ReturnDiagnosticInfo")]
        public bool ReturnDiagnosticInfo { get; set; } = false;
        /// <summary>
        /// 返回项目时间
        /// </summary>
        [XmlAttribute("ReturnItemTime")] 
        public bool ReturnItemTime { get; set; } = true;
        /// <summary>
        /// 返回项目路径
        /// </summary>
        [XmlAttribute("ReturnItemPath")]
        public bool ReturnItemPath { get; set; } = true;
        /// <summary>
        /// 返回项目名称
        /// </summary>
        [XmlAttribute("ReturnItemName")] 
        public bool ReturnTimeName { get; set; } = true;
        /// <summary>
        /// 请求截止日期
        /// </summary>
        [XmlAttribute("RequestDeadline")]
        public DateTime RequestDeadline { get; set; }
        /// <summary>
        /// 区域ID
        /// </summary>
        [XmlAttribute("LocaleID")] 
        public string LocaleID { get; set; }
        /// <summary>
        /// 客户端请求句柄
        /// </summary>
        [XmlAttribute("ClientRequestHandle")]
        public string ClientRequestHandle { get; set; }
        #endregion

        #region 方法

        #region 析构器
        /// <summary>
        /// 析构器
        /// </summary>
        ~RequestOptions()
        {

        }
        #endregion

        #endregion
    }
}