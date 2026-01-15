using System;
using System.Collections.Generic;
using System.Text;
using XiaoFeng.Xml;
using XiaoFeng.Json;
using System.Xml.Serialization;
using System.ComponentModel;
/****************************************************************
*  Copyright © (2026) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2026-01-08 14:19:17                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 响应结果
    /// </summary>
    public class ResponseResult<T> : ResponseResult
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public ResponseResult() : base() { }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="message">消息</param>
        public ResponseResult(string message) : base(message) { }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="status">状态</param>
        public ResponseResult(ResponseStatus status) : base(status) { }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="data">数据</param>
        public ResponseResult(T data)
        {
            this.Status = data == null ? ResponseStatus.Error : ResponseStatus.Success;
            this.Data = data;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; } = default;
        #endregion


    }
    /// <summary>
    /// 响应结果
    /// </summary>
    public class ResponseResult
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public ResponseResult()
        {
            this.Status = ResponseStatus.Error;
        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="message">消息</param>
        public ResponseResult(string message)
        {
            this.Status = ResponseStatus.Error;
            this.Message = message;
        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="status">状态</param>
        public ResponseResult(ResponseStatus status)
        {
            this.Status = status;
            this.Message = status.GetDescription();
        }
        #endregion

        #region 属性
        /// <summary>
        /// 状态
        /// </summary>
        [JsonConverter(typeof(Json.StringEnumConverter))]
        [XmlConverter(typeof(XiaoFeng.Xml.StringEnumConverter))]
        public ResponseStatus Status { get; set; }
        /// <summary>
        /// 错误消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 请求包
        /// </summary>
        [XmlIgnore,JsonIgnore]
        public string RequestXml { get; set; }
        /// <summary>
        /// 响应包
        /// </summary>
        [XmlIgnore, JsonIgnore] 
        public string ResponseXml { get; set; }
        #endregion

        #region 方法

        #region 析构器
        /// <summary>
        /// 析构器
        /// </summary>
        ~ResponseResult()
        {

        }
        #endregion

        #endregion
    }
    /// <summary>
    /// 响应状态
    /// </summary>
    public enum ResponseStatus
    {
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")] 
        Success = 200,
        /// <summary>
        /// 解析出错
        /// </summary>
        [Description("解析出错")]
        Error = 501,
        /// <summary>
        /// 订阅轮询失败
        /// </summary>
        [Description("订阅轮询失败")]
        SubscriptionPolledRefreshFailed = 502,
        /// <summary>
        /// 连接失败
        /// </summary>
        [Description("连接失败")]
        ConnectionFailed = 503,
        /// <summary>
        /// 参数出错
        /// </summary>
        [Description("参数出错")]
        ParameterError = 504,
        /// <summary>
        /// 解析出错
        /// </summary>
        [Description("解析出错")]
        ParseError = 505
    }
}