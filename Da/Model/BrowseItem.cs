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
    public class BrowseItem
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
        /// <param name="id">项Id</param>
        /// <param name="hasChildren">是否有子集</param>
        public BrowseItem(string name, string id, bool hasChildren)
        {
            Name = name;
            Id = id;
            HasChildren = hasChildren;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 是否是子集
        /// </summary>
        public Boolean HasChildren { get; set; }
        #endregion

        #region 方法

        #endregion
    }
}