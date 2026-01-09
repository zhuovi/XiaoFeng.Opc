using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

/****************************************************************
*  Copyright © (2026) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2026-01-09 08:53:08                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 获取属性响应
    /// </summary>
    [XmlRoot("GetPropertiesResponse", Namespace = XmlDaHelper.Namesapce)]
    public class GetPropertiesResponse
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public GetPropertiesResponse()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 属性结果
        /// </summary>
        public ReplyBase GetPropertiesResult { get; set; }
        /// <summary>
        /// 属性列表
        /// </summary>
        [XmlArrayItem("PropertyLists")]
        public List<PropertyReplyList> PropertyLists { get; set; }
        /// <summary>
        /// 错误列表
        /// </summary>
        [XmlArrayItem("Errors")]
        public List<OPCError> Errors { get; set; }
        #endregion

        #region 方法

        #region 析构器
        /// <summary>
        /// 析构器
        /// </summary>
        ~GetPropertiesResponse()
        {

        }
        #endregion

        #endregion
    }
}