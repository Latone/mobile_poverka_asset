using mobile_poverka_asset.Services;
using mobile_poverka_asset.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using mobile_poverka_asset.Models;
using mobile_poverka_asset.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using mobile_poverka_asset.Database;


namespace mobile_poverka_asset
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<IMessage, MessageAndroid>();
            //Encoding
            
            MainPage = new AppShell();
        }
        /*public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PoverkaContext>(options =>
                options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection"),
                ef => ef.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddControllers();
        }*/
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
