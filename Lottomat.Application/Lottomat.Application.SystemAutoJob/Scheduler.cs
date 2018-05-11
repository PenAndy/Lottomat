using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Lottomat.Application.Entity.CommonEntity;
using Lottomat.Application.SystemAutoJob.Interface;
using Lottomat.Utils.Date;

namespace Lottomat.Application.SystemAutoJob
{
    public class Scheduler
    {
        private readonly SchedulerConfiguration _configuration = null;
        /// <summary>
        /// 停止任务时间节点
        /// </summary>
        private readonly List<int> _hour = new List<int>{ 0, 1, 2, 3, 4, 5 };

        //计时器
        private static readonly System.Timers.Timer _timer = new System.Timers.Timer();

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="config"></param>
        public Scheduler(SchedulerConfiguration config)
        {
            _configuration = config;
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        public void Start()
        {
            _timer.Start();
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(ExecuteJob);//到达时间的时候执行事件
            _timer.Interval = 0.5 * 60 * 1000; //设置间隔时间，为毫秒
            _timer.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件
        }

        /// <summary>
        /// 执行
        /// </summary>
        private void ExecuteJob(object sender, EventArgs e)
        {
            DateTime now = DateTimeHelper.Now;
            if (!_hour.Contains(now.Hour))
            {
                List<TaskList> lists = _configuration._taskList;
                if (lists.Count != 0)
                {
                    foreach (TaskList list in lists)
                    {
                        //执行每一个任务
                        foreach (ISchedulerJob job in list.Jobs)
                        {
                            ThreadStart myThreadDelegate = new ThreadStart(job.Execute);

                            Thread myThread = new Thread(myThreadDelegate)
                            {
                                IsBackground = true
                            };
                            myThread.Start();
                        }
                        //线程休眠
                        Thread.Sleep(list.SleepInterval);
                    }
                }
            }
        }
    }
}