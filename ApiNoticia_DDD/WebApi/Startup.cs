using Aplicacao.Aplicacoes;
using Aplicacao.Interfaces;
using Dominio.Interfaces;
using Dominio.Interfaces.Genericos;
using Dominio.Interfaces.InterfaceServicos;
using Dominio.Servicos;
using Entidades.Entidades;
using Infraestrutura.Configuracoes;
using Infraestrutura.Repositorio;
using Infraestrutura.Repositorio.Generico;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Token;

namespace WebApi
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
            //services.AddCors();
            //services.AddDbContext<Contexto>(options=>
            //options.UseSqlServer(
            //     Configuration.GetConnectionString("Default")));
            services.AddDbContext<Contexto>(context => context.UseInMemoryDatabase("NoticiaDb"));

            services.AddDefaultIdentity<ApplicationUser>(options=>
            options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<Contexto>();

            // INTERFACE E REPOSITORIO
            services.AddSingleton(typeof(IGenerico<>), typeof(RepositorioGenerico<>));
            services.AddSingleton<INoticia, RepositorioNoticia>();
            services.AddSingleton<IUsuario, RepositorioUsuario>();

            // SERVI�O DOMINIO
            services.AddSingleton<IServicoNoticia, ServicoNoticia>();

            // INTERFACE APLICA��O
            services.AddSingleton<IAplicacaoNoticia, AplicacaoNoticia>();
            services.AddSingleton<IAplicacaoUsuario, AplicacaoUsuario>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = "Teste.Securiry.Bearer",
                        ValidAudience = "Teste.Securiry.Bearer",
                        IssuerSigningKey = JwtSecurityKey.Create("Secret_Key-12345678")
                    };

                    option.Events = new JwtBearerEvents
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


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header usando o Bearer.
                                    Entre com 'Bearer ' [espa�o] ent�o coloque seu token.
                                    Exemplo: 'Bearer 12345oiuytr'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"

                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //var urlCliente1 = "https://dominiodocliente.com.br";
            //var urlCliente2 = "https://dominiodocliente2.com.br";

            //app.UseCors(b => b.WithOrigins(urlCliente1, urlCliente2));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
