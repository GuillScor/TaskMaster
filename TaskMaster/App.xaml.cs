using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskMaster.Data;
using TaskMaster.ViewModels;
using TaskMaster.Views;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace TaskMaster
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var connectionString = "server=localhost;port=3306;database=task;user=root;password=root";
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29)); // Spécifie la version de MySQL (ex : 8.0.29)

            var builder = MauiApp.CreateBuilder();

            // Configure le DbContext avec MySQL local
            builder.Services.AddDbContext<Data.AppDbContext>(dbContextOptions =>
                dbContextOptions.UseMySql(connectionString, serverVersion) // Utilise MySQL avec la version spécifiée
            );

            builder.Services.AddSingleton<AppShell>();  // Ajout du Shell de l'application

            builder.UseMauiApp<App>(); // Démarre l'application MAUI

            var app = builder.Build();
            MainPage = new AppShell();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}