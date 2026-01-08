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
*  Create Time : 2026-01-09 00:40:21                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 写 请求项目列表
    /// </summary>
    public class WriteRequestItemList
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public WriteRequestItemList()
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
        /// 项
        /// </summary>
        [XmlArrayItem("Items")]
        public List<ItemValue> Items { get; set; }
        #endregion

        #region 方法

        #endregion
    }
}