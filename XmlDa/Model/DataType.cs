using System;
using System.Collections.Generic;
using System.Text;

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
        @string = 0,
        boolean = 1,
        @float = 2,
        @double = 3,
        @decimal = 4,
        @long = 5,
        @int = 6,
        @short = 7,
        @byte = 8,
        unsignedLong = 9,
        unsignedInt = 10,
        unsignedShort = 11,
        unsignedByte = 12,
        base64Binary = 13,
        dateTime = 14,
        time = 15,
        date = 16,
        duration = 17,
        QName = 18,
        AnyType = 19,
        ArrayOfByte = 20,
        ArrayOfShort = 21,
        ArrayOfUnsignedShort = 22,
        ArrayOfInt = 23,
        ArrayOfUnsignedInt = 24,
        ArrayOfLong = 25,
        ArrayOfUnsignedLong = 26,
        ArrayOfFloat = 27,
        ArrayOfDecimal = 28,
        ArrayOfDouble = 29,
        ArrayOfBoolean = 30,
        ArrayOfString = 31,
        ArrayOfDateTime = 32,
        ArrayOfAnyType = 33
    }
}