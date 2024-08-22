using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YS.Admin.Util;
using YS.Admin.Util.Model;
using YS.Admin.Web.Controllers;
using Microsoft.Extensions.Logging;
using UEditor.Core;
using Z.Expressions;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Http.Features;
using YS.Admin.Business.AutoJob;
using System.Net;
using YS.Admin.Web.Hubs;

namespace YS.Admin.Web
{
    public class Startup
	{
		public IConfiguration Configuration { get; }
		public IWebHostEnvironment WebHostEnvironment { get; set; }

		public Startup(IConfiguration configuration, IWebHostEnvironment env)
		{
			Configuration = configuration;
			WebHostEnvironment = env;
			GlobalContext.LogWhenStart(env);
			GlobalContext.HostingEnvironment = env;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			//if (WebHostEnvironment.IsDevelopment())
			//{
			services.AddRazorPages().AddRazorRuntimeCompilation();
            //} 
            services.Configure<FormOptions>(x =>
			{
				x.ValueLengthLimit = int.MaxValue;
				x.MultipartBodyLengthLimit = int.MaxValue;
			});



			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));


            // 添加WebSocket支持
            services.AddSignalR();

            services.AddControllersWithViews(options =>
			{
				options.Filters.Add<GlobalExceptionFilter>();
				options.ModelMetadataDetailsProviders.Add(new ModelBindingMetadataProvider());
			}).AddNewtonsoftJson(options =>
			{
				// 返回数据首字母不小写，CamelCasePropertyNamesContractResolver是小写
				options.SerializerSettings.ContractResolver = new DefaultContractResolver();
			});

			services.AddMemoryCache();
			services.AddSession();
			services.AddHttpContextAccessor();


            // 注册为单例
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddOptions();

			services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(GlobalContext.HostingEnvironment.ContentRootPath + Path.DirectorySeparatorChar + "DataProtection"));
			services.AddUEditorService();
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);  // 注册Encoding

			GlobalContext.SystemConfig = Configuration.GetSection("SystemConfig").Get<SystemConfig>();
			GlobalContext.Services = services;
			GlobalContext.Configuration = Configuration;
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			System.AppContext.SetSwitch("System.Drawing.EnableUnixSupport", true);
			if (!string.IsNullOrEmpty(GlobalContext.SystemConfig.VirtualDirectory))
			{
				app.UsePathBase(new PathString(GlobalContext.SystemConfig.VirtualDirectory)); // 让 Pathbase 中间件成为第一个处理请求的中间件， 才能正确的模拟虚拟路径
			}
			if (WebHostEnvironment.IsDevelopment())
			{
				GlobalContext.SystemConfig.Debug = true;
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			string resource = Path.Combine(env.ContentRootPath, "upload");
			FileHelper.CreateDirectory(resource);
			//DefaultFilesOptions options = new DefaultFilesOptions();
			//options.DefaultFileNames.Clear();
			//options.DefaultFileNames.Add("index.html"); // 设置其他默认文件名

			//app.UseDefaultFiles(options); // 使用自定义的默认文件选项


			app.UseStaticFiles(new StaticFileOptions
			{
				OnPrepareResponse = GlobalContext.SetCacheControl
			});
			app.UseStaticFiles(new StaticFileOptions
			{
				RequestPath = "/upload",
				FileProvider = new PhysicalFileProvider(resource),
				OnPrepareResponse = GlobalContext.SetCacheControl,
				ContentTypeProvider = new FileExtensionContentTypeProvider(new Dictionary<string, string>
					{
					{ ".apk","application/vnd.android.package-archive"},
					{ ".pdf","application/pdf"},
					{ ".jpeg","image/jpeg"},
					{ ".jpg","image/jpeg"},
					{ ".gif","image/gif"},
					{ ".png","image/png"},
					{ ".mp4","video/mp4"},
					{ ".mp3","audio/mpeg"}
					})
			});
			app.UseSession();
			app.UseRouting();
			app.UseAuthorization();

            //app.UseMiddleware<AccessBlocklist>(); //启动黑名单认证

            app.UseCors();
            app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute("areas", "{area:exists}/{controller=Home}/{action=Index}/{id?}");

				endpoints.MapControllerRoute(
		name: "Default",
		pattern: "{controller}/{action}/{id?}",
		defaults: new { controller = "Web", action = "Index" }
	);
                endpoints.MapHub<SignalRHup>("/SignalRHup");
            });
            // 启用WebSocket传输
            //app.UseWebSockets();

            GlobalContext.ServiceProvider = app.ApplicationServices;


			//if (!GlobalContext.SystemConfig.Debug)
			//{
			//new JobCenter().ClearScheduleJob();
			//new JobCenter().Start(); // 启动定时任务
			//} 
		}
	}
}
