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
*  Create Time : 2026-01-09 00:46:49                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 写响应
    /// </summary>
    [XmlRoot("WriteResponse", Namespace = XmlDaHelper.Namesapce)]
    public class WriteResponse
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public WriteResponse()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 读结果
        /// </summary>
        public ReplyBase WriteResult { get; set; }
        /// <summary>
        /// 项列表
        /// </summary>
        public ReplyItemList RItemList { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        [XmlArrayItem("Errors")]
        public List<OPCError> Errors { get; set; }
        #endregion

        #region 方法

        #endregion
    }
}