using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SehirRehberi.API.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SehirRehberi.API.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace SehirRehberi.API
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
            var key = Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value);
            // injection alan�: hangi nesne i�in hangi somut nesneleri ve onlar�n konfugurasyonlar�n� kullanac��z?
            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings")); //Cloudinary foto�raf y�klemek
            services.AddDbContext<DataContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddAutoMapper(typeof(Startup)); // Hata bunu yaz�nca d�zeliyor
                                                     //services.AddControllers();
            //Sonsuz referans d�ng�s�ne girerse ignore edecektir

            services.AddControllers().AddNewtonsoftJson();

            services.AddControllers()
           .AddNewtonsoftJson(options =>
           {
               options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
           });


            //services.AddControllers().AddJsonOptions(opt =>
            //{
            //    opt.JsonSerializerOptions.ReadCommentHandling = (System.Text.Json.JsonCommentHandling)Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //});
            services.AddCors();
            services.AddScoped<IAppRepository, AppRepository>();//bir controller IAppRepository isterse AppRepository getir anlam�d�r.
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {

                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false, // 
                };
            });  // response headerlara neleri koyaca��m�z� yazaca��z
                 //object p = services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                 //{
                 //    options.TokenValidationParameters = new TokenValidationParameters
                 //    {
                 //        ValidateIssuerSigningKey = true,
                 //        IssuerSigningKey = new SymmetricSecurityKey(key),
                 //        ValidateIssuer = false,
                 //        ValidateAudience = false
                 //     };
                 //});


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(hostName => true).AllowCredentials()); // taray�c�lara bana g�ven ben de sana g�veniyorum diyorsun.

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
