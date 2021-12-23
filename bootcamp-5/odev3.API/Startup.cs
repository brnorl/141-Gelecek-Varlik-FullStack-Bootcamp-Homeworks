using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using odev3.API.Attribute;
using odev3.API.Cache;
using odev3.API.Infrastructure;
using odev3.API.MailService;
using odev3.Service.Product;
using odev3.Service.User;

namespace odev3.API
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
            var _mappingProfile = new MapperConfiguration(mp => { mp.AddProfile(new MappingProfile()); });
            IMapper mapper = _mappingProfile.CreateMapper();
            //AutoMapper added.
            services.AddSingleton(mapper);


            //Services Added.
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IProductService, ProductService>();
            //Cache servisleri
            services.AddMemoryCache();
            services.AddSingleton<IUserCache, UserCache>();
            //Loginfilter servisleri
            services.AddScoped<LoginFilter>();

            services.AddSingleton<ISendMail, SendMail>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "odev3.API", Version = "v1" });
            });
        }
        //Server=.\\SQLEXPRESS;Database=Proje;Trusted_Connection=True;
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                GlobalConfiguration.Configuration
            .UseSqlServerStorage("Server=.\\SQLEXPRESS;Database=Proje;Trusted_Connection=True");
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "odev3.API v1"));
            }

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
