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
*  Create Time : 2025-06-25 11:13:15                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.Opc
{
    /// <summary>
    /// 连接事件
    /// </summary>
    /// <param name="session">会话</param>
    public delegate void ConnectEventHandler(OpcConnectEventArgs e);
    
}