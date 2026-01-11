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
*  Create Time : 2026-01-09 08:49:25                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 项目标识
    /// </summary>
    public class ItemIdentifier
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public ItemIdentifier()
        {
            
        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="itemName">项目名称</param>
        public ItemIdentifier(string itemName)
        {
            this.ItemName = itemName;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 项目路径
        /// </summary>
        [XmlAttribute("ItemPath")]
        public string ItemPath { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [XmlAttribute("ItemName")]
        public string ItemName { get; set; }
        #endregion

        #region 方法

        #region 析构器
        /// <summary>
        /// 析构器
        /// </summary>
        ~ItemIdentifier()
        {

        }
        #endregion

        #endregion
    }
}