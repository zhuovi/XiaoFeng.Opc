using Opc;
using Opc.Da;
using System;
using System.Collections.Generic;
using System.Text;

/****************************************************************
*  Copyright © (2025) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2025-11-03 21:40:38                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.DA.Model
{
    /// <summary>
    /// 服务说明
    /// </summary>
    public class ServerHost
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public ServerHost()
        {

        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="name">服务名称</param>
        /// <param name="url">服务地址</param>
        public ServerHost(string name,URL url)
        {
            Name = name;
            Url = url;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 服务名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 服务地址
        /// </summary>
        public URL Url {  get; set; }

        #endregion

        #region 方法

        #endregion
    }
}