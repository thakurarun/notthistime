using Amazon.DynamoDBv2;
using Database.Repository.Features.ProfileManagement.Register;
using DynamoDbSetup;
using DynamoDbSetup.DynamoDb;
using DynamoDbSetup.DynamoDb.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace NotThisTime
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; private set; }

        public Startup()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());

            var dynamoDbConfig = Configuration.GetSection("DynamoDb");
            var runLocalDynamoDb = dynamoDbConfig.GetValue<bool>("LocalMode");
            if (runLocalDynamoDb)
            {
                services.AddSingleton<IAmazonDynamoDB>(sp =>
                {
                    Environment.SetEnvironmentVariable("AWS_ACCESS_KEY_ID", Configuration["DynamoDb:AccessKey"]);
                    Environment.SetEnvironmentVariable("AWS_SECRET_ACCESS_KEY", Configuration["DynamoDb:SecretKey"]);
                    Environment.SetEnvironmentVariable("AWS_REGION", Configuration["DynamoDb:Region"]);
                    AmazonDynamoDBConfig clientConfig = new AmazonDynamoDBConfig();
                    // Set the endpoint URL
                    clientConfig.ServiceURL = "http://localhost:8000";
                    // mylocaldb
                    return new AmazonDynamoDBClient(clientConfig);
                });
            }
            else
            {
                Environment.SetEnvironmentVariable("AWS_ACCESS_KEY_ID", Configuration["AWS:AccessKey"]);
                Environment.SetEnvironmentVariable("AWS_SECRET_ACCESS_KEY", Configuration["AWS:SecretKey"]);
                Environment.SetEnvironmentVariable("AWS_REGION", Configuration["AWS:Region"]);
                services.AddAWSService<IAmazonDynamoDB>();
            }

            services.AddSingleton<ISetupDb, SetupDb>();
            services.AddSingleton<IProfile, Profile>();

            services.AddScoped<IRegistrationProcess, RegistrationProcess>();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
