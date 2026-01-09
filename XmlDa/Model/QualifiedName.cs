using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

/****************************************************************
*  Copyright © (2026) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2026-01-09 09:58:32                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 限定名称
    /// </summary>
    public class QualifiedName : ICloneable, IFormattable, IComparable
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public QualifiedName()
        {

        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="namespaceIndex">索引</param>
        public QualifiedName(string name, ushort namespaceIndex = 0)
        {
            this.Name = name;
            this.NamespaceIndex = namespaceIndex;
        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="qualifiedName">实例对象</param>
        public QualifiedName(QualifiedName qualifiedName)
        {
            this.Name = qualifiedName.Name;
            this.NamespaceIndex = qualifiedName.NamespaceIndex;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 名称
        /// </summary>
        [XmlIgnore]
        public string Name { get; set; }
        /// <summary>
        /// 空间索引
        /// </summary>
        [XmlIgnore]
        public ushort NamespaceIndex { get; set; }
        [XmlText]
        public string TextValue
        {
            get { return this.Name; }
            set { this.Name = value; }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 比较
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return -1;
            }

            if (this == obj)
            {
                return 0;
            }

            QualifiedName qualifiedName = obj as QualifiedName;
            if (qualifiedName == null)
            {
                return typeof(QualifiedName).GetTypeInfo().GUID.CompareTo(obj.GetType().GetTypeInfo().GUID);
            }

            if (qualifiedName.NamespaceIndex != NamespaceIndex)
            {
                return NamespaceIndex.CompareTo(qualifiedName.NamespaceIndex);
            }

            if (this.Name != null)
            {
                return string.CompareOrdinal(this.Name, qualifiedName.Name);
            }

            return 0;
        }
        /// <summary>
        /// 大于
        /// </summary>
        /// <param name="value1">对象1</param>
        /// <param name="value2">对象2</param>
        /// <returns></returns>
        public static bool operator >(QualifiedName value1, QualifiedName value2)
        {
            if ((object)value1 != null)
            {
                return value1.CompareTo(value2) > 0;
            }

            return false;
        }
        /// <summary>
        /// 小于
        /// </summary>
        /// <param name="value1">对象1</param>
        /// <param name="value2">对象2</param>
        /// <returns></returns>
        public static bool operator <(QualifiedName value1, QualifiedName value2)
        {
            if ((object)value1 != null)
            {
                return value1.CompareTo(value2) < 0;
            }

            return true;
        }
        /// <summary>
        /// HASH码
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            HashCode hashCode = default(HashCode);
            if (this.Name != null)
            {
                hashCode.Add(this.Name);
            }

            hashCode.Add(this.NamespaceIndex);
            return hashCode.ToHashCode();
        }
        /// <summary>
        /// 相等
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (this == obj)
            {
                return true;
            }

            QualifiedName qualifiedName = obj as QualifiedName;
            if (qualifiedName == null)
            {
                return false;
            }

            if (qualifiedName.NamespaceIndex != NamespaceIndex)
            {
                return false;
            }

            return qualifiedName.Name == this.Name;
        }
        /// <summary>
        /// ==
        /// </summary>
        /// <param name="value1">对象1</param>
        /// <param name="value2">对象2</param>
        /// <returns></returns>
        public static bool operator ==(QualifiedName value1, QualifiedName value2)
        {
            return value1?.Equals(value2) ?? ((object)value2 == null);
        }
        /// <summary>
        /// !=
        /// </summary>
        /// <param name="value1">对象1</param>
        /// <param name="value2">对象2</param>
        /// <returns></returns>
        public static bool operator !=(QualifiedName value1, QualifiedName value2)
        {
            if ((object)value1 != null)
            {
                return !value1.Equals(value2);
            }

            return (object)value2 != null;
        }
        ///<inheritdoc/>
        public virtual object Clone()
        {
            return MemberwiseClone();
        }
        /// <summary>
        /// 对对象进行深度复制。
        /// </summary>
        /// <returns></returns>
        public new object MemberwiseClone()
        {
            return this;
        }
        /// <summary>
        /// 转换字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.ToString(null, null);
        }
        /// <summary>
        /// 转换字符串
        /// </summary>
        /// <param name="format">字符串格式</param>
        /// <param name="formatProvider">驱动</param>
        /// <returns></returns>
        /// <exception cref="FormatException">格式化异常</exception>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null)
            {
                StringBuilder stringBuilder = new StringBuilder(((this.Name != null) ? this.Name.Length : 0) + 10);
                if (this.NamespaceIndex == 0)
                {
                    if (this.Name != null && this.Name.IndexOf(':') != -1)
                    {
                        stringBuilder.Append("0:");
                    }
                }
                else
                {
                    stringBuilder.Append(this.NamespaceIndex);
                    stringBuilder.Append(':');
                }
                if (this.Name != null)
                {
                    stringBuilder.Append(this.Name);
                }
                return stringBuilder.ToString();
            }
            throw new FormatException("Invalid format string: '{0}'.".format(format));
        }
        /// <summary>
        /// 强制转换
        /// </summary>
        /// <param name="value">对象</param>
        public static implicit operator QualifiedName(string value)
        {
            return new QualifiedName(value);
        }
        /// <summary>
        /// 是否为空
        /// </summary>
        [XmlIgnore]
        public bool IsEmpty => IsNull(this);
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">对象</param>
        /// <returns></returns>
        public static bool IsNull(QualifiedName value) => !(value.Name.IsNotNullOrEmpty() || value.NamespaceIndex != 0);

        #region 析构器
        /// <summary>
        /// 析构器
        /// </summary>
        ~QualifiedName()
        {

        }
        #endregion

        #endregion
    }
}