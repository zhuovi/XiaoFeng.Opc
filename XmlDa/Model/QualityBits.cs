using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

/****************************************************************
*  Copyright © (2026) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2026-01-08 23:41:09                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 质量
    /// </summary>
    public enum QualityBits
    {
        /// <summary>
        /// 坏
        /// </summary>
        [Description("坏")]
        bad,
        /// <summary>
        /// 配置错误
        /// </summary>
        [Description("配置错误")] 
        badConfigurationError,
        /// <summary>
        /// 未连接
        /// </summary>
        [Description("未连接")] 
        badNotConnected,
        /// <summary>
        /// 设备故障
        /// </summary>
        [Description("设备故障")] 
        badDeviceFailure,
        /// <summary>
        /// 传感器故障
        /// </summary>
        [Description("传感器故障")] 
        badSensorFailure,
        /// <summary>
        /// 坏最后已知值
        /// </summary>
        [Description("坏最后已知值")] 
        badLastKnownValue,
        /// <summary>
        /// 通信故障
        /// </summary>
        [Description("通信故障")] 
        badCommFailure,
        /// <summary>
        /// 服务中断
        /// </summary>
        [Description("服务中断")] 
        badOutOfService,
        /// <summary>
        /// 等待初始化数据错误
        /// </summary>
        [Description("等待初始化数据错误")] 
        badWaitingForInitialData,
        /// <summary>
        /// 不确定
        /// </summary>
        [Description("不确定")] 
        uncertain,
        /// <summary>
        /// 不确定持久可用值
        /// </summary>
        [Description("不确定持久可用值")] 
        uncertainLastUsableValue,
        /// <summary>
        /// 不确定性传感器不准确
        /// </summary>
        [Description("不确定性传感器不准确")] 
        uncertainSensorNotAccurate,
        /// <summary>
        /// 超出不确定性
        /// </summary>
        [Description("超出不确定性")] 
        uncertainEUExceeded,
        /// <summary>
        /// 不确定亚正常
        /// </summary>
        [Description("不确定亚正常")] 
        uncertainSubNormal,
        /// <summary>
        /// 好
        /// </summary>
        [Description("好")] 
        good,
        /// <summary>
        /// 良好的本地覆盖
        /// </summary>
        [Description("良好的本地覆盖")] 
        goodLocalOverride,
    }
}