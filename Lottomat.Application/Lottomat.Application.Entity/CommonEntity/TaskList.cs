using System.Collections;

namespace Lottomat.Application.Entity.CommonEntity
{
    /// <summary>
    /// 任务模型
    /// </summary>
    public class TaskList
    {
        /// <summary>
        /// 间隔执行时间，单位：毫秒
        /// </summary>
        public int SleepInterval
        {
            get;
            set;
        }

        /// <summary>
        /// 任务列表
        /// </summary>
        public ArrayList Jobs
        {
            get;
            set;
        }
    }
}