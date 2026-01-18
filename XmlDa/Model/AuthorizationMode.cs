using System;
using System.Collections.Generic;
using System.Text;

/****************************************************************
*  Copyright © (2026) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2026-01-17 20:28:44                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 认证模式
    /// </summary>
    public enum AuthorizationMode
    {
        /// <summary>
        /// 基础认证
        /// </summary>
        Basic,
        /// <summary>
        /// Bearer Token
        /// </summary>
        Bearer,
        /// <summary>
        /// 账号和密码
        /// </summary>
        AccountAndPassword,
        /// <summary>
        /// 头认证
        /// </summary>
        Header
    }
}