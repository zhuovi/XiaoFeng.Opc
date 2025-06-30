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
*  Create Time : 2025-06-24 14:25:15                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.Opc.Model
{
    /// <summary>
    /// 写节点数据
    /// </summary>
    public class WriteNode
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public WriteNode()
        {

        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="nodeValueId">节点Id</param>
        /// <param name="value">值</param>
        public WriteNode(NodeValueId nodeValueId, object value)
        {
            this.NodeValueId = nodeValueId;
            this.Value = value;
        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="namespaceIndex">空间索引</param>
        /// <param name="value">值</param>
        public WriteNode(string address,ushort namespaceIndex,object value)
        {
            this.NodeValueId =new NodeValueId(address,namespaceIndex);
            this.Value= value;
        }

        #endregion

        #region 属性
        /// <summary>
        /// 节点ID
        /// </summary>
        public NodeValueId NodeValueId { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }
        /// <summary>
        /// 写入状态
        /// </summary>
        public StatusCode StatusCode { get; set; }
        #endregion

        #region 方法
        #region 析构器
        /// <summary>
        /// 析构器
        /// </summary>
        ~WriteNode()
        {

        }
        #endregion
        #endregion
    }
}