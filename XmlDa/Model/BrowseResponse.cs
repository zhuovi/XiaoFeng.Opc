using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using XiaoFeng.OPC.XmlDa;
using XiaoFeng.OPC.XmlDa.Model;

/****************************************************************
*  Copyright © (2026) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2026-01-09 08:45:00                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 浏览响应
    /// </summary>
    [XmlRoot("BrowseResponse", Namespace = XmlDaHelper.Namesapce)]
    public class BrowseResponse
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public BrowseResponse()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 浏览结果
        /// </summary>
        public ReplyBase BrowseResult { get; set; }
        /// <summary>
        /// 节点
        /// </summary>
        [XmlArrayItem("Elements")]
        public List<BrowseElement> Elements { get; set; }
        /// <summary>
        /// 错误
        /// </summary>
        [XmlArrayItem("Errors")]
        public List<OPCError> Errors { get; set; }
        /// <summary>
        /// 续传点
        /// </summary>
        [XmlAttribute("ContinuationPoint")]
        public string ContinuationPoint { get; set; }
        /// <summary>
        /// 更多节点
        /// </summary>
        [XmlAttribute("MoreElements")]
        public bool MoreElements { get; set; }
        #endregion

        #region 方法

        #region 析构器
        /// <summary>
        /// 析构器
        /// </summary>
        ~BrowseResponse()
        {

        }
        #endregion

        #endregion
    }
}