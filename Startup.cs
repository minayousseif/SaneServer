using System;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SaneServer.Server.Data;
using SaneServer.Server.Services;
using SaneServer.Server.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SaneServer.Server.Helpers;


namespace SaneServer
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
            // configure the app settings and bind it to a setting instance
            var appSettingsConfig = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsConfig);

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("DataSource=scanners.db"));

            // configure the jwt auth
            var appSettings = appSettingsConfig.Get<AppSettings>();
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Secret)),
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false

                };
            });
            
            services.AddControllers()
                    .AddJsonOptions(options => {
                         options.JsonSerializerOptions.WriteIndented = true;
                         options.JsonSerializerOptions.IgnoreNullValues = true;
                    });

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "Client/build";
            });

            services.AddScoped<IShellService, ShellService>();
            services.AddScoped<ISaneService, SaneService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICustomPasswordValidator, CustomPasswordValidator>();

            StaticFiles.CreateStaticFilesDirs();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "Client";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
