using System;
using System.Collections.Generic;
using System.Text;

/****************************************************************
*  Copyright © (2026) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2026-01-12 00:35:46                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 访问权限
    /// </summary>
    public enum AccessRights
    {
        /// <summary>
        /// 未知
        /// </summary>
        unknown = 0,
        /// <summary>
        /// 可读
        /// </summary>
        readable = 1,
        /// <summary>
        /// 可写
        /// </summary>
        writable = 2,
        /// <summary>
        /// 可读写
        /// </summary>
        readWritable = 3
    }
}