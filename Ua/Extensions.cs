using Opc.Ua;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XiaoFeng.Opc.Model;

/****************************************************************
*  Copyright © (2025) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2025-06-26 8:40:48                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.Opc
{
    /// <summary>
    /// OPC扩展
    /// </summary>
    public static class Extensions
    {
        #region 属性

        #endregion

        #region 方法
        /// <summary>
        /// <see cref="NodeId"/> 转换为 <see cref="NodeValueId"/>
        /// </summary>
        /// <param name="nodeId">NodeId 节点</param>
        /// <returns>返回一个 <see cref="NodeValueId"/></returns>
        public static NodeValueId ParseNodeValueId(this NodeId nodeId)
        {
            return (NodeValueId)nodeId;
        }
        /// <summary>
        /// <see cref="NodeValueId"/> 转换为 <see cref="NodeId"/>
        /// </summary>
        /// <param name="nodeValueId">NodeValueId 节点</param>
        /// <returns>返回一个 <see cref="NodeId"/></returns>
        public static NodeId ParseNodeId(this NodeValueId nodeValueId)
        {
            return (NodeId)nodeValueId;
        }
        #endregion
    }
}