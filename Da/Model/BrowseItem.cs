using Opc.Da;
using System;
using System.Collections.Generic;
using System.Text;

/****************************************************************
*  Copyright © (2025) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2025-11-03 21:26:44                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.DA.Model
{
    /// <summary>
    /// 浏览项
    /// </summary>
    public class BrowseItem:BrowseElement
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public BrowseItem() { }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="name">项名称</param>
        /// <param name="itemName">项Id</param>
        /// <param name="itemPath">项路径</param>
        public BrowseItem(string name, string itemName, string itemPath)
        {
            Name = name;
            ItemName = itemName;
            ItemPath = itemPath;
            IsItem = true;
            HasChildren = false;
            Children = null;
        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="name">项名称</param>
        /// <param name="itemName">项Id</param>
        /// <param name="itemPath">项路径</param>
        /// <param name="children">子集</param>
        public BrowseItem(string name, string itemName, string itemPath, List<BrowseItem> children)
        {
            Name = name;
            ItemName = itemName;
            ItemPath = itemPath;
            IsItem = false;
            HasChildren = !(children == null || children.Count == 0);
            Children = children;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 子节点
        /// </summary>
        public List<BrowseItem> Children { get; set; }
        #endregion

        #region 方法

        #endregion
    }
}