using System;
using System.Collections.Generic;
using System.Text;

/****************************************************************
*  Copyright © (2025) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2025-06-04 18:02:43                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.Opc
{
    /// <summary>
    /// OPC错误消息
    /// </summary>
    public class OpcException : Exception
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public OpcException() : base() { }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="message">错误消息</param>
        public OpcException(string message) : base(message) { }
        #endregion

        #region 属性

        #endregion

        #region 方法
        
        #endregion
    }
}