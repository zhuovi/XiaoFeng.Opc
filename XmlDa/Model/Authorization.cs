using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

/****************************************************************
*  Copyright © (2026) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2026-01-17 20:28:22                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 认证
    /// </summary>
    public class Authorization
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public Authorization()
        {

        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="mode">模式</param>
        /// <param name="token">token</param>
        public Authorization(AuthorizationMode mode,string token)
        {
            this.Mode= mode;
            this.Token= token;
        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="mode">模式</param>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <param name="key">key</param>
        /// <param name="token">token</param>
        public Authorization(AuthorizationMode mode,string account,string password,string key,string token)
        {
            this.Mode= mode;
            this.Account= account;
            this.Password= password;
            this.Key= key;
            this.Token= token;
        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="value">header</param>
        public Authorization(NameValueHeaderValue value)
        {
            this.Mode = AuthorizationMode.Header;
            this.Key= value.Name;
            this.Token= value.Value;
        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        public Authorization(string account,string password)
        {
            this.Mode = AuthorizationMode.AccountAndPassword;
            this.Account= account;
            this.Password= password;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 认证模式
        /// </summary>
        public AuthorizationMode Mode { get; set; }
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        #endregion

        #region 方法
        ///<inheritdoc/>
        public override string ToString()
        {
            switch (this.Mode)
            {
                case AuthorizationMode.Basic:
                    return $"Basic {this.Token}";
                case AuthorizationMode.Bearer:
                    return $"Bearer {this.Token}";
                case AuthorizationMode.AccountAndPassword:
                    return $"{this.Account}:{this.Password}";
                default:
                    return this.Token;
            }
        }
        /// <summary>
        /// 带认证模式的字符串
        /// </summary>
        /// <returns></returns>
        public string ToStringX()
        {
            return $"{this.Mode}:{this}";
        }
        #endregion
    }
}