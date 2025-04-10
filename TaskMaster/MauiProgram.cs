using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskMaster.Data;
using TaskMaster.ViewModels;
using TaskMaster.Views;

namespace TaskMaster
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            var connectionString = "server=localhost;port=3306;database=task;user=root;password=root";
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(connectionString, serverVersion));

            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<AppShell>();

            return builder.Build();
        }
    }
}
