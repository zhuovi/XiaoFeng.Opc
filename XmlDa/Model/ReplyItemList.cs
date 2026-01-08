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
*  Create Time : 2026-01-09 00:07:16                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 回复项目列表
    /// </summary>
    public class ReplyItemList
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public ReplyItemList()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 项目集
        /// </summary>
        [XmlArrayItem("Items")]
        public List<ItemValue> Items { get; set; }
        /// <summary>
        /// 预留
        /// </summary>
        [XmlAttribute("Reserved")]
        public string Reserved { get; set; }
        #endregion

        #region 方法

        #endregion
    }
}