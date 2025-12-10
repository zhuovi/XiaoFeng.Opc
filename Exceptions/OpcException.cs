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
namespace XiaoFeng.OPC
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
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="innerException">导致当前异常的异常</param>
        public OpcException(string message, Exception innerException) : base(message, innerException) { }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="errorCode">错误代码</param>
        /// <param name="innerException">导致当前异常的异常</param>
        public OpcException(string message, int errorCode, Exception innerException) : this(message, innerException)
        {
            this.ErrorCode = errorCode;
        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="errorCode">错误代码</param>
        public OpcException(string message,int errorCode) : base(message)
        {
            this.ErrorCode = errorCode;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorCode { get; }
        #endregion

        #region 方法

        #endregion
    }
}