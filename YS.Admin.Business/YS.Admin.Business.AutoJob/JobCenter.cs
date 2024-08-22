using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Quartz;
using YS.Admin.Entity.SystemManage;
using YS.Admin.Service.SystemManage;
using YS.Admin.Util;
using YS.Admin.Util.Model;
using YS.Admin.Util.Extension;
using YS.Admin.Enum;
using YS.Admin.Business.SystemManage;

namespace YS.Admin.Business.AutoJob
{
    public class JobCenter
    {
        public void Start()
        {
            Task.Run(async () =>
            {
                TData<List<QuartzoptionsEntity>> obj = await new QuartzoptionsBLL().GetList(null);
                if (obj.Tag == 1)
                {
                    AddScheduleJob(obj.Data);
                }
            });
        }

        #region 添加任务计划
        private void AddScheduleJob(List<QuartzoptionsEntity> entityList)
        {
            try
            {
                foreach (QuartzoptionsEntity entity in entityList)
                {
                    if (entity.TaskStatus == 0)
                        continue;
                    if (entity.StartTime == null)
                    {
                        entity.StartTime = DateTime.Now;
                    }
                    DateTimeOffset starRunTime = DateBuilder.NextGivenSecondDate(entity.StartTime, 1);
                    if (entity.EndTime == null)
                    {
                        entity.EndTime = DateTime.MaxValue.AddDays(-1);
                    }
                    DateTimeOffset endRunTime = DateBuilder.NextGivenSecondDate(entity.EndTime, 1);

                    var scheduler = JobScheduler.GetScheduler();
                    IJobDetail job = JobBuilder.Create<JobExecute>().WithIdentity(entity.TaskName, entity.GroupName).Build();
                    job.JobDataMap.Add("Id", entity.Id);

                    ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create()
                                                 .StartAt(starRunTime)
                                                 .EndAt(endRunTime)
                                                 .WithIdentity(entity.TaskName, entity.GroupName)
                                                 .WithCronSchedule(entity.CronExpression)
                                                 .Build();

                    scheduler.ScheduleJob(job, trigger);
                    scheduler.Start();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        public void AddScheduleJob(QuartzoptionsEntity entity)
        {
            try
            {
                if (entity.StartTime == null)
                {
                    entity.StartTime = DateTime.Now;
                }
                DateTimeOffset starRunTime = DateBuilder.NextGivenSecondDate(entity.StartTime, 1);
                if (entity.EndTime == null)
                {
                    entity.EndTime = DateTime.MaxValue.AddDays(-1);
                }
                DateTimeOffset endRunTime = DateBuilder.NextGivenSecondDate(entity.EndTime, 1);

                var scheduler = JobScheduler.GetScheduler();
                IJobDetail job = JobBuilder.Create<JobExecute>().WithIdentity(entity.TaskName, entity.GroupName).Build();
                job.JobDataMap.Add("Id", entity.Id);

                ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create()
                                             .StartAt(starRunTime)
                                             .EndAt(endRunTime)
                                             .WithIdentity(entity.TaskName, entity.GroupName)
                                             .WithCronSchedule(entity.CronExpression)
                                             .Build();
                scheduler.ScheduleJob(job, trigger);


                // 创建一个立即执行的简单触发器
                var immediateTrigger = TriggerBuilder.Create()
                        .WithIdentity("immediateTrigger", entity.GroupName)
                        .StartNow()
                        .Build();

                // 使用相同的作业和立即触发的触发器
                scheduler.ScheduleJob(job, immediateTrigger);

                scheduler.Start();


                JobKey jobKey = new JobKey("immediateTrigger", entity.GroupName);
                scheduler.DeleteJob(jobKey);


            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }
        #endregion

        #region 清除任务计划
        public void ClearScheduleJob()
        {
            try
            {
                JobScheduler.GetScheduler().Clear();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }


        public async Task<bool> StopJob(QuartzoptionsEntity entity)
        {
            try
            {
                // 获取Scheduler实例
                IScheduler scheduler = JobScheduler.GetScheduler();
                // 定义要删除的作业的键
                JobKey jobKey = new JobKey(entity.TaskName, entity.GroupName);

                // 检查作业是否存在
                if (await scheduler.CheckExists(jobKey))
                {
                    // 删除作业
                    await scheduler.DeleteJob(jobKey);
                    return true;
                }
                else
                {
                    Console.WriteLine("作业不存在。");
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return false;
            }
        }
        #endregion
    }
}
