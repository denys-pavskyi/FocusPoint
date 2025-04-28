using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using FocusPoint.DAL.Configurations;
using FocusPoint.DAL.Repositories.Interfaces;
using FocusPoint.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FocusPoint.BLL.Other;
using AutoMapper;
using FocusPoint.BLL.Interfaces;
using FocusPoint.BLL.Services;
using FocusPoint.PL.Views;
using FocusPoint.PL.ViewModels;

namespace FocusPoint.PL
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public static IHost? AppHost { get; private set; }


        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    string? connectionString = hostContext.Configuration.GetConnectionString("FocusPointDB");

                    services.AddDbContext<AppDbContext>(options =>
                        options.UseNpgsql(connectionString));

                    // Repositories
                    services.AddScoped<IMainNoteRepository, MainNoteRepository>();
                    services.AddScoped<ISavedNoteRepository, SavedNoteRepository>();
                    services.AddScoped<ITaskItemRepository, TaskItemRepository>();
                    services.AddScoped<IUserRepository, UserRepository>();
                    services.AddScoped<IWorkSessionRepository, WorkSessionRepository>();
                    services.AddScoped<IWorkStatisticRepository, WorkStatisticRepository>();
                    services.AddScoped<IUserSettingRepository, UserSettingRepository>();

                    // Services list
                    services.AddScoped<IMainNoteService, MainNoteService>();
                    services.AddScoped<ISavedNoteService, SavedNoteService>();
                    services.AddScoped<ITaskItemService, TaskItemService>();
                    services.AddScoped<IUserService, UserService>();
                    services.AddScoped<IWorkSessionService, WorkSessionService>();
                    services.AddScoped<IWorkStatisticService, WorkStatisticService>();
                    services.AddScoped<IUserSettingsService, UserSettingsService>();



                    // ViewModels
                    services.AddScoped<MainViewModel>();
                    services.AddScoped<AuthViewModel>();


                    // Views
                    services.AddTransient<MainView>();
                    services.AddTransient<AuthView>();
                    services.AddTransient<SettingsView>();


                    // Other

                    var mapperConfig = new MapperConfiguration(mc =>
                    {
                        mc.AddProfile(new MapperProfile());
                    });

                    var mapper = mapperConfig.CreateMapper();
                    services.AddSingleton(mapper);

                })
                .Build();

        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();

            var authViewModel = AppHost.Services.GetRequiredService<AuthViewModel>();
            var authWindow = AppHost.Services.GetRequiredService<AuthView>();

            authWindow.DataContext = authViewModel;
            authViewModel.OnRequestClose += (s, e) => authWindow.Close();

            authWindow.Show();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();

            base.OnExit(e);
        }
    }

}
