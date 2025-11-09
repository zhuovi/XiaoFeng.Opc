using System;
using System.Collections.Generic;
using System.Text;

/****************************************************************
*  Copyright © (2025) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2025-11-03 21:30:54                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.DA.Model
{
    /// <summary>
    /// 组
    /// </summary>
    public class Group
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public Group()
        {

        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="name">组名称</param>
        /// <param name="updateRate">更新率</param>
        /// <param name="items">项集</param>
        public Group(string name, int updateRate, List<string> items)
        {
            Name = name;
            UpdateRate = updateRate;
            Items = items;
        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="name">组名称</param>
        /// <param name="updateRate">更新率</param>
        public Group(string name, int updateRate) : this(name, updateRate, new List<string>()) { }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="name">组名称</param>
        /// <param name="items">项集</param>
        public Group(string name, List<string> items) : this(name,1000,items) { }
        #endregion

        #region 属性
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 更新率
        /// </summary>
        public int UpdateRate { get; set; } = 1000;
        /// <summary>
        /// 客户端句柄
        /// </summary>
        public object ClientHandle { get; set; } = Guid.NewGuid().ToString("N");
        /// <summary>
        /// 死区值 默认是0 设为0时，服务器端该组内任何数据变化都通知组
        /// </summary>
        public float Deadband { get; set; } = 0;
        /// <summary>
        /// 项集
        /// </summary>
        public List<string> Items { get; set;  }
        #endregion

        #region 方法
        /// <summary>
        /// 默认值
        /// </summary>
        internal void Default()
        {
            this.Name = "Group 1";
            this.UpdateRate = 1000;
            this.Items = new List<string>();
        }
        #endregion
    }
}