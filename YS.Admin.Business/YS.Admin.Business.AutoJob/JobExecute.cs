using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl.Triggers;
using SixLabors.ImageSharp.Drawing;
using YS.Admin.Entity.SystemManage;
using YS.Admin.Enum;
using YS.Admin.Service.SystemManage;
using YS.Admin.Util;
using YS.Admin.Util.Extension;
using YS.Admin.Util.Model;
using YS.Admin.Util.Http;
using System.Net.Http;

namespace YS.Admin.Business.AutoJob
{
	public class JobExecute : IJob
	{
		private QuartzoptionsService autoJobService = new QuartzoptionsService();
		private QuartzlogService autoJobLogService = new QuartzlogService();
		 
		public Task Execute(IJobExecutionContext context)
		{
			return Task.Run(async () =>
			{
				TData obj = new TData();
				long jobId = 0;
				JobDataMap jobData = null;
				QuartzoptionsEntity dbJobEntity = null;
				try
				{
					jobData = context.JobDetail.JobDataMap;
					jobId = jobData["Id"].ParseToLong();
					// 获取数据库中的任务
					dbJobEntity = await autoJobService.GetEntity(jobId);


					if (dbJobEntity != null)
					{
                        QuartzoptionsEntity entity = new QuartzoptionsEntity();
                        entity.Id= jobId;
                        entity.LastRunTime = DateTime.Now;
						await autoJobService.SaveForm(entity); 
					}

					if (dbJobEntity != null)
					{
						if (dbJobEntity.TaskStatus == StatusEnum.Yes.ParseToInt())
						{
							CronTriggerImpl trigger = context.Trigger as CronTriggerImpl;
							if (trigger != null)
							{
								if (trigger.CronExpressionString != dbJobEntity.CronExpression)
								{
									// 更新任务周期
									trigger.CronExpressionString = dbJobEntity.CronExpression;
									await JobScheduler.GetScheduler().RescheduleJob(trigger.Key, trigger);
								}
								foreach (var item in jobData.Values)
								{
									//重新到数据库查找任务的所有信息。
									//item
									QuartzoptionsEntity qe = await autoJobService.GetEntity(item.ParseToLong());
                                    TaskMethodEnum enumValueParsed = (TaskMethodEnum)TaskMethodEnum.Parse(typeof(TaskMethodEnum), qe.Method);
									switch (enumValueParsed)
									{
										case TaskMethodEnum.Get:
											{
												//string addr = qe.ApiUrl + qe.PostData;
												//obj.Message = addr.AccessApi();

                                                Dictionary<string, string> header = new Dictionary<string, string>();
												if (!string.IsNullOrEmpty(dbJobEntity.AuthKey)
													&& !string.IsNullOrEmpty(dbJobEntity.AuthValue))
												{
													header.Add(dbJobEntity.AuthKey.Trim(), dbJobEntity.AuthValue.Trim());
												}
												obj.Message = await HttpManager.SendAsync(HttpMethod.Get, dbJobEntity.ApiUrl.Trim(), dbJobEntity.PostData,
													dbJobEntity.TimeOut.ParseToInt(), header);
                                                obj.Tag = 1;
                                            }

											break;
										case TaskMethodEnum.Post:
											{
												Dictionary<string, string> header = new Dictionary<string, string>();
												if (!string.IsNullOrEmpty(dbJobEntity.AuthKey)
													&& !string.IsNullOrEmpty(dbJobEntity.AuthValue))
												{
													header.Add(dbJobEntity.AuthKey.Trim(), dbJobEntity.AuthValue.Trim());
												}
												obj.Message = await HttpManager.SendAsync(HttpMethod.Post, dbJobEntity.ApiUrl.Trim(), dbJobEntity.PostData,
													dbJobEntity.TimeOut.ParseToInt(), header);
                                                obj.Tag = 1;
                                            }
											break;
										case TaskMethodEnum.LocalExe:
											{
                                                #region 执行程序
                                                // 脚本或程序的路径
                                                string scriptPath = qe.PostData;

                                                // 创建Process对象
                                                Process process = new Process();

                                                // 配置启动信息
                                                process.StartInfo.FileName = scriptPath;
                                                process.StartInfo.UseShellExecute = false; // 不使用Shell执行
                                                process.StartInfo.RedirectStandardOutput = true; // 重定向标准输出
                                                process.StartInfo.CreateNoWindow = true; // 不创建新窗口
                                                try
                                                {
                                                    // 启动进程
                                                    process.Start();

                                                    // 读取输出
                                                    string output = process.StandardOutput.ReadToEnd();
                                                    process.WaitForExit(); // 等待进程退出
                                                    obj.Message = output;
                                                }
                                                catch (Exception ex)
                                                {
                                                    obj.Message = ex.Message;
                                                }
                                                obj.Tag = 1;
                                                #endregion
                                            }
                                            break;
										case TaskMethodEnum.Sql:
											{
                                                obj.Message = await autoJobService.ExecuteBySql(qe.PostData);
                                                obj.Tag = 1;
                                            }
                                            break;
										case TaskMethodEnum.Other:
											break;
										default:
											break;
									}
								}
								#region 执行任务
								switch (context.JobDetail.Key.Name)
								{
									case "数据库备份":
										obj = await new DatabasesBackupJob().Start();
										break;
								}
								#endregion
							}
						}
					}
				}
				catch (Exception ex)
				{
					obj.Message = ex.GetOriginalException().Message;
					LogHelper.Error(ex);
				}

				try
				{
					if (dbJobEntity != null)
					{
						if (dbJobEntity.TaskStatus == StatusEnum.Yes.ParseToInt())
						{
							#region 更新下次运行时间
							await autoJobService.SaveForm(new QuartzoptionsEntity
							{
								Id = dbJobEntity.Id,
								NextStartTime = context.NextFireTimeUtc.Value.DateTime.AddHours(8)
							});
							#endregion

							#region 记录执行状态
							//todo:增加操作状态的记录
							await autoJobLogService.SaveForm(new QuartzlogEntity
							{
								TaskName = context.JobDetail.Key.Name,
								LogStatus = obj.Tag,
								ResponseContent = obj.Message
							});
							#endregion
						}
					}
				}
				catch (Exception ex)
				{
					obj.Message = ex.GetOriginalException().Message;
					LogHelper.Error(ex);
				}
			});
		}
	}
}
