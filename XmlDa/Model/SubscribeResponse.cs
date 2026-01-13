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
*  Create Time : 2026-01-09 01:07:42                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 订阅响应
    /// </summary>
    [XmlRoot("SubscribeResponse", Namespace = XmlDaHelper.Namesapce)]
    public class SubscribeResponse
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public SubscribeResponse()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 服务器订阅句柄
        /// </summary>
        [XmlAttribute]
        public string ServerSubHandle { get; set; }
        /// <summary>
        /// 读结果
        /// </summary>
        public ReplyBase SubscribeResult { get; set; }
        /// <summary>
        /// 项列表
        /// </summary>
        public SubscribeReplyItemList RItemList { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        [XmlArrayItem("Errors")]
        public List<OPCError> Errors { get; set; }
        #endregion

        #region 方法

        #endregion
    }
}