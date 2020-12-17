using System;
using System.Net.Http;
using System.Xml;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;
using WeatherService.Clients;
using WeatherService.Services;

namespace WeatherService
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WeatherService", Version = "v1" });
            });

            var refitSettings = new RefitSettings()
            {
                ContentSerializer = new XmlContentSerializer(new XmlContentSerializerSettings
                    {
                        XmlReaderWriterSettings = new XmlReaderWriterSettings()
                        {
                            ReaderSettings = new XmlReaderSettings
                            {
                                IgnoreWhitespace = true
                            }
                        }
                    }
                )
            };

            services.TryAddTransient(_ => RestService.For<IOpenWeatherClient>(new HttpClient()
            {
                BaseAddress = new Uri(Configuration["Api:OpenWeatherBaseAddress"])
            }, refitSettings));

            services.AddTransient<IWeatherService, Services.WeatherService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WeatherService v1"));
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
