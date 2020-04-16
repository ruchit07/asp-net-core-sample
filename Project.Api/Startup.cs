using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Ginger.Authentication.Jwt;
using Newtonsoft.Json.Serialization;
using Project.Data.Context;
using Project.Data.Infrastructure;
using Project.Data.Repositories;
using Project.Service;

namespace Project.Startup
{
    public class Startup
    {
        public IContainer ApplicationContainer { get; private set; }
        private IHostingEnvironment HostingEnvironment { get; set; }
        public IConfigurationRoot Configuration { get; }
        private string CurrentURL { get; set; }
        private string ConnectionString
        {
            get
            {
                return this.HostingEnvironment.IsDevelopment() ? Configuration.GetConnectionString("DefaultConnection") : Configuration.GetConnectionString("DefaultConnection");
            }
        }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            this.HostingEnvironment = env;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add the whole configuration object here.
            services.AddSingleton<IConfiguration>(Configuration);

            // Add framework services.
            services.AddDbContext<ProjectContext>(options =>
                options.UseSqlServer(this.ConnectionString));

            ProjectContext.ConnectionString = this.ConnectionString;
            ProjectContext.SecretKey = Configuration.GetSection("JwtConfig:SecretKey").Value;
            ProjectContext.CurrentURL = Configuration.GetSection("JwtConfig:ValidIssuer").Value;
            ProjectContext.AppURL = Configuration.GetSection("JwtConfig:ValidAudience").Value;
            ProjectContext.TokenExpireMinute = Configuration.GetSection("JwtConfig:TokenExpireMinute").Value;

            services.AddMvc().AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.PropertyNamingPolicy = null;
                o.JsonSerializerOptions.DictionaryKeyPolicy = null;
            });
            services.AddRazorPages().AddMvcOptions(options => options.EnableEndpointRouting = false);
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("CorsPolicy",
            //        b => b.AllowAnyOrigin()
            //        .AllowAnyMethod()
            //        .AllowAnyHeader()
            //        .AllowCredentials()
            //        .AllowAnyOrigin());
            //});
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddCors();

            // JWT - Start
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,

                            ValidIssuer = Configuration.GetSection("JwtConfig:ValidIssuer").Value,
                            ValidAudience = Configuration.GetSection("JwtConfig:ValidAudience").Value,
                            IssuerSigningKey = JwtSecurityKey.Create(Configuration.GetSection("JwtConfig:SecretKey").Value)
                        };

                        options.Events = new JwtBearerEvents
                        {
                            OnAuthenticationFailed = context =>
                            {
                                Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                                return Task.CompletedTask;
                            },
                            OnTokenValidated = context =>
                            {
                                Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                                return Task.CompletedTask;
                            }
                        };
                    });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Member",
                    policy => policy.RequireClaim("sid"));
            });
            // JWT - End

            // create a Autofac container builder
            var builder = new ContainerBuilder();
            builder.Populate(services);
            AutofacModule.RegisterType(builder);
            ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule());
        }

        public void Configure(IApplicationBuilder app, ProjectContext context)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                ProjectContext.ConnectionString = Configuration.GetConnectionString("DefaultConnection");
                serviceScope.ServiceProvider.GetService<ProjectContext>()
                    .Database.Migrate();
            }

            //Cors
            //app.UseCors("CorsPolicy");
            app.UseMvc();
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseSession();
            DbInitializer.Initialize(context);
        }
    }
}