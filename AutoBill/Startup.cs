using AutoBill.Data;
using AutoBill.Email;
using AutoBill.Models;
using AutoBill.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBill
{
    public class Startup
    {
        private static string adminEmail;
        private static string adminPassword;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //public Startup(IHostingEnvironment env)
        //{
        //    var builder = new ConfigurationBuilder();

        //    if (env.IsDevelopment())
        //    {
        //        builder.AddUserSecrets<Startup>();
        //    }

        //    Configuration = builder.Build();
        //}


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //.AddCookie()
            //.AddJwtBearer(jwtBearerOptions =>
            //{
            //    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
            //    {
            //        ValidateActor = false,
            //        ValidateAudience = false,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = Configuration["Token:Issuer"],
            //        ValidAudience = Configuration["Token:Audience"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:Key"]))
            //    };
            //});

            //var context = new ApplicationDbContext(optionsBuilder);
            //context.Database.EnsureCreated();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddNodeServices();// this is in package Microsoft.AspNetCore.NodeServices
            services.AddMvc();
            services.AddScoped<IBillService, BillService>();

            services.AddSingleton<IEmailConfiguration>(Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
            services.AddTransient<IEmailSender, EmailSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                              IHostingEnvironment env,
                              UserManager<ApplicationUser> userManager,
                              RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                adminEmail = Configuration["AdminEmail"];
                adminPassword = Configuration["AdminPassword"];
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // Make sure there's a test admin account
            EnsureRolesAsync(roleManager).Wait();
            EnsureTestAdminAsync(userManager).Wait();
            DatabaseSetup.EnsureDatabaseNotEmptyAsync().Wait();
            app.UseStaticFiles();

            app.UseAuthentication();
            //app.UseMvcWithDefaultRoute(); this will effect Save action.
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var alreadyExists = await roleManager.RoleExistsAsync(Constants.AdministratorRole);
            if (alreadyExists)
                return;

            await roleManager.CreateAsync(new IdentityRole(Constants.AdministratorRole));
        }

        private static async Task EnsureTestAdminAsync(UserManager<ApplicationUser> userManager)
        {
            var testAdmin = await userManager.Users
                                             .Where(x => x.UserName == adminEmail)
                                             .SingleOrDefaultAsync();

            if (testAdmin != null)
                return;


            testAdmin = new ApplicationUser { UserName = adminEmail, Email = adminEmail, SecurityStamp = DateTime.Now.ToString() };
            await userManager.CreateAsync(testAdmin, adminPassword);
            await userManager.AddToRoleAsync(testAdmin, Constants.AdministratorRole);
        }

        
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlite(connectionString);
            return new ApplicationDbContext(builder.Options);
        }
    }



}
