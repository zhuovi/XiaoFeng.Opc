using Opc.Ua;
using System;
using System.Collections.Generic;
using System.Text;

/****************************************************************
*  Copyright © (2025) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2025-06-17 15:47:01                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.Opc.Model
{
    /// <summary>
    ///节点模型
    /// </summary>
    public class NodeValue
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public NodeValue()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 节点 ID
        /// </summary>
        public NodeValueId NodeId { get; set; }
        /// <summary>
        /// 节点类型
        /// </summary>
        public NodeClass NodeClass { get; set; }
        /// <summary>
        /// 浏览名称
        /// </summary>
        public string BrowseName { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 节点说明
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 节点值
        /// </summary>
        public object Value { get; set; }
        /// <summary>
        /// 子节点
        /// </summary>
        public List<NodeValue> ChildNodes { get; set; }
        #endregion

        #region 方法
        ///<inheritdoc/>
        public override string ToString()
        {
            return $"name={this.BrowseName.IfEmpty(this.DisplayName)};value={this.Value.getValue()};";
        }

        #region 析构器
        /// <summary>
        /// 析构器
        /// </summary>
        ~NodeValue()
        {

        }
        #endregion
        #endregion
    }
}