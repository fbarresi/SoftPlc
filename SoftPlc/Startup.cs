using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SoftPlc.Interfaces;
using SoftPlc.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace SoftPlc
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
			services.AddSingleton<IPlcService, PlcService>();

	        // Register the Swagger generator, defining 1 or more Swagger documents
	        services.AddSwaggerGen(c =>
	        {
		        c.SwaggerDoc("v1", new Info
		        {
			        Version = "v1",
			        Title = "SoftPlc API",
			        Description = ".NET Core Web API to SoftPlc",
			        TermsOfService = "None",
			        Contact = new Contact
			        {
				        Name = "Federico Barresi",
				        Email = string.Empty,
				        Url = "https://github.com/fbarresi/SoftPlc"
			        },
			        License = new License
			        {
				        Name = "Use under MIT",
				        Url = "https://github.com/fbarresi/SoftPlc/blob/master/LICENSE"
					}
		        });

		        // Set the comments path for the Swagger JSON and UI.
		        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
		        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
		        c.IncludeXmlComments(xmlPath);
			});

			services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

	        // Enable middleware to serve generated Swagger as a JSON endpoint.
	        app.UseSwagger();

	        // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
	        // specifying the Swagger JSON endpoint.
	        app.UseSwaggerUI(c =>
	        {
		        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SoftPlc API V1");
	        });

			app.UseMvc();

			//initialize plc service on start
			var plcService = app.ApplicationServices.GetService<IPlcService>();
        }
    }
}
