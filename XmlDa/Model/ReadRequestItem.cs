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
*  Create Time : 2026-01-08 23:16:51                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 读取请求项
    /// </summary>
    public class ReadRequestItem
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public ReadRequestItem()
        {

        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="itemName">项目名称</param>
        /// <param name="itemPath">项目路径</param>
        /// <param name="reqType">需求类型</param>
        /// <param name="maxAge">最大期限</param>
        /// <param name="clientItemHandle">客户端项目句柄</param>
        public ReadRequestItem(string itemName, string itemPath, string reqType, int maxAge, string clientItemHandle)
        {
            ItemName = itemName;
            ItemPath = itemPath;
            ReqType = reqType;
            MaxAge = maxAge;
            ClientItemHandle = clientItemHandle;
        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="itemName">项目名称</param>
        public ReadRequestItem(string itemName)
        {
            ItemName = itemName;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 项目路径
        /// </summary>
        [XmlAttribute("ItemPath")]
        public string ItemPath { get; set; }
        /// <summary>
        /// 需求类型
        /// </summary>
        [XmlAttribute("ReqType")]
        public string ReqType { get; set; }
        /// <summary>
        /// 最大期限
        /// </summary>
        [XmlAttribute("MaxAge")]
        public int MaxAge { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [XmlAttribute("ItemName")]
        public string ItemName { get; set; }
        /// <summary>
        /// 客户端项目句柄
        /// </summary>
        [XmlAttribute("ClientItemHandle")]
        public string ClientItemHandle { get; set; }
        #endregion

        #region 方法

        #endregion
    }
}