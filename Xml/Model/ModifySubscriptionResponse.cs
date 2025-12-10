using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

/****************************************************************
*  Copyright © (2025) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2025-12-10 22:26:31                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XML.Model
{
    /// <summary>
    /// 修改订阅响应
    /// </summary>
    [XmlRoot("ModifySubscriptionResponse", Namespace = OpcXmlHelper.Namespace)]
    public class ModifySubscriptionResponse:CreateSubscriptionResponse
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public ModifySubscriptionResponse()
        {

        }
        #endregion

        #region 属性

        #endregion

        #region 方法

        #endregion
    }
}