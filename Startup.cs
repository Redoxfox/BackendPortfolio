using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;
using ServerAPI.Helpers;
using ServerAPI.Interfaces;
using ServerAPI.Interfaces.InterfacesBets;
using ServerAPI.Queries.QueriesBets;
using ServerAPI.Queries.QueriesUsers;
using ServerAPI.Repositories;
using ServerAPI.Services;
using System.Net;
using System.Text;

namespace ServerAPI
{
    public static class Startup
    {
        
        

        public static WebApplication InicializarApp(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);
            var app = builder.Build();
            
            Configure(app);
            return app;
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {

            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:4200",
                                                          "https://localhost:7205/",
                                                          "http://localhost:8095",
                                                          "http://localhost:8064",
                                                          "https://digital.tuxcarlostorres.com")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();
                                  });
            });
            builder.Services.AddControllers();

           
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            var AppSettingsSection = builder.Configuration.GetSection("AppSettings:VisitorSecretKey");
            builder.Services.AddScoped<IQueriesUser, QueriesUsers>();
            builder.Services.AddScoped<IQueriesBets, QueriesBets>();
            builder.Services.AddScoped<IRegistration, RegistrationService>();
            builder.Services.AddScoped<ILogin, LoginService>();
            builder.Services.AddScoped<IValidations, Validations>();
            builder.Services.Configure<AppSettings>(AppSettingsSection);

            var KeyJwt = Encoding.ASCII.GetBytes(AppSettingsSection.Value);

            builder.Services.AddAuthentication(d =>
            {
                d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(
                    d =>
                    {
                        d.RequireHttpsMetadata = false;
                        d.SaveToken = true;
                        d.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(KeyJwt),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    }); 


            builder.Services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.KnownProxies.Add(IPAddress.Parse("161.35.103.248"));
            }
            );

            

        }

        private static void Configure(WebApplication app)
        {
        
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseCors("_myAllowSpecificOrigins");

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
