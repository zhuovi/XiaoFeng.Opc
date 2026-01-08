using System;
using System.Collections.Generic;
using System.Text;

/****************************************************************
*  Copyright © (2026) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2026-01-08 10:13:03                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 请求Action
    /// </summary>
    public enum SoapAction
    {
        /// <summary>
        /// 服务器状态
        /// </summary>
        GetStatus = 0,
        /// <summary>
        /// 读取项目
        /// </summary>
        Read = 1,
        /// <summary>
        /// 写项目
        /// </summary>
        Write = 2,
        /// <summary>
        /// 订阅
        /// </summary>
        Subscribe = 3,
        /// <summary>
        /// 订阅轮询刷新
        /// </summary>
        SubscriptionPolledRefresh = 4,
        /// <summary>
        /// 取消订阅
        /// </summary>
        SubscriptionCancel = 5,
        /// <summary>
        /// 浏览
        /// </summary>
        Browse = 6,
        /// <summary>
        /// 获取所有属性
        /// </summary>
        GetProperties = 7
    }
}