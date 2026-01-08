using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using XiaoFeng.OPC.XML;
using XiaoFeng.OPC.XML.Model;

/****************************************************************
*  Copyright © (2025) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2025-12-28 21:05:45                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.Xml
{
    /// <summary>
    /// SoapHelper 类说明
    /// </summary>
    internal static class SoapHelper
    {
        /// <summary>
        /// 将请求实体序列化为 SOAP 1.1 信封
        /// </summary>
        public static string SerializeToSoapEnvelope<TRequest>(TRequest request) where TRequest : BaseRequest
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var sb = new StringBuilder();
            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Encoding = Encoding.UTF8,
                Indent = true
            };

            using (var writer = XmlWriter.Create(sb, settings))
            {
                // 写入 SOAP 1.1 信封
                writer.WriteStartElement("soap", "Envelope", OpcXmlHelper.Soap11EnvelopeNamespace);
                writer.WriteStartElement("soap", "Body", OpcXmlHelper.Soap11EnvelopeNamespace);

                // 序列化请求实体
                var serializer = new XmlSerializer(typeof(TRequest), OpcXmlHelper.Namespace);
                serializer.Serialize(writer, request);

                // 关闭标签
                writer.WriteEndElement(); // soap:Body
                writer.WriteEndElement(); // soap:Envelope
            }
            return sb.ToString();
        }

        /// <summary>
        /// 从 SOAP 响应中反序列化出响应实体
        /// </summary>
        public static TResponse DeserializeFromSoapEnvelope<TResponse>(string soapXml) where TResponse : BaseResponse, new()
        {
            if (string.IsNullOrEmpty(soapXml)) throw new ArgumentException("SOAP 响应为空", nameof(soapXml));

            using (var reader = XmlReader.Create(new StringReader(soapXml)))
            {
                reader.MoveToContent();

                // 检查是否为 SOAP Fault
                if (reader.Name == "Fault" && reader.NamespaceURI == OpcXmlHelper.Soap11EnvelopeNamespace)
                {
                    var fault = DeserializeSoapFault(reader);
                    throw new OpcException($"OPC 服务器错误：{fault.Message}", fault.ErrorCode);
                }

                // 定位到响应节点
                reader.ReadToDescendant(typeof(TResponse).Name, OpcXmlHelper.Namespace);
                //if (!reader.Success)
                 //   throw new OpcException($"SOAP 响应中未找到 {typeof(TResponse).Name} 节点");

                // 反序列化响应实体
                var serializer = new XmlSerializer(typeof(TResponse), OpcXmlHelper.Namespace);
                var response = (TResponse)serializer.Deserialize(reader);

                // 检查响应状态
                if (response.ResultCode != 0)
                    throw new OpcException($"操作失败：{response.ResultText}", response.ResultCode);
            
            return response;
            }
        }

        /// <summary>
        /// 解析 SOAP Fault 错误
        /// </summary>
        private static SoapFault DeserializeSoapFault(XmlReader reader)
        {
            var fault = new SoapFault();
            while (reader.Read())
            {
                if (reader.IsStartElement("faultcode", OpcXmlHelper.Soap11EnvelopeNamespace))
                    fault.ErrorCode = int.TryParse(reader.ReadElementContentAsString(), out var code) ? code : -1;
                else if (reader.IsStartElement("faultstring", OpcXmlHelper.Soap11EnvelopeNamespace))
                    fault.Message = reader.ReadElementContentAsString();
            }
            return fault;
        }

        /// <summary>
        /// SOAP Fault 临时实体
        /// </summary>
        private class SoapFault
        {
            public int ErrorCode { get; set; }
            public string Message { get; set; } = string.Empty;
        }
    }
}