using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProductService.Clients;
using ProductService.Interfaces;
using ProductService.Services;
using Refit;

namespace ProductService
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
            services.AddMvc().AddNewtonsoftJson();
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
            });


            services.AddDbContext<ProductContext>(options => 
                options.UseNpgsql(Configuration.GetConnectionString("Product")));


            services.AddAutoMapper(typeof(Startup));

         
            var refitSettings = new RefitSettings
            {                
                ContentSerializer = new NewtonsoftJsonContentSerializer(new JsonSerializerSettings
                {                    
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                })
            };


            services.TryAddTransient(_ => RestService.For<IPoligonClient>(new HttpClient()
            {
                BaseAddress = new Uri("https://cloud-api.yandex.net/v1/disk")
            }, refitSettings));

            services.TryAddTransient(_=>RestService.For<IImageClient>(new HttpClient()
                {
                    BaseAddress = new Uri("https://localhost:5005")
                }, refitSettings));

            services.TryAddTransient(_=>RestService.For<IPriceClient>(new HttpClient()
                {
                    BaseAddress = new Uri("https://localhost:5003")
                }, refitSettings));


            services.AddSwaggerGenNewtonsoftSupport();
            services.AddSwaggerGen();
            services.AddControllers();
            services.AddTransient<IProductService, Services.ProductService>();
            services.AddTransient<IYaDiskService, YaDiskService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./swagger/v1/swagger.json", $"Product Service API");
                c.RoutePrefix = string.Empty;
            });

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
