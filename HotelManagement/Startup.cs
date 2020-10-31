using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using HotelManagement.Models;
using HotelManagement.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HotelManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IClientsRepository, ClientsRepository>();
            services.AddScoped<IBookingsRepository, BookingsRepository>();
            services.AddScoped<IRoomsRepository, RoomsRepository>();
            services.AddScoped<IDbRepository, DbRepository>();

            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("HotelManagementConnection")));
            
            services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                var dateConverter = new IsoDateTimeConverter
                {
                    DateTimeFormat = "dd'-'MM'-'yyyy'T'HH':'mm"
                };

                options.SerializerSettings.Converters.Add(dateConverter);
                options.SerializerSettings.Culture = new CultureInfo("en-GB");
                options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                //options.SerializerSettings.Converters.Add(new StringEnumConverter());
        });

            services.AddSwaggerGenNewtonsoftSupport();
            services.AddSwaggerGen();
            services.AddAutoMapper(typeof(Startup));
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelManagement V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
