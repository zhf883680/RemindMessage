using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTpl
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(serverOptions =>
                    {
                        serverOptions.Limits.MaxRequestBodySize = long.MaxValue;
                        serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromSeconds(60);
                        serverOptions.Limits.RequestHeadersTimeout = TimeSpan.FromSeconds(60);
                    }).UseKestrel().UseStartup<Startup>();
                });
    }
}
