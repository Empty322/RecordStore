using Data;
using Data.Interfaces;
using Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecordStore.Controllers;
using TicTacToe.Services;

namespace RecordStore
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
			services.AddDistributedMemoryCache();
			services.AddSession();
			services.AddDbContext<ApplicationDbContext>(options => 
				options.UseMySQL(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("RecordStore")));
			services.AddTransient<IRecordRepository, RecordRepository>();
			services.AddTransient<IArtistRepository, ArtistRepository>();
			services.AddTransient<ICountryRepository, CountryRepository>();
			services.AddTransient<IGenreRepository, GenreRepository>();
			services.AddTransient<IOrderRepository, OrderRepository>();

			services.Configure<YandexOptions>(Configuration.GetSection("YandexOptions"));
			services.Configure<EmailServiceOptions>(Configuration.GetSection("Email"));
			services.AddSingleton<IEmailService, EmailService>();

			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			services.Configure<IdentityOptions>(options =>
			{
				options.Password.RequiredLength = 5;
				options.Password.RequireDigit = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireNonAlphanumeric = false;
			});

            services.AddMvc();
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
			
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

			app.UseSession();
			app.UseAuthentication();

			//ApplicationUser applicationUser = new ApplicationUser { UserName = Configuration["Admin:Login"] };
			//var result = await userManager.CreateAsync(applicationUser, Configuration["Admin:Password"]);
			//if(result.Succeeded)
			//{
			//	await roleManager.CreateAsync(new IdentityRole("Admin"));
			//	await userManager.AddToRoleAsync(applicationUser, "Admin");
			//}

			app.UseStaticFiles();

            app.UseMvc(routes =>
            {
				routes.MapRoute(
                    name: "default",
                    template: "{controller=Records}/{action=List}/{id?}");
            });

			DbInitializer.Seed(app);
		}
	}
}
