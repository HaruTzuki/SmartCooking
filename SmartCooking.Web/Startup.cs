using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartCooking.Data.Context;
using SmartCooking.Data.Domain;
using SmartCooking.Data.Repository;
using SmartCooking.Infastructure.Dto;

namespace SmartCooking.Web
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
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IUnitRepository, UnitRepository>();
			services.AddScoped<IRecipeRepository, RecipeRepository>();
			services.AddScoped<IItemRepository, ItemRepository>();
			services.AddScoped<IItemCategoryRepository, ItemCategoryRepository>();

			services.AddDbContext<MyDbContext>();

			IMvcBuilder mvc = services.AddControllersWithViews();

#if (DEBUG)
			mvc.AddRazorRuntimeCompilation();
#endif

			services.AddRazorPages();
			services.AddSession();
			services.AddMemoryCache();
			services.AddMvc().AddRazorPagesOptions(opt =>
			{
				opt.Conventions.AddPageRoute("/Home/Index", "");
			});

			//services.AddRouting(opt =>
			//{
			//	opt.ConstraintMap["recipeCountViewModel"] = typeof(string);
			//	opt.LowercaseUrls = true;
			//});
			services.AddControllersWithViews();
			services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseSession();
			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
				endpoints.MapControllers();
				endpoints.MapControllerRoute(
				   name: "default",
				   pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}