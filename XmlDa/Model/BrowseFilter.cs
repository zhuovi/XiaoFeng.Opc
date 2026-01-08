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
*  Create Time : 2026-01-08 23:51:23                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 浏览筛选器
    /// </summary>
    public enum BrowseFilter
    {
        /// <summary>
        /// 所有
        /// </summary>
        [Description("所有")]
        all,
        /// <summary>
        /// 分支
        /// </summary>
        [Description("分支")] 
        branch,
        /// <summary>
        /// 项目
        /// </summary>
        [Description("项目")] 
        item,
    }
}