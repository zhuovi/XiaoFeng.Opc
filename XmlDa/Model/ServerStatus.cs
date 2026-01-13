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
*  Create Time : 2026-01-08 10:58:33                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 服务器状态
    /// </summary>
    public class ServerStatus
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public ServerStatus()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 服务器名称
        /// </summary>
        public string ServerName { get; set; }
        /// <summary>
        /// 供应商信息
        /// </summary>
        public string VendorInfo { get; set; }
        /// <summary>
        /// 产品版本
        /// </summary>
        [XmlAttribute("ProductVersion")]
        public string ProductVersion { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string StatusInfo { get; set; }
        /// <summary>
        /// 启动时间
        /// </summary>
        [XmlAttribute("StartTime")]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 当前时间
        /// </summary>
        public DateTime CurrentTime { get; set; }
        /// <summary>
        /// 最后重置时间
        /// </summary>
        public DateTime LastResetTime { get; set; }
        /// <summary>
        /// 次要版本
        /// </summary>
        public int MinorVersion { get; set; }
        /// <summary>
        /// 主要版本
        /// </summary>
        public int MajorVersion { get; set; }
        /// <summary>
        /// 支持区域ID
        /// </summary>
        [XmlArrayItem("SupportedLocaleIDs")]
        public List<string> SupportedLocaleIDs { get; set; }
        /// <summary>
        /// 支持接口版本
        /// </summary>
        [XmlArrayItem("SupportedInterfaceVersions")]
        public List<string> SupportedInterfaceVersions { get; set; }
        #endregion

        #region 方法

        #region 析构器
        /// <summary>
        /// 析构器
        /// </summary>
        ~ServerStatus()
        {

        }
        #endregion

        #endregion
    }
}