using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AutoMapper;
using ConexionResidencial.Infraestructure;
using ConexionResidencial.Applications;
using ConexionResidencial.App.Services;
using ConexionResidencial.Core.Constants;

namespace ConexionResidencial.App
{
    public class Startup
    {
        private readonly string _MyCors = "Cors";
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAuthorization();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Secret"])),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: _MyCors, builder =>
                {
                    builder.WithOrigins(_configuration.GetSection("AllowedOrigins").Get<string[]>());
                    builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });

            });

            AddSwagger(services);

            ConnectionStrings.Conexion = _configuration.GetConnectionString("Conexion");

            services.AddInfrastructure(_configuration);
            services.AddApplication(_configuration);
            services.AddPresentationServices(_configuration, services.BuildServiceProvider());
        }

        public void Configure(IApplicationBuilder app,
                                IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(_MyCors);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger(options =>
            {
                options.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    if (httpReq.Headers.ContainsKey("X-Forwarded-Host"))
                    {
                        var servers = new List<OpenApiServer>
                        {
                            new()
                            {
                                Url = $"https://{httpReq.Headers["X-Forwarded-Host"]}/ConexionResidencial"
                            }
                        };

                        swagger.Servers = servers;
                    }
                });
            });

            app.UseSwaggerUI(c =>
            {
#if RELEASE
                c.SwaggerEndpoint("v1/swagger.json", "ConexionResidencial V1");
#elif DEBUG
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ConexionResidencial V1");
#endif
                c.DocumentTitle = "Servicios de ConexionResidencial";
                c.DocExpansion(DocExpansion.None);
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {

                var groupName = "v1";
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
					  Enter 'Bearer' [space] and then your token in the text input below.
					  \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"API ConexionResidencial",
                    Version = groupName,
                    Description = "API",
                    Contact = new OpenApiContact
                    {
                        Name = "Tus Peluqueras",
                        Email = string.Empty,
                        Url = new Uri("https://www.ConexionResidencial.cl/"),
                    }
                });
                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            });
        }
    }
}
