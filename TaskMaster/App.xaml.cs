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
        public App(AppShell appShell) // Injecté depuis DI
        {
            InitializeComponent();
            MainPage = appShell;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            try
            {
                return new Window(MainPage); // Ou AppShell si utilisé
            }
            catch (Exception ex)
            {
                Console.WriteLine("Crash dans CreateWindow : " + ex.Message);
                Console.WriteLine(ex.ToString());
                throw; // Pour voir la stacktrace dans VS
            }
        }
    }
}