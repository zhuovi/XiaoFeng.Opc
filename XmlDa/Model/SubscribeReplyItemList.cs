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
*  Create Time : 2026-01-09 00:51:28                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 订阅回复项目列表
    /// </summary>
    public class SubscribeReplyItemList
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public SubscribeReplyItemList()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 订阅项
        /// </summary>
        [XmlArrayItem("Items")]
        public List<SubscribeItemValue> Items { get; set; }
        /// <summary>
        /// 修订采样率
        /// </summary>
        [XmlAttribute("RevisedSamplingRate")]
        public int RevisedSamplingRate { get; set; }
        #endregion

        #region 方法

        #endregion
    }
}