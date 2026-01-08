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
*  Create Time : 2026-01-09 01:02:12                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 订阅请求项列表
    /// </summary>
    public class SubscribeRequestItemList
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public SubscribeRequestItemList()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 项
        /// </summary>
        [XmlArrayItem("Items")]
        public List<SubscribeRequestItem> Items { get; set; }
        /// <summary>
        /// 项目路径
        /// </summary>
        [XmlAttribute("ItemPath")]
        public string ItemPath { get; set; }
        /// <summary>
        /// 需求类型
        /// </summary>
        [XmlAttribute("ReqType")]
        public string ReqType { get; set; }
        /// <summary>
        /// 死区
        /// </summary>
        [XmlAttribute("Deadband")]
        public float Deadband { get; set; }
        /// <summary>
        /// 请求的采样率
        /// </summary>
        [XmlAttribute("RequestedSamplingRate")]
        public int RequestedSamplingRate { get; set; }
        /// <summary>
        /// 启用缓冲
        /// </summary>
        [XmlAttribute("EnableBuffering")]
        public bool EnableBuffering { get; set; }
        #endregion

        #region 方法

        #endregion
    }
}