using Bp.Api.Models;
using Bp.Api.Service;
using BP.Api.Extensions;
using BP.Api.Loggers;
using BP.Api.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BP.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddFluentValidation();

            services.ConfigureMapping();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BP.Api", Version = "v1" });
            });
            services.AddHealthChecks();

            services.AddScoped<IContactService,ContactService>();

            services.AddTransient<IValidator<ContactDTO>, ContactValidator>();

            services.AddHttpClient("garantiapi", config =>
             {
                 config.BaseAddress = new Uri("http://www.garanti.com");
                 config.DefaultRequestHeaders.Add("Authorization", "Bearer 1212121");
             });

            //Burda da conf yapabilirdik fakat biz program cs de yapmayý tercih ettik
            //Bu ayarlarýn Çalýþabilmesi için appsetting.json daki logging conf larýn silinemsi gereklidir
            //services.AddLogging(i => //dependecy injection ý yapmýþ olduk peki kim için => ILooger interface i için 
            //{
            //    i.ClearProviders();
            //    i.SetMinimumLevel(LogLevel.Information);
            //    //i.SetMinimumLevel(LogLevel.Debug);
            //    //i.AddDebug();
            //    i.AddProvider(new MyCustomLoggerFactory());
            //});


            services.AddLogging();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BP.Api v1"));
            }


            app.UseCustomHealthCheck();

            app.UseResponseCaching();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
