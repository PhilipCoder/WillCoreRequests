using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CodeBuilder;
using CodeBuilder.JS;
using CodeBuilder.JS.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;

namespace TestWeb
{
    public class Startup
    {
        public Startup(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Microsoft.Extensions.Configuration.IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //Adds the WillCore Service
            services.AddWillCoreRequests<JavaScript>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors("all");

            //========================================================================================
            //Change the JavaScript Configuration
            app.ConfigureJavascript((config, mappings) =>
            {
               
            });
            //Configure WillCore reflection
            app.ConfigureWillCoreReflection(conf =>
            {
                conf.AttributeExcludeFilterAttributes.Add(typeof(DisableRequestSizeLimitAttribute));
            });
            //Build The Code
            app.WillCoreBuildMyCode<ControllerBase>();
            //========================================================================================

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseStaticFiles();
        }
    }
}
