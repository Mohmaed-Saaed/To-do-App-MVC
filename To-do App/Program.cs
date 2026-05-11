using BLL.Interfaces;
using BLL.Services;
using DAL.Mapping;
using DAL.Repository.Repository;
using DAL.Repository.RepositoryInterface;
using DAL.UnitOfWork.Interface;
using DAL.UnitOfWork.UnitOFWork;
using Domain.Utilities.Interface;
using Domain.Utilities.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using To_do_App.ViewModels.TaskItemVM;
namespace To_do_App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

            builder.Services.AddDbContext<DAL.DbContexApp.DbContextApp>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<DAL.DbContexApp.DbContextApp>()
                .AddDefaultTokenProviders();

            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ITaskItemService, TaskItemService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddMemoryCache();


            builder.Services.AddMapster();

            TypeAdapterConfig.GlobalSettings.Scan(
                Assembly.GetExecutingAssembly());
            var app = builder.Build();
            // Seed initial data (roles, admin user, categories)
            To_do_App.Data.SeedData.EnsureSeedData(app.Services);
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
