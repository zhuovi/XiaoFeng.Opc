using Opc.Ua.Client;
using System;
using System.Collections.Generic;
using System.Text;

/****************************************************************
*  Copyright © (2025) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2025-06-25 11:17:26                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.Opc
{
    /// <summary>
    /// 连接事件
    /// </summary>
    public class OpcConnectEventArgs:EventArgs
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public OpcConnectEventArgs()
        {

        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="session">会话</param>
        /// <param name="message">消息</param>
        public OpcConnectEventArgs(ISession session, string message)
        {
            Session = session;
            Message = message;
        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="message">消息</param>
        public OpcConnectEventArgs(string message)
        {
            this.Message = message;
        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="session">会话</param>
        public OpcConnectEventArgs(ISession session)
        {
            this.Session = session;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 会话
        /// </summary>
        public ISession Session { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccessed
        {
            get
            {
                if (this.Session == null) return false;
                return this.Session.Connected;
            }
        }
        #endregion

        #region 方法
        #region 析构器
        /// <summary>
        /// 析构器
        /// </summary>
        ~OpcConnectEventArgs()
        {

        }
        #endregion
        #endregion
    }
}