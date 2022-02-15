using Data_Access_Layer.Audit;
using Data_Access_Layer.MoviesPortal;
using Data_Access_Layer.Users;
using Data_Access_Layer.Users.Identity;
using Data_Access_Layer.Users.Identity.UserClaim;
using Manager;
using Manager.Helper;
using Manager.Managers.MoviesManager;
using Manager.Managers.RoleManager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviePortal
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
            services.AddMvc();

            services.AddScoped<IAccountManager, AccountManager>();
            services.AddScoped<IRoleManager, RoleManager>();
            services.AddScoped<IMoviesManager, MoviesManager>();
            services.AddScoped<NadjiUsera>();
            services.AddScoped<Audit>();


            services.AddDbContext<UsersMovieContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnectionString_UsersMovie")));
            services.AddDbContext<AuditContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnectionString_Audit")));
            services.AddDbContext<MoviesPortalContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnectionString_MoviePortal")));


            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 2;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            }
            ).AddEntityFrameworkStores<UsersMovieContext>();

            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, UserClaims>();

            //UserClaims
            services.AddAuthorization(options => options.AddPolicy(
                                      "UserActive", policy => policy.RequireAssertion(x =>
                                      x.User.Identity.Active())));

       

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/Error/ErrorPage");
                //app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/ErrorPage");
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
