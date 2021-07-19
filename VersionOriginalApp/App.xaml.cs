using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using VersionOriginalApp.Services.Interfaces;

namespace VersionOriginalApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;
        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient("versionOriginalApi", client =>
            {
                client.BaseAddress =
                    new Uri("http://localhost:8080/api/");
            });
            services.AddTransient<IVersionOriginalApiService, Services.VersionOriginalApiService>();
            services.AddSingleton<MainWindow>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow?.Show();
        }
    }
}
