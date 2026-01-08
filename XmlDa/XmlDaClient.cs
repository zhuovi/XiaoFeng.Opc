using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XiaoFeng.Http;
using XiaoFeng.OPC.XmlDa.Model;

/****************************************************************
*  Copyright © (2026) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2026-01-08 10:44:32                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa
{
    /// <summary>
    /// 请求客户端
    /// </summary>
    public class XmlDaClient
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public XmlDaClient()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 区域ID
        /// </summary>
        public string LocaleID { get; set; } = "en-US";
        /// <summary>
        /// 客户端请求句柄
        /// </summary>
        public string ClientRequestHandle { get; set; } = Guid.NewGuid().ToString("N");
        /// <summary>
        /// Soap 协议版本
        /// </summary>
        public OpcXmlVersion OpcXmlVersion { get; set; } = OpcXmlVersion.XmlDa10;
        /// <summary>
        /// 服务器地址
        /// </summary>
        public Uri ServerAddress { get; set; }
        #endregion

        #region 方法

        #region 获取服务器状态
        /// <summary>
        /// 获取服务器状态
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseResult<ServerStatus>> GetServerStatusAsync()
        {
            var model = new Envelope<GetServerStatusRequest>
            {
                Body = new SoapBody<GetServerStatusRequest>
                {
                    Value = new GetServerStatusRequest
                    {
                        LocaleID = this.LocaleID,
                        ClientRequestHandle = this.ClientRequestHandle
                    }
                }
            };
            return await this.ExecuteAsync(SoapAction.GetStatus, model, html =>
            {
                var entity = html.XmlToEntity<Envelope<GetServerStatusResponse>>();
                if (entity != null && entity.Body?.Value?.ServerStatus != null)
                {
                    return entity.Body?.Value?.ServerStatus;
                }
                return null;
            }).ConfigureAwait(false);
        }
        #endregion

        #region 读项

        #endregion

        #region 执行请求响应
        /// <summary>
        /// 执行请求响应
        /// </summary>
        /// <typeparam name="T">请求类型</typeparam>
        /// <typeparam name="T1">响应类型</typeparam>
        /// <param name="soapAction">请求Action</param>
        /// <param name="requestBody">请求数据</param>
        /// <param name="func">处理方法</param>
        /// <returns></returns>
        internal async Task<ResponseResult<T1>> ExecuteAsync<T, T1>(SoapAction soapAction, T requestBody, Func<string, T1> func)
        {
            var result = new ResponseResult<T1>();
            result.Status = ResponseStatus.Error;
            if (this.ServerAddress.IsNullOrEmpty())
            {
                result.Message = "服务器地址出错.";
                return result;
            }
            var http = new HttpRequest(this.ServerAddress.ToString())
            {
                Method = HttpMethod.Post,
                BodyData = requestBody.EntityToXml().format(((double)this.OpcXmlVersion / 10).ToString("F1"))
            };
            result.RequestXml = http.BodyData;
            http.AddHeader("SOAPAction", XmlDaHelper.GetSoapAction(soapAction, this.OpcXmlVersion));
            var response = await http.GetResponseAsync().ConfigureAwait(false);
            result.ResponseXml = response.Html;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (response.Html.IsXml())
                {
                    result.Data = func.Invoke(response.Html);
                    if (result.Data != null) result.Status = ResponseStatus.Success;
                    return result;
                }
                result.Message = "响应格式不正确.";
                return result;
            }
            result.Message = "响应出错";
            return result;
        }
        #endregion

        #region 析构器
        /// <summary>
        /// 析构器
        /// </summary>
        ~XmlDaClient()
        {

        }
        #endregion

        #endregion
    }
}