using System;
using System.Collections.Generic;
using System.Text;
using XiaoFeng.OPC.DA;

/****************************************************************
*  Copyright © (2025) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2025-11-09 13:38:43                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.DA
{
    /// <summary>
    /// 连接事件
    /// </summary>
    /// <param name="client">客户端</param>
    public delegate void ConnectEventHandler(DaClient client);
}