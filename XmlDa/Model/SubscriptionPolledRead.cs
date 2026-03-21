using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XiaoFeng.Threading;

/****************************************************************
*  Copyright © (2026) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2026-03-19 14:34:07                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 订阅轮询读
    /// </summary>
    public class SubscriptionPolledRead: Subscription
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public SubscriptionPolledRead()
        {

        }
        #endregion

        #region 属性
        /// <summary>
        /// 任务作业
        /// </summary>
        public TaskJob Worker { get; set; }
        /// <summary>
        /// 回调
        /// </summary>
        public Action<ITaskJob,SubscriptionPolledRead> Callback { get; set; }
        /// <summary>
        /// 取消指令
        /// </summary>
        public CancellationTokenSource CancellationTokenSource { get; set; }
        /// <summary>
        /// 上次节点数据
        /// </summary>
        public ConcurrentDictionary<string,ItemValue> Nodes { get; set; }
        /// <summary>
        /// 是否调试
        /// </summary>
        public bool IsDebug { get; set; } = true;
        #endregion

        #region 方法
        /// <summary>
        /// 启动
        /// </summary>
        public void Start()
        {
            this.Worker = new TaskJob(j =>
            {
                this.Callback.Invoke(j, this);
            });
            this.Worker.Run();
        }
        /// <summary>
        /// 暂停
        /// </summary>
        public void Pause()
        {
            this.Worker.Pause();
        }
        /// <summary>
        /// 恢复
        /// </summary>
        public void Resume()
        {
            this.Worker.Resume();
        }
        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            this.Worker.Stop();
        }

        #region 析构器
        /// <summary>
        /// 析构器
        /// </summary>
        ~SubscriptionPolledRead()
        {

        }
        #endregion

        #endregion
    }
}