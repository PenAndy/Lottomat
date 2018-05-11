using System.Collections.Generic;
using Lottomat.Application.Entity.CommonEntity;

namespace Lottomat.Application.SystemAutoJob
{
    public class SchedulerConfiguration
    {
        //任务列表
        public readonly List<TaskList> _taskList;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="taskList">任务列表</param>
        public SchedulerConfiguration(List<TaskList> taskList)
        {
            _taskList = taskList;
        }
    }
}