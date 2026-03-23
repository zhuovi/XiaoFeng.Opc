using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Schema;
using System.Xml.Serialization;
using XiaoFeng.Data.SQL;
using XiaoFeng.Json;
using XiaoFeng.Xml;

/****************************************************************
*  Copyright © (2026) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2026-01-15 00:08:30                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// Opc值
    /// </summary>
    public class OpcValue
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public OpcValue()
        {

        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="value">值</param>
        public OpcValue(string value)
        {
            this.Value = value;
        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="dataType">类型</param>
        public OpcValue(string value, DataType dataType)
        {
            this.Value = value;
            this.XsiType = $"xsd:{dataType}";
        }
        #endregion

        #region 属性
        /// <summary>
        /// 值
        /// </summary>
        [XmlText]
        public string Value { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        [XmlAttribute("type", Namespace = "http://www.w3.org/2001/XMLSchema-instance",Form = XmlSchemaForm.Qualified
        )]
        public string XsiType
        {
            get => this._XsiType;
            set => this._XsiType = value;
        }
        /// <summary>
        /// 类型
        /// </summary>
        private string _XsiType = "xsd:string";
        #endregion

        #region 方法
        /// <summary>
        /// 获取类型
        /// </summary>
        /// <returns></returns>
        public Type getType()
        {
            var typeName = this.XsiType.StartsWith("xsd:") ? this.XsiType.SubString(4) : this.XsiType;

            return Type.GetType(typeName.ToEnum<DataType>().GetTypeName().Name);
        }
        /// <summary>
        /// 强制转换
        /// </summary>
        /// <param name="v">值</param>
        public static explicit operator string(OpcValue v)
        {
            return v.Value;
        }
        /// <summary>
        /// 隐式转换
        /// </summary>
        /// <param name="v">值</param>
        public static implicit operator OpcValue(string v)
        {
            return new OpcValue(v);
        }
        ///<inheritdoc/>
        public override string ToString()
        {
            return this.Value;
        }
        /// <summary>
        /// 获取值
        /// </summary>
        /// <returns></returns>
        public object GetValue()
        {
            var dataType = this.XsiType;
            if (dataType.StartsWith("xsd:")) dataType = dataType.Substring(4);
            var isArray = dataType.StartsWith("ArrayOf") || dataType.EqualsIgnoreCase("base64Binary");
            var typeName = dataType.ToEnum<DataType>().GetTypeName().Name;
            var type = Type.GetType($"System.{typeName}, mscorlib");
            if (isArray)
            {
                if (this.Value.IsNotNullOrEmpty())
                {
                    var xml = this.Value.XmlToEntity<XmlValue>();
                    if (xml==null || !xml.HasChildNodes)return null;
                    
                    var arr = Array.CreateInstance(type, xml.ChildNodes.Count);
                    for(var i = 0; i < xml.ChildNodes.Count; i++)
                    {
                        arr.SetValue(xml.ChildNodes[i].Value.GetValue(type), i);
                    }
                    return arr;
                }
                return null;
            }
            else
                return this.Value.GetValue(type);
        }
        /// <summary>
        /// 获取值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public T GetValue<T>()
        {
            return (T)this.GetValue();
        }
        #endregion

    }
}