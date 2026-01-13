using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using XiaoFeng.Json;

/****************************************************************
*  Copyright © (2026) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2026-01-14 00:13:03                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 订阅信息
    /// </summary>
    public class Subscription
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public Subscription()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 订阅ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 订阅项
        /// </summary>
        public List<ItemIdentifier> Items { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastTime { get; set; }
        /// <summary>
        /// 更新速率 单位为毫秒
        /// </summary>
        public int UpdateRate { get; set; }
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool Enable { get; set; } = true;
        /// <summary>
        /// 回调事件
        /// </summary>
        public event NotificationEventHadler Notification;
        /// <summary>
        /// 取消标识
        /// </summary>
        private CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();
        /// <summary>
        /// 客户端
        /// </summary>
        [XmlIgnore,JsonIgnore]
        public XmlDaClient DaClient { get; set; }
        #endregion

        #region 方法
        /// <summary>
        /// 启用订阅
        /// </summary>
        public void Start()
        {
            this.CancellationTokenSource = new CancellationTokenSource();
            this.Enable = true;
            Task.Run(async () =>
            {
                while (!this.CancellationTokenSource.IsCancellationRequested)
                {

                    await Task.Delay(this.UpdateRate).ConfigureAwait(false);
                }
            }, this.CancellationTokenSource.Token);
        }
        /// <summary>
        /// 停用订阅
        /// </summary>
        public void Stop()
        {
            this.CancellationTokenSource.Cancel();
            this.Enable = false;
        }
        #endregion
    }
}