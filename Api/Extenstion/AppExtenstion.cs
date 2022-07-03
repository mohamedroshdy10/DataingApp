using System.Text;
using Api.DataContext;
using Api.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Api.Extenstion
{
    public static class AppExTesntions
    {
        public static IServiceCollection ApplicarionServices(this IServiceCollection services,IConfiguration config)
        {
              services.AddDbContext<AppDBContext>(op=>{
                op.UseSqlite(config.GetConnectionString("DefaulteConnection"));
            });
             
            return services;
        }
       public static IServiceCollection IdentitiyServices(this IServiceCollection services,IConfiguration config)
       {
            services.AddScoped(typeof(ITokenServices),typeof(TokenServices));
                    //install JWTBearr
           services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options=>{
                        options.TokenValidationParameters=new TokenValidationParameters{
                       ValidateIssuerSigningKey=true,
                      IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                     ValidateIssuer=false,
                    ValidateAudience=false,
                        };
                    });
                    return services;
        }
        
    }
}