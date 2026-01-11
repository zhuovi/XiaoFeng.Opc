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
            var request = new Envelope<GetServerStatusRequest>
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
            return await this.ExecuteAsync(SoapAction.GetStatus, request, html =>
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
        /// <summary>
        /// 读取项
        /// </summary>
        /// <param name="items">项名</param>
        /// <returns></returns>
        public async Task<ResponseResult<ReadResponse>> ReadAsync(params ItemIdentifier[] items)
        {
            if (items == null || items.Length == 0) return new ResponseResult<ReadResponse>("参数出错.");
            return await this.ReadAsync(new List<ItemIdentifier>(items)).ConfigureAwait(false);
        }
        /// <summary>
        /// 读取项
        /// </summary>
        /// <param name="items">项名</param>
        /// <returns></returns>
        public async Task<ResponseResult<ReadResponse>> ReadAsync(List<ItemIdentifier> items)
        {
            if (items == null || items.Count == 0) return new ResponseResult<ReadResponse>("参数出错.");
            var itemsa = new List<ReadRequestItem>();
            foreach (var name in items)
            {
                itemsa.Add(new ReadRequestItem()
                {
                    ItemName = name.ItemName,
                    ItemPath = name.ItemPath,
                    ClientItemHandle = this.ClientRequestHandle
                });
            }
            var request = new Envelope<ReadRequest>
            {
                Body = new SoapBody<ReadRequest>
                {
                    Value = new ReadRequest
                    {
                        Options = new RequestOptions
                        {
                            ClientRequestHandle = this.ClientRequestHandle,
                            LocaleID = this.LocaleID,
                            ReturnErrorText = true,
                            ReturnItemPath = true,
                            ReturnDiagnosticInfo = true,
                            ReturnItemTime = true,
                            ReturnTimeName = true
                        },
                        ItemList = new ReadRequestItemList
                        {
                            Items = itemsa
                        }
                    }
                }
            };
            return await this.ExecuteAsync(SoapAction.Read, request, html =>
            {
                var entity = html.XmlToEntity<Envelope<ReadResponse>>();
                if (entity != null && entity.Body?.Value != null)
                {
                    return entity.Body?.Value;
                }
                return null;
            }).ConfigureAwait(false);
        }
        /// <summary>
        /// 读取项
        /// </summary>
        /// <param name="items">项名</param>
        /// <returns></returns>
        public async Task<List<NodeValue>> ReadNodeAsync(params ItemIdentifier[] items)
        {
            if (items == null || items.Length == 0) return null;
            return await this.ReadNodeAsync(new List<ItemIdentifier>(items)).ConfigureAwait(false);
        }
        /// <summary>
        /// 读取项
        /// </summary>
        /// <param name="items">项名集</param>
        /// <returns></returns>
        public async Task<List<NodeValue>> ReadNodeAsync(List<ItemIdentifier> items)
        {
            if (items == null || items.Count == 0) return null;
            var responseResult = await this.ReadAsync(items).ConfigureAwait(false);
            if (responseResult == null || responseResult.Status == ResponseStatus.Error || responseResult.Data == null) return null;
            var list = new List<NodeValue>();
            foreach (var item in responseResult.Data.RItemList.Items)
            {
                list.Add(new NodeValue(item));
            }
            return list;
        }
        #endregion

        #region 写项
        /// <summary>
        /// 写项
        /// </summary>
        /// <param name="values">值</param>
        /// <returns></returns>
        public async Task<ResponseResult<WriteResponse>> WriteAsync(params ItemValue[] values)
        {
            if (values == null || values.Length == 0) return new ResponseResult<WriteResponse>("参数出错.");
            return await this.WriteAsync(new List<ItemValue>(values)).ConfigureAwait(false);
        }
        /// <summary>
        /// 写项
        /// </summary>
        /// <param name="values">值</param>
        /// <returns></returns>
        public async Task<ResponseResult<WriteResponse>> WriteAsync(List<ItemValue> values)
        {
            if (values == null || values.Count == 0) return new ResponseResult<WriteResponse>("参数出错.");
            var request = new Envelope<WriteRequest>
            {
                Body = new SoapBody<WriteRequest>
                {
                    Value = new WriteRequest
                    {
                        Options = new RequestOptions
                        {
                            ClientRequestHandle = this.ClientRequestHandle,
                            LocaleID = this.LocaleID,
                            ReturnErrorText = true,
                            ReturnItemPath = true,
                            ReturnDiagnosticInfo = true,
                            ReturnItemTime = true,
                            ReturnTimeName = true
                        },
                        ItemList = new WriteRequestItemList
                        {
                            Items = values
                        },
                        ReturnValuesOnReply = true
                    }
                }
            };
            return await this.ExecuteAsync(SoapAction.Write, request, html =>
            {
                var entity = html.XmlToEntity<Envelope<WriteResponse>>();
                if (entity != null && entity.Body?.Value != null)
                {
                    return entity.Body?.Value;
                }
                return null;
            }).ConfigureAwait(false);
        }
        /// <summary>
        /// 写项
        /// </summary>
        /// <param name="values">值</param>
        /// <returns></returns>
        public async Task<List<NodeValue>> WriteNodeAsync(params ItemValue[] values)
        {
            if (values == null || values.Length == 0) return null;
            return await this.WriteNodeAsync(new List<ItemValue>(values)).ConfigureAwait(false);
        }
        /// <summary>
        /// 写项
        /// </summary>
        /// <param name="values">值</param>
        /// <returns></returns>
        public async Task<List<NodeValue>> WriteNodeAsync(List<ItemValue> values)
        {
            if (values == null || values.Count == 0) return null;
            var responseResult = await this.WriteAsync(values).ConfigureAwait(false);
            if (responseResult == null || responseResult.Status == ResponseStatus.Error || responseResult.Data == null) return null;
            var list = new List<NodeValue>();
            foreach (var item in responseResult.Data.RItemList.Items)
            {
                list.Add(new NodeValue(item));
            }
            return list;
        }
        #endregion

        #region 订阅
        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="rate">速率</param>
        /// <param name="items">项目</param>
        /// <returns></returns>
        public async Task<ResponseResult<SubscribeResponse>> SubscribeAsync(int rate,params ItemIdentifier[] items)
        {
            if (items == null || items.Length == 0) return new ResponseResult<SubscribeResponse>("参数出错.");
            return await this.SubscribeAsync(new List<ItemIdentifier>(items),rate).ConfigureAwait(false);
        }
        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="items">项目</param>
        /// <param name="rate">速率</param>
        /// <returns></returns>
        public async Task<ResponseResult<SubscribeResponse>> SubscribeAsync(List<ItemIdentifier> items,int rate =10000)
        {
            if (items == null || items.Count == 0) return new ResponseResult<SubscribeResponse>("参数出错.");
            var itemsa = new List<SubscribeRequestItem>();
            foreach (var name in items)
            {
                itemsa.Add(new SubscribeRequestItem()
                {
                    ItemName = name.ItemName,
                    ItemPath = name.ItemPath,
                    ClientItemHandle = this.ClientRequestHandle
                });
            }
            var request = new Envelope<SubscribeRequest>
            {
                Body = new SoapBody<SubscribeRequest>
                {
                    Value = new SubscribeRequest
                    {
                        Options = new RequestOptions
                        {
                            ClientRequestHandle = this.ClientRequestHandle,
                            LocaleID = this.LocaleID,
                            ReturnErrorText = true,
                            ReturnItemPath = true,
                            ReturnDiagnosticInfo = true,
                            ReturnItemTime = true,
                            ReturnTimeName = true
                        },
                        ItemList = new SubscribeRequestItemList
                        {
                            Items = itemsa
                        },
                        SubscriptionPingRate = rate
                    }
                }
            };
            return await this.ExecuteAsync(SoapAction.Subscribe, request, html =>
            {
                var entity = html.XmlToEntity<Envelope<SubscribeResponse>>();
                if (entity != null && entity.Body?.Value != null)
                {
                    return entity.Body?.Value;
                }
                return null;
            }).ConfigureAwait(false);
        }
        #endregion

        #region 取消订阅

        #endregion

        #region 轮询订阅

        #endregion

        #region 浏览
        /// <summary>
        /// 浏览节点
        /// </summary>
        /// <param name="options">请求配置</param>
        /// <returns></returns>
        public async Task<ResponseResult<BrowseResponse>> BrowseAsync(BrowseRequest options = null)
        {
            var option = new BrowseRequest
            {
                BrowseFilter = BrowseFilter.all,
                ReturnAllProperties = true,
                ReturnPropertyValues = true,
                LocaleID = this.LocaleID,
                ClientRequestHandle = this.ClientRequestHandle
            };
            option = option.Extend(options);
            var request = new Envelope<BrowseRequest>
            {
                Body = new SoapBody<BrowseRequest>
                {
                    Value = option
                }
            };
            return await this.ExecuteAsync(SoapAction.Browse, request, html =>
            {
                var entity = html.XmlToEntity<Envelope<BrowseResponse>>();
                if (entity != null && entity.Body?.Value != null)
                {
                    return entity.Body?.Value;
                }
                return null;
            }).ConfigureAwait(false);
        }
        #endregion

        #region 浏览节点
        /// <summary>
        /// 浏览节点
        /// </summary>
        /// <param name="browseAll">是否浏览所有节点</param>
        /// <param name="options">请求配置</param>
        /// <returns></returns>
        public async Task<List<NodeValue>> BrowseAsync(bool browseAll = false, BrowseRequest options = null)
        {
            if (options == null)
                options = new BrowseRequest
                {
                    BrowseFilter = BrowseFilter.all,
                    ReturnAllProperties = true,
                    ReturnPropertyValues = true,
                    LocaleID = this.LocaleID,
                    ClientRequestHandle = this.ClientRequestHandle
                };
            var responseResult = await this.BrowseAsync(options).ConfigureAwait(false);
            if (responseResult == null || responseResult.Status == ResponseStatus.Error || responseResult.Data == null) return null;
            var list = new List<NodeValue>();
            foreach(var e in responseResult.Data.Elements)
            {
                var nodeValue = new NodeValue(e);
                list.Add(nodeValue);
                if (browseAll && nodeValue.HasChildren)
                {
                    options.ItemName = e.ItemName;
                    nodeValue.ChildNodes = await this.BrowseAsync(browseAll, options).ConfigureAwait(false); 
                }
            };
            return list;
        }
        #endregion

        #region 获取属性
        /// <summary>
        /// 读取属性
        /// </summary>
        /// <param name="items">项</param>
        /// <returns></returns>
        public async Task<ResponseResult<GetPropertiesResponse>> GetPropertiesAsync(params ItemIdentifier[] items)
        {
            if (items == null || items.Length == 0) return new ResponseResult<GetPropertiesResponse>("参数出错.");
            return await this.GetPropertiesAsync(new List<ItemIdentifier>(items)).ConfigureAwait(false);
        }
        /// <summary>
        /// 读取属性
        /// </summary>
        /// <param name="items">项</param>
        /// <returns></returns>
        public async Task<ResponseResult<GetPropertiesResponse>> GetPropertiesAsync(List<ItemIdentifier> items)
        {
            if (items == null || items.Count == 0) return new ResponseResult<GetPropertiesResponse>("参数出错.");
            var request = new Envelope<GetPropertiesRequest>
            {
                Body = new SoapBody<GetPropertiesRequest>
                {
                    Value = new GetPropertiesRequest
                    {
                        ClientRequestHandle = this.ClientRequestHandle,
                        LocaleID = this.LocaleID,
                        ReturnAllProperties = true,
                        ReturnErrorText = true,
                        ReturnPropertyValues = true,
                        ItemIDs = items
                    }
                }
            };
            return await this.ExecuteAsync(SoapAction.GetProperties, request, html =>
            {
                var entity = html.XmlToEntity<Envelope<GetPropertiesResponse>>();
                if (entity != null && entity.Body?.Value != null)
                {
                    return entity.Body?.Value;
                }
                return null;
            }).ConfigureAwait(false);
        }
        /// <summary>
        /// 读取属性
        /// </summary>
        /// <param name="items">项目</param>
        /// <returns></returns>
        public async Task<Dictionary<string,List<ItemProperty>>> GetNodePropertiesAsync(params ItemIdentifier[] items)
        {
            if (items == null || items.Length == 0) return null;
            return await this.GetNodePropertiesAsync(new List<ItemIdentifier>(items)).ConfigureAwait(false);
        }
        /// <summary>
        /// 读取属性
        /// </summary>
        /// <param name="items">项目</param>
        /// <returns></returns>
        public async Task<Dictionary<string, List<ItemProperty>>> GetNodePropertiesAsync(List<ItemIdentifier> items)
        {
            if (items == null || items.Count == 0) return null;
            var responseResult = await this.GetPropertiesAsync(items).ConfigureAwait(false);
            if (responseResult == null || responseResult.Status == ResponseStatus.Error || responseResult.Data == null) return null;
            var dic = new Dictionary<string, List<ItemProperty>>();
            foreach (var item in responseResult.Data.PropertyLists)
            {
                dic.Add(item.ItemName,item.Properties);
            }
            return dic;
        }
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
                IsReset=true,
                Timeout=100000,
                ContentType="text/xml",
                BodyData = requestBody.EntityToXml().format(((double)this.OpcXmlVersion / 10).ToString("F1"))
            };
            result.RequestXml = http.BodyData;
            http.AddHeader("SOAPAction", XmlDaHelper.GetSoapAction(soapAction, this.OpcXmlVersion));
            var response = await http.GetResponseAsync().ConfigureAwait(false);
            result.ResponseXml = response.Html;
            http.Dispose();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (response.Html.IsXml())
                {
                    result.Data = func.Invoke(result.ResponseXml);
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