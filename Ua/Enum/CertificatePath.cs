using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

/****************************************************************
*  Copyright © (2025) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2025-06-04 17:01:16                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.Opc
{
    /// <summary>
    /// 证书路径
    /// </summary>
    public enum CertificatePath
    {
        /// <summary>
        /// 申请证书
        /// </summary>
        [Description("申请证书")]
        Application,
        /// <summary>
        /// 可信发卡机构证书
        /// </summary>
        [Description("可信发卡机构证书")]
        TrustedIssuer,
        /// <summary>
        /// 可信对等证书
        /// </summary>
        [Description("可信对等证书")]
        TrustedPeer,
        /// <summary>
        /// 拒绝证书
        /// </summary>
        [Description("拒绝证书")]
        Rejected
    }
}