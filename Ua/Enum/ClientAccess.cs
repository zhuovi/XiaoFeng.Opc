using System;
using System.Collections.Generic;
using System.Text;

/****************************************************************
*  Copyright © (2025) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2025-06-04 15:30:10                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.Opc
{
    /// <summary>
    /// 客户端权限
    /// </summary>
    public enum ClientAccess
    {
        /// <summary>
        /// 只读
        /// </summary>
       OnlyRead,
       /// <summary>
       /// 读写
       /// </summary>
       ReadAndWrite
    }
}