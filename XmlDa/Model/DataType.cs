using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using XiaoFeng.OPC.XmlDa.Model;

/****************************************************************
*  Copyright © (2026) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2026-01-12 00:23:01                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 数据类型
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// 字符串
        /// </summary>
        [Description("字符串")]
        [TypeName("String")]
        @string = 0,
        /// <summary>
        /// 布尔值
        /// </summary>
        [Description("布尔值")]
        [TypeName("Boolean")]
        boolean = 1,
        /// <summary>
        /// 单精度浮点型
        /// </summary>
        [Description("单精度浮点型")]
        [TypeName("Single")]
        @float = 2,
        /// <summary>
        /// 双精度浮点型
        /// </summary>
        [Description("双精度浮点型")]
        [TypeName("Double")] 
        @double = 3,
        /// <summary>
        /// 小数型
        /// </summary>
        [Description("小数型")]
        [TypeName("Decimal")] 
        @decimal = 4,
        /// <summary>
        /// 长整型
        /// </summary>
        [Description("长整型")]
        [TypeName("Int64")] 
        @long = 5,
        /// <summary>
        /// 长整型
        /// </summary>
        [Description("整型")]
        [TypeName("Int32")]
        @int = 6,
        /// <summary>
        /// 有符号8位整数
        /// </summary>
        [Description("有符号8位整数")]
        [TypeName("Int16")]
        @short = 7,
        /// <summary>
        /// 字节
        /// </summary>
        [Description("字节")]
        [TypeName("Byte")] 
        @byte = 8,
        /// <summary>
        /// 无符号长整型
        /// </summary>
        [Description("无符号长整型")]
        [TypeName("UInt64")] 
        unsignedLong = 9,
        /// <summary>
        /// 无符号整型
        /// </summary>
        [Description("无符号整型")]
        [TypeName("UInt32")] 
        unsignedInt = 10,
        /// <summary>
        /// 无符号8位整数
        /// </summary>
        [Description("无符号8位整数")]
        [TypeName("UInt16")] 
        unsignedShort = 11,
        /// <summary>
        /// 无符号字节
        /// </summary>
        [Description("无符号字节")]
        [TypeName("SByte")] 
        unsignedByte = 12,
        /// <summary>
        /// 字节数组
        /// </summary>
        [Description("字节数组")]
        [TypeName("Byte")] 
        base64Binary = 13,
        /// <summary>
        /// 日期类型
        /// </summary>
        [Description("日期类型")]
        [TypeName("DateTime")] 
        dateTime = 14,
        /// <summary>
        /// 日期类型
        /// </summary>
        [Description("日期类型")]
        [TypeName("DateTime")] 
        time = 15,
        /// <summary>
        /// 日期类型
        /// </summary>
        [Description("日期类型")]
        [TypeName("DateTime")] 
        date = 16,
        /// <summary>
        /// 有符号整型
        /// </summary>
        [Description("有符号整型")]
        [TypeName("UInt32")] 
        duration = 17,
        /// <summary>
        /// 字符串
        /// </summary>
        [Description("字符串")]
        [TypeName("String")] 
        QName = 18,
        /// <summary>
        /// 任意类型
        /// </summary>
        [Description("任意类型")]
        [TypeName("Object")] 
        AnyType = 19,
        /// <summary>
        /// 字节数组
        /// </summary>
        [Description("字节数组")]
        [TypeName("Byte")] 
        ArrayOfByte = 20,
        /// <summary>
        /// 有符号8位整数
        /// </summary>
        [Description("有符号8位整数数组")]
        [TypeName("Int16")]
        ArrayOfShort = 21,
        /// <summary>
        /// 无符号8位整数
        /// </summary>
        [Description("无符号8位整数数组")]
        [TypeName("UInt16")] 
        ArrayOfUnsignedShort = 22,
        /// <summary>
        /// 有符号整型数组
        /// </summary>
        [Description("有符号整型数组")]
        [TypeName("Int32")] 
        ArrayOfInt = 23,
        /// <summary>
        /// 无符号整型数组
        /// </summary>
        [Description("无符号整型数组")]
        [TypeName("UInt32")] 
        ArrayOfUnsignedInt = 24,
        /// <summary>
        /// 有符号长整型数组
        /// </summary>
        [Description("有符号长整型数组")]
        [TypeName("Int64")] 
        ArrayOfLong = 25,
        /// <summary>
        /// 无符号长整型数组
        /// </summary>
        [Description("无符号长整型数组")]
        [TypeName("UInt64")]
        ArrayOfUnsignedLong = 26,
        /// <summary>
        /// 单精度数组
        /// </summary>
        [Description("单精度数组")]
        [TypeName("Single")] 
        ArrayOfSingle = 27,
        /// <summary>
        /// 小数型数组
        /// </summary>
        [Description("小数型数组")]
        [TypeName("Decimal")] 
        ArrayOfDecimal = 28,
        /// <summary>
        /// 双精度数组
        /// </summary>
        [Description("双精度数组")]
        [TypeName("Double")] 
        ArrayOfDouble = 29,
        /// <summary>
        /// 布尔值数组
        /// </summary>
        [Description("布尔值数组")]
        [TypeName("Boolean")] 
        ArrayOfBoolean = 30,
        /// <summary>
        /// 字符串数组
        /// </summary>
        [Description("字符串数组")]
        [TypeName("String")] 
        ArrayOfString = 31,
        /// <summary>
        /// 时间数组
        /// </summary>
        [Description("时间数组")]
        [TypeName("DateTime")] 
        ArrayOfDateTime = 32,
        /// <summary>
        /// 任意类型数组
        /// </summary>
        [Description("任意类型数组")]
        [TypeName("Object")] 
        ArrayOfAnyType = 33
    }
}