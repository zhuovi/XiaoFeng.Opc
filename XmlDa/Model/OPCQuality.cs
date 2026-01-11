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
*  Create Time : 2026-01-08 23:53:48                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// OPC质量
    /// </summary>
    public class OPCQuality
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public OPCQuality()
        {

        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="quality">质量</param>
        public OPCQuality(QualityBits quality)
        {
            this.QualityField = quality;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 质量
        /// </summary>
        [XmlAttribute("QualityField")]
        public QualityBits QualityField { get; set; } = QualityBits.good;
        /// <summary>
        /// 限制
        /// </summary>
        [XmlAttribute("LimitField")] 
        public LimitBits LimitField { get; set; } = LimitBits.none;
        /// <summary>
        /// 供应
        /// </summary>
        [XmlAttribute("VendorField")] 
        public SByte VendorField { get; set; }
        #endregion

        #region 方法

        #endregion
    }
}