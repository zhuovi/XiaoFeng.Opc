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
*  Create Time : 2026-01-08 23:10:07                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 读请求项列表
    /// </summary>
    public class ReadRequestItemList
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public ReadRequestItemList()
        {

        }
        #endregion

        #region 属性
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
        /// 最大期限
        /// </summary>
        [XmlAttribute("MaxAge")]
        public int MaxAge { get; set; }
        /// <summary>
        /// 项
        /// </summary>
        [XmlArrayItem("Items")]
        public List<ReadRequestItem> Items { get; set; }
        #endregion

        #region 方法

        #endregion
    }
}