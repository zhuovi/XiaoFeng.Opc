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
*  Create Time : 2025-06-24 16:04:01                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.Opc.Model
{
    /// <summary>
    /// 删除节点
    /// </summary>
    public class DeleteNode
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public DeleteNode()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 节点ID
        /// </summary>
        public NodeValueId NodeValueId { get; set; }
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
        ~DeleteNode()
        {

        }
        #endregion
        #endregion
    }
}