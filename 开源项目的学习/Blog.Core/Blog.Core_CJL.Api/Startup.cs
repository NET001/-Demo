using Autofac;
using Blog.Core_CJL.Api.Filter;
using Blog.Core_CJL.Common.Helper;
using Blog.Core_CJL.Common.LogHelper;
using Blog.Core_CJL.Extensions;
using Blog.Core_CJL.Extensions.Middlewares;
using Blog.Core_CJL.Extensions.ServiceExtensions;
using Blog.Core_CJL.IServices;
using Blog.Core_CJL.Model.Seed;
using Blog.Core_CJL.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core_CJL.Api
{
    public class Startup
    {
        private IServiceCollection _services;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }
        //�ṩ��appsettings.json���в���
        //�˽ӿ���Program��CreateDefaultBuilder�����о;ͽ�����ע��,���Կ���ֱ���õ�ʵ��
        public IConfiguration Configuration { get; }
        //�ṩ�й�Ӧ�ó��������������е� web ������������Ϣ
        //�˽ӿ���Program��CreateDefaultBuilder�����о;ͽ�����ע��,���Կ���ֱ���õ�ʵ��
        public IWebHostEnvironment Env { get; }
        //�˷�������Ҫ��������DI����ͨ��IServiceCollection��չ��������������
        //�ٷ��Ƽ�����չ��������Microsoft.Extensions.DependencyInjection����,�˴���Ҫ��ʵ�ֶ�������Extensions����
        public void ConfigureServices(IServiceCollection services)
        {
            /*
            DIע����������������
            AddTransient��ÿ�����󣬶���ȡһ���µ�ʵ������ʹͬһ�������ȡ���Ҳ���ǲ�ͬ��ʵ��
            AddScoped��ÿ�����󣬶���ȡһ���µ�ʵ����ͬһ�������ȡ��λ�õ���ͬ��ʵ��
            AddSingleton��ÿ�ζ���ȡͬһ��ʵ��
             */
            //ע���װappsettings.json�����İ�����
            services.AddSingleton(new Appsettings(Configuration));
            //ע����־������
            services.AddSingleton(new LogLock(Env.ContentRootPath));
            //�˴�������Ҫ���JwtClaim�����л�ȡֵ����ʾ����
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            //����ע��
            services.AddMemoryCacheSetup();
            //Redisע��
            services.AddRedisCacheSetup();
            //ע��ISqlSugarClientʵ��
            services.AddSqlsugarSetup();
            //��������
            services.AddDbSetup();
            //AutoMapper����
            services.AddAutoMapperSetup();
            //����������CORS����
            services.AddCorsSetup();
            //MiniProfilerһ���������ܼ���Swagger���ʹ��
            services.AddMiniProfilerSetup();
            //Swagger����
            services.AddSwaggerSetup();
            //�������ע�롣ʹ��Quartz.NET
            services.AddJobSetup();
            //ע���ȡ��Ȩ�����Ϣ�İ�����
            services.AddHttpContextSetup();
            //����̨��ӡ
            services.AddAppConfigSetup(Env);
            //RedisInit
            services.AddRedisInitMqSetup();
            /*
             ������֤����Ȩ���������Ƚ�����֤�ڽ�����Ȩ,��֤���Ȼ������JWT Token�Ƿ���Ч,
            ����Ч�����뵽��Ȩ�жϵ�����
            ��֤���ж����Ƿ��ܹ�����,����Ȩ���ж����Ƿ���Ȩ�޽��з���
             */
            //JWT��֤
            services.AddAuthentication_JWTSetup();
            //��Ȩ
            services.AddAuthorizationSetup();
            //IpRateLimit�������
            services.AddIpPolicyRateLimitSetup(Configuration);
            //IO�����ܹ����ʵ�wwwroot�µ��ļ�
            services.Configure<KestrelServerOptions>(x => x.AllowSynchronousIO = true)
                    .Configure<IISServerOptions>(x => x.AllowSynchronousIO = true);
            //����������
            services.AddControllers(o =>
            {
                o.Filters.Add(typeof(GlobalExceptionsFilter));
                o.Conventions.Insert(0, new GlobalRoutePrefixFilter(new RouteAttribute(RoutePrefix.Name)));
            })
            //Json���л�����
             .AddNewtonsoftJson(options =>
             {
                 options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                 options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                 //ʱ���ʽ��ϵͳΪ׼
                 options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
             });
            //�滻
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());
            //���뷽ʽΪϵͳ����
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _services = services;
        }
        //����AutoFac��IOC����ʱ��һ���̶�д��
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModuleRegister());
            builder.RegisterModule<AutofacPropertityModuleReg>();
        }
        //�м��������,Configure�е�һЩ������ConfigureServices���Ѿ�������ע�����ֱ��ʹ��
        /*
         �м��������Ҳ��ͨ����չ����ʵ�ֵ�
        �Զ����м����Ҫ��Extensions���MiddlewareHelpers.cs��ͨ��app.UseMiddleware�����������
        - - -
        �м���ĺ����Ƕ�HttpContext���в���,�м������ִ��˳��˳���Ǹ������õ�˳��,�м���з�Ϊ
        next()����ǰ�ͷ�����,����ǰ���õ�request,���������õ�response
         */
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MyContext myContext, ITasksQzServices tasksQzServices, ISchedulerCenter schedulerCenter, IHostApplicationLifetime lifetime)
        {
            // Ip����,�����Źܵ����
            app.UseIpLimitMildd();
            /*
            �˿��д�˺ü�����ͬ���͵���־�м��,
            ��Щ��־�м������ͨ�����ý��п�����ر�,
            �ܸ�����Ҫ������ѡ��
             */
            //������־�м��
            app.UseReuestResponseLog();
            //�û���־�м��
            app.UseRecordAccessLogsMildd();
            //ip��־�м��
            app.UseIPLogMildd();
            //�˴������������һ��ҳ��,��������ʾ��ǰϵͳ������
            app.UseAllServicesMildd(_services);
            //�ж��Ƿ�Ϊ�������Ի������ڿ������Ի������ڿ������Ի�����ע��¶����ҳ����Ϣ
            if (env.IsDevelopment())
            {
                // �ڿ��������У�ʹ���쳣ҳ�棬�������Ա�¶�����ջ��Ϣ�����Բ�Ҫ��������������
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //�ض���errorģ��ҳ
                app.UseExceptionHandler("/Error");
                // �ڷǿ��������У�ʹ��HTTP�ϸ�ȫ����(or HSTS) ���ڱ���web��ȫ�Ƿǳ���Ҫ�ġ�
                // ǿ��ʵʩ HTTPS �� ASP.NET Core����� app.UseHttpsRedirection
                //app.UseHsts();
            }
            //swagger�м������ָ����һ���Զ����ģ��
            app.UseSwaggerMildd(() => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("Blog.Core_CJL.Api.index.html"));
            //CORS
            app.UseCors(Appsettings.app(new string[] { "Startup", "Cors", "PolicyName" }));
            //��̬�ļ��ܹ�����wwwroot
            app.UseStaticFiles();
            //Cookie
            app.UseCookiePolicy();
            //״̬��
            app.UseStatusCodePages();
            //����·��
            app.UseRouting();
            //��֤
            app.UseAuthentication();
            //��Ȩ
            app.UseAuthorization();
            //miniprofiler
            app.UseMiniProfilerMildd();
            //�ϵ��м��,����·��
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            /*
             ϵͳÿ������������һ����������еķ���
             */
            //���ɳ�ʼ����
            app.UseSeedDataMildd(myContext, Env.WebRootPath);
            //��ʼ���������
            app.UseQuartzJobMildd(tasksQzServices, schedulerCenter);
            //����ע������
            app.UseConsulMildd(Configuration, lifetime);
        }
    }
}
