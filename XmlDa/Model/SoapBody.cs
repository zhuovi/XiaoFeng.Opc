using System;
using System.Collections.Generic;
using System.Text;

/****************************************************************
*  Copyright © (2026) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2026-01-07 23:04:40                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// SoapBody 类说明
    /// </summary>
    public class SoapBody<T> where T : class
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public SoapBody()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// Body值
        /// </summary>
        public T Value { get; set; }
        #endregion

        #region 方法

        #endregion
    }
}