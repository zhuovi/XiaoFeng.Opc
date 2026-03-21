using System;
using System.Collections.Generic;
using System.Text;

/****************************************************************
*  Copyright © (2026) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2026-03-17 15:08:17                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 类型名称
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class TypeNameAttribute:Attribute
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public TypeNameAttribute() { }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="name">类型名称</param>
        public TypeNameAttribute(string name)
        {
            this.Name = name;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        #endregion

        #region 方法

        #region 析构器
        /// <summary>
        /// 析构器
        /// </summary>
        ~TypeNameAttribute()
        {

        }
        #endregion

        #endregion
    }
}