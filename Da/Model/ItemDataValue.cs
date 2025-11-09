using System;
using System.Collections.Generic;
using System.Text;

/****************************************************************
*  Copyright © (2025) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2025-11-03 21:39:09                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.DA.Model
{
    /// <summary>
    /// 项数据值
    /// </summary>
    public class ItemDataValue
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public ItemDataValue()
        {

        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="name">项名称</param>
        /// <param name="value">项值</param>
        public ItemDataValue(string name, object value)
        {
            Name = name;
            Value = value;
        }

        #endregion

        #region 属性
        /// <summary>
        /// 项名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 项值
        /// </summary>
        public object Value { get; set; }
        #endregion

        #region 方法

        #endregion
    }
}