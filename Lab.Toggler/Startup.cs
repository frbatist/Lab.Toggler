using Lab.Toggler.Infra.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using System.IO;
using System;

namespace Lab.Toggler
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
            services.AddDbContext<TogglerContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddMediatR(typeof(Startup));
            services.RegisterServices();

            services.AddMvcCore()
                .AddApiExplorer()
                .AddAuthorization(options =>
                {
                    options.AddPolicy("admin", builder =>
                    {
                        builder.RequireRole(new[] { "Admin" });
                    });
                })
                .AddJsonFormatters()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "Toggler.Api";
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Feature Toggler",
                        Version = "v1",
                        Description = "REST API for feature toggle management.",
                    });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<TogglerContext>();
                context.Database.Migrate();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Feature Toggler API V1");
            });

            app.UseMvc();
        }
    }
}
