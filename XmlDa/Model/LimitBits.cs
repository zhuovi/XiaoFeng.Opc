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
*  Create Time : 2026-01-08 23:48:52                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 限制位
    /// </summary>
    public enum LimitBits
    {
        /// <summary>
        /// 无
        /// </summary>
        [Description("无")]
        none,
        /// <summary>
        /// 低
        /// </summary>
        [Description("低")] 
        low,
        /// <summary>
        /// 高
        /// </summary>
        [Description("高")] 
        high,
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")] 
        constant,
    }
}