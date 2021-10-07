using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using QiriPeru.Ecommerce.API.Dtos;
using QiriPeru.Ecommerce.API.Middleware;
using QiriPeru.Ecommerce.BussinessLogic.Data;
using QiriPeru.Ecommerce.BussinessLogic.Logic;
using QiriPeru.Ecommerce.Core.Entities;
using QiriPeru.Ecommerce.Core.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QiriPeru.Ecommerce.API
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

            services.AddScoped<ITokenService, TokenService>();

            var builder = services.AddIdentityCore<Usuario>();
            builder = new IdentityBuilder(builder.UserType, builder.Services);
            builder.AddRoles<IdentityRole>();
            builder.AddEntityFrameworkStores<SeguridadDbContext>();
            builder.AddSignInManager<SignInManager<Usuario>>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:Key"])),
                    ValidIssuer = Configuration["Token:Issuer"],
                    ValidateIssuer = true,
                    ValidateAudience = false,
                };
            });


            services.AddAutoMapper(typeof(MappingProfiles));

            services.AddDbContext<QiriPeruDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddDbContext<SeguridadDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("IdentitySeguridad"));
            });

            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var configuration = ConfigurationOptions.Parse(Configuration.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(configuration);
            });

            services.TryAddSingleton<ISystemClock, SystemClock>();

            services.AddTransient<IProductoRepository, ProductoRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IGenericSeguridadRepository<>), typeof(GenericSeguridadRepository<>));

            services.AddControllers();

            services.AddScoped<ICarritoCompraRepository, CarritoCompraRepository>();

            services.AddCors(opt =>
           {
               opt.AddPolicy("CorsRule", rule =>
               {
                   rule.AllowAnyHeader().AllowAnyMethod().WithOrigins("*");
               });
           });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStatusCodePagesWithReExecute("/errors", "?code={0}");

            app.UseRouting();

            app.UseCors("CorsRule");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
