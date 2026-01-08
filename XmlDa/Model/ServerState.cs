using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

/****************************************************************
*  Copyright © (2026) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2026-01-08 22:47:08                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 服务器状态
    /// </summary>
    public enum ServerState
    {
        /// <summary>
        /// 运行
        /// </summary>
        [Description("运行")]
        running=0,
        /// <summary>
        /// 失败
        /// </summary>
        [Description("失败")]
        failed = 1,
        /// <summary>
        /// 无配置
        /// </summary>
        [Description("无配置")]
        noConfig = 2,
        /// <summary>
        /// 挂起
        /// </summary>
        [Description("挂起")]
        suspended = 3,
        /// <summary>
        /// 测试
        /// </summary>
        [Description("测试")]
        test = 4,
        /// <summary>
        /// 通信故障
        /// </summary>
        [Description("通信故障")]
        commFault = 5
    }
}