using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using XiaoFeng.OPC.XmlDa;

/****************************************************************
*  Copyright © (2026) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2026-01-08 19:11:09                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// ReadRequest 类说明
    /// </summary>
    [XmlRoot("Read", Namespace = XmlDaHelper.Namesapce)]
    public class ReadRequest
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public ReadRequest()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 读配置
        /// </summary>
        [XmlElement("Options")]
        public ReadOptions Options { get; set; }
        /// <summary>
        /// 项列表
        /// </summary>
        [XmlArray("ItemList")]
        [XmlArrayItem("Items")]
        public List<ReadItem> ItemList { get; set; }
        #endregion

        #region 方法

        #region 析构器
        /// <summary>
        /// 析构器
        /// </summary>
        ~ReadRequest()
        {

        }
        #endregion

        #endregion
    }
}