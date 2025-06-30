using Opc.Ua;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using XiaoFeng.Collections;

/****************************************************************
*  Copyright © (2025) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2025-06-17 15:56:36                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.Opc.Model
{
    /// <summary>
    /// 节点ID
    /// </summary>
    public class NodeValueId : IComparable, IEquatable<NodeValueId>, ICloneable
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public NodeValueId()
        {
            this.Identifier = null;
            this.IdentifierType = IdType.Numeric;
            this.NamespaceIndex = 0;
        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="valueId">节点ID</param>
        /// <exception cref="ArgumentNullException">当值是空的时候抛出异常</exception>
        public NodeValueId(NodeValueId valueId)
        {
            if (valueId == null) throw new ArgumentNullException(nameof(valueId));
            this.Identifier = valueId.Identifier;
            this.NamespaceIndex = valueId.NamespaceIndex;
            this.IdentifierType = valueId.IdentifierType;
        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="nodeId">NodeId节点</param>
        /// <exception cref="ArgumentNullException">异常</exception>
        public NodeValueId(NodeId nodeId)
        {
            if(nodeId == null) throw new ArgumentNullException(nameof (nodeId));
            this.Identifier = nodeId.Identifier;
            this.NamespaceIndex = nodeId.NamespaceIndex;
            this.IdentifierType= nodeId.IdType;
        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="identifierType">标识符类型</param>
        /// <param name="namespaceIndex">空间索引</param>
        /// <param name="identifier">值</param>
        public NodeValueId(IdType identifierType, ushort namespaceIndex, object identifier)
        {
            this.IdentifierType = identifierType;
            this.NamespaceIndex = namespaceIndex;
            this.Identifier = identifier;
        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="namespaceIndex">空间索引</param>
        public NodeValueId(uint value, ushort namespaceIndex = 0) : this(IdType.Numeric, namespaceIndex, value) { }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="namespaceIndex">空间索引</param>
        public NodeValueId(string value, ushort namespaceIndex = 0) : this(IdType.String, namespaceIndex, value)
        {
            if (namespaceIndex > 0)
            {
                this.NamespaceIndex = namespaceIndex;
                this.Identifier = value;
                this.IdentifierType = IdType.String;
                return;
            }
            if (value.IsNullOrEmpty())
            {
                this.NamespaceIndex = 0;
                this.Identifier = null;
                this.IdentifierType = IdType.Numeric;
                return;
            }
            if (!value.Contains("="))
            {
                this.NamespaceIndex = 2;
                this.Identifier = value;
                this.IdentifierType = IdType.String;
                return;
            }
            var pc = new ParameterCollection(value.ReplacePattern(@"\s*;\s*", "&"));
            var namespaceSet = false;
            if (pc.Contains("ns"))
            {
                this.NamespaceIndex = pc["ns"].ToCast<ushort>();
                namespaceSet = true;
            }
            if (pc.Contains("i"))
            {
                this.Identifier = pc["i"].ToCast<int>();
                this.IdentifierType = IdType.Numeric;
                return;
            }
            if (pc.Contains("s"))
            {
                this.Identifier = pc["s"];
                this.IdentifierType = IdType.String;
                return;
            }
            if (pc.Contains("g"))
            {
                this.Identifier = pc["g"].ToCast<Guid>();
                this.IdentifierType = IdType.Guid;
                return;
            }
            if (pc.Contains("b"))
            {
                this.Identifier = pc["b"].FromBase64StringToBytes();
                this.IdentifierType = IdType.Opaque;
                return;
            }
            if (namespaceSet)
            {
                this.Identifier = value;
                this.IdentifierType = IdType.String;
                return;
            }
        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="namespaceIndex">空间索引</param>
        public NodeValueId(Guid value, ushort namespaceIndex = 0) : this(IdType.Guid, namespaceIndex, value) { }
        public NodeValueId(byte[] value, ushort namespaceIndex = 0)
        {
            this.IdentifierType = IdType.Opaque;
            this.NamespaceIndex = namespaceIndex;
            if (value != null)
            {
                var val = new byte[value.Length];
                Array.Copy(value, val, value.Length);
                this.Identifier = val;
            }
        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="namespaceIndex">空间索引</param>
        public NodeValueId(object value, ushort namespaceIndex = 0)
        {
            this.NamespaceIndex = namespaceIndex;
            if (value is uint)
            {
                this.Identifier = value;
                this.IdentifierType = IdType.Numeric;
                return;
            }
            if (value is null || value is string)
            {
                this.Identifier = value.ToString();
                this.IdentifierType = IdType.String;
                return;
            }
            if (value is Guid val)
            {
                this.Identifier = val;
                this.IdentifierType = IdType.Guid;
                return;
            }
            if (value is byte[] vals)
            {

                this.Identifier = value;
                this.IdentifierType = IdType.Opaque;
                return;
            }
        }
        #endregion

        #region 属性
        /// <summary>
        /// 标识类型
        /// </summary>
        [Description("标识类型")]
        public IdType IdentifierType { get; set; }
        /// <summary>
        /// 空间索引
        /// </summary>
        [Description("空间索引")]
        public ushort NamespaceIndex { get; set; }
        /// <summary>
        /// 标识内容
        /// </summary>
        [Description("标识内容")]
        public object Identifier { get; set; }
        #endregion

        #region 方法
        /// <summary>
        /// 值转换成 <see cref="NodeValueId"/>
        /// </summary>
        /// <param name="value">值</param>
        public static implicit operator NodeValueId(uint value)
        {
            return new NodeValueId(value);
        }
        /// <summary>
        /// 把 <see cref="NodeValueId"/> 转换成 uint
        /// </summary>
        /// <param name="id"><see cref="NodeValueId"/></param>
        public static explicit operator uint(NodeValueId id)
        {
            if (id == null || id.Identifier.IsNullOrEmpty()) return 0;
            return id.ToCast<uint>();
        }
        /// <summary>
        /// 值转换成 <see cref="NodeValueId"/>
        /// </summary>
        /// <param name="value">值</param>
        public static implicit operator NodeValueId(Guid value)
        {
            return new NodeValueId(value);
        }
        /// <summary>
        /// 把 <see cref="NodeValueId"/> 转换成GUID
        /// </summary>
        /// <param name="id"><see cref="NodeValueId"/></param>
        public static explicit operator Guid(NodeValueId id)
        {
            if (id == null || id.Identifier.IsNullOrEmpty()) return Guid.Empty;
            return id.Identifier.ToCast<Guid>();
        }
        /// <summary>
        /// 值转换成 <see cref="NodeValueId"/>
        /// </summary>
        /// <param name="value">值</param>
        public static implicit operator NodeValueId(string value)
        {
            return new NodeValueId(value);
        }
        /// <summary>
        /// 把 <see cref="NodeValueId"/> 转换成字符串
        /// </summary>
        /// <param name="id"><see cref="NodeValueId"/></param>
        public static explicit operator string(NodeValueId id)
        {
            if (id == null || id.Identifier.IsNullOrEmpty()) return string.Empty;
            if (id.IdentifierType == IdType.Opaque) return ((byte[])id.Identifier).ToBase64String();
            return (string)id.Identifier;
        }
        /// <summary>
        /// 把字节数组转换成 <see cref="NodeValueId"/>
        /// </summary>
        /// <param name="vals">字节数组</param>
        public static implicit operator NodeValueId(byte[] vals)
        {
            return new NodeValueId(vals);
        }
        /// <summary>
        /// 把 <see cref="NodeValueId"/> 转换成字节数组
        /// </summary>
        /// <param name="id">NodeValueId</param>
        public static explicit operator byte[](NodeValueId id)
        {
            if (id == null || id.Identifier.IsNullOrEmpty()) return Array.Empty<byte>();
            return (byte[])id.Identifier;
        }
        /// <summary>
        /// 把 <see cref="NodeId"/> 转换为 <see cref="NodeValueId"/>
        /// </summary>
        /// <param name="nodeId">节点Id</param>
        public static implicit operator NodeValueId(NodeId nodeId)
        {
            return new NodeValueId(nodeId);
        }
        /// <summary>
        /// 把 <see cref="NodeValueId"/> 转换为 <see cref="NodeId"/>
        /// </summary>
        /// <param name="nodeId">节点Id</param>
        public static explicit operator NodeId(NodeValueId nodeId)
        {
            return new NodeId(nodeId.Identifier,nodeId.NamespaceIndex);
        }
        /// <summary>
        /// 是否相等
        /// </summary>
        /// <param name="left">左边对象</param>
        /// <param name="right">右边对象</param>
        /// <returns></returns>
        public static bool operator ==(NodeValueId left, NodeValueId right)
        {
            return left.Equals(right);
        }
        /// <summary>
        /// 是否不相等
        /// </summary>
        /// <param name="left">左边对象</param>
        /// <param name="right">右边对象</param>
        /// <returns></returns>
        public static bool operator !=(NodeValueId left, NodeValueId right)
        {
            return !left.Equals(right);
        }
        ///<inheritdoc/>
        public override string ToString()
        {
            var sbr = new StringBuilder();
            if (this.NamespaceIndex > 0)
            {
                sbr.Append($"ns={this.NamespaceIndex};");
            }
            if (this.IdentifierType == IdType.Numeric)
                sbr.Append($"i={this.Identifier};");
            else if (this.IdentifierType == IdType.String)
                sbr.Append($"s={this.Identifier};");
            else if (this.IdentifierType == IdType.Guid)
                sbr.Append($"g={((Guid)this.Identifier).ToString("D")};");
            else if (this.IdentifierType == IdType.Opaque)
                sbr.Append($"b={((byte[])this.Identifier).ToBase64String()};");
            return sbr.ToString();
        }
        /// <summary>
        /// 和另一个对象作对比
        /// </summary>
        /// <param name="obj">另一个对象</param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (Object.ReferenceEquals(obj, null)) return -1;
            if (Object.ReferenceEquals(obj, this)) return 0;
            if (obj is NodeValueId o)
            {
                return this.ToString().CompareTo(o.ToString());
            }
            if (obj is uint ou)
            {
                return this.ToString().CompareTo(new NodeValueId(ou).ToString());
            }
            if (obj is string os)
                return this.Identifier.ToString().CompareTo(os);
            if (obj is Guid og)
            {
                return this.ToString().CompareTo(new NodeValueId(og).ToString());
            }
            if (obj is byte[] ob)
                return this.ToString().CompareTo(new NodeValueId(ob).ToString());
            return -1;
        }
        /// <summary>
        /// 是否相等
        /// </summary>
        /// <param name="other">另一个对象</param>
        /// <returns></returns>
        public bool Equals(NodeValueId other)
        {
            return this.CompareTo(other) == 0;
        }
        /// <summary>
        /// 复制
        /// </summary>
        /// <returns>复制后的对象</returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        /// <summary>
        /// 获取 HashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
        /// <summary>
        /// 是否相等
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return CompareTo(obj) == 0;
        }

        #region 析构器
        /// <summary>
        /// 析构器
        /// </summary>
        ~NodeValueId()
        {

        }
        #endregion
        #endregion
    }
}