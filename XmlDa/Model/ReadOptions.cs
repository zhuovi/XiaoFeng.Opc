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
*  Create Time : 2026-01-08 19:11:55                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// ReadOptions 类说明
    /// </summary>
    public class ReadOptions
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public ReadOptions()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 错误信息
        /// </summary>
        [XmlAttribute("ReturnErrorText")]
        public bool ReturnErrorText { get; set; } = true;
        /// <summary>
        /// 项时间
        /// </summary>
        [XmlAttribute("ReturnItemTime")] 
        public bool ReturnItemTime { get; set; } = true;
        /// <summary>
        /// 项名称
        /// </summary>
        [XmlAttribute("ReturnItemName")] 
        public bool ReturnTimeName { get; set; } = true;
        /// <summary>
        /// 区域ID
        /// </summary>
        [XmlAttribute("LocaleID")] 
        public string LocaleID { get; set; }
        #endregion

        #region 方法

        #region 析构器
        /// <summary>
        /// 析构器
        /// </summary>
        ~ReadOptions()
        {

        }
        #endregion

        #endregion
    }
}