using AspNetCoreRateLimit;
using MCK.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using VueCliMiddleware;

namespace WebApiTpl
{
    public class Startup
    {
        /// <summary>
        ///     记录sql语句日志
        /// </summary>
        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder =>
            {
                builder.AddFilter((category, level) =>
                    category == DbLoggerCategory.Database.Command.Name
                    && level == LogLevel.Information).AddConsole();
            });

        public Startup(IConfiguration configuration)
        {
            
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddSingleton<IExtensionService, ExtensionService>();
            //gzip压缩
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
            });
            services.AddControllers().AddJsonOptions(options =>
            {
                //不修改返回的json的首字母大小写
                //options.JsonSerializerOptions.PropertyNamingPolicy = null; //大小写转换
                options.JsonSerializerOptions.Converters.Add(
                    new Core.Extensions.DatetimeJsonConverter()); // SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                //中文编码
                options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
            });
             #region db

//             StaticVar.DbConnection =
//                 Core.Extensions.SecurityExtensions.AESDecrypt(Configuration["ConnectionSettings:main"], 0);
           
          
//            //数据库注册
//            services.AddDbContext<MCKMainEntities>(options => options
//                // replace with your connection string
//#if DEBUG //输出日志
//                .UseLoggerFactory(MyLoggerFactory)
//#endif
//                .UseMySql(StaticVar.DbConnection, mySqlOptions => mySqlOptions
//                    // replace with your Server Version and Type
//                    .ServerVersion(new Version(8, 0, 18), ServerType.MySql))
//            );

            #endregion
            #region Jwt验证
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = Configuration["JwtSettings:Issuer"],
                    ValidAudience = Configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Configuration["JwtSettings:SecretKey"])),
                };
            });

            #endregion
            #region Ip请求限制

            //clientId思路  每次请求login界面 返回guid 将guid作为clientID的限制值
            //重新设定规则
            //仅当使用此clientId时计算限制次数 否则直接拒绝  其实没多大用  刷新一下clientId就行
            // 存储IP计数器及配置规则

            services.AddMemoryCache();
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            // 按照文档，这两个是3.x版的breaking change，要加上

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            #endregion
            //接口文档
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "Please enter into field the word 'Bearer' followed by a space and the token value",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference()
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    }, Array.Empty<string>() }
                });
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Web 5.0接口",
                    Description = @"除GetToken接口外 所有接口都需要添加Http头: Authorization:Bearer {token}"
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddControllers();
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<WebApiTpl.Middleware.GlobalExceptionMiddleware>();
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "接口V1"); });
            app.UseRouting();
            app.UseSpaStaticFiles();
            //调试接口
            if (env.IsDevelopment())
            {
                app.UseCors(options =>
                {
                    options.WithOrigins("http://localhost:8080");
                    options.AllowAnyHeader();
                    options.AllowAnyMethod();
                    options.AllowCredentials();
                });
            };
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //建议调试时候 前端单独运行 增加调试效率
            if (!env.IsDevelopment())
            {
                app.UseSpa(spa =>
                {
                    spa.Options.SourcePath = "dist";
                });
            }
        }
    }
}
