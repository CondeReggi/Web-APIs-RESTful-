﻿using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace WebApiAuthores
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices( IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions( x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles );

            services.AddDbContext<AplicationDbContext>( options => options.UseSqlServer( Configuration.GetConnectionString("defaultConnection") ) );

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddAutoMapper(typeof(Startup));

        }

        public void Configure( IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
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
