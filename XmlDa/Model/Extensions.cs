using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

/****************************************************************
*  Copyright © (2026) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2026-01-10 11:29:26                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class Extensions
    {

        #region 属性

        #endregion

        #region 方法
        /// <summary>
        /// 给对象赋值
        /// </summary>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <typeparam name="T1">源对象类型</typeparam>
        /// <param name="obj">目标对象</param>
        /// <param name="obj1">源对象</param>
        /// <returns></returns>
        public static T Extend<T, T1>(this T obj, T1 obj1) where T : class
        {
            if (obj == null) obj = Activator.CreateInstance<T>();
            if (obj1 == null) return obj;
            var targetType = typeof(T);
            typeof(T1).GetPropertiesAndFields(p =>
            {
                var name = p.Name;
                var property = targetType.GetProperty(name, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
                if (property == null)
                {
                    var field = targetType.GetField(name, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
                    if (field == null) return;
                    var value = p is PropertyInfo _p ? _p.GetValue(obj1) : ((FieldInfo)p).GetValue(obj1);
                    if (value == null) return;
                    field.SetValue(obj, value.GetValue(field.FieldType));
                }
                else
                {
                    var value = p is PropertyInfo _p ? _p.GetValue(obj1) : ((FieldInfo)p).GetValue(obj1);
                    if (value == null) return;
                    property.SetValue(obj, value.GetValue(property.PropertyType));
                }
            });
            return obj;
        }
        #endregion
    }
}