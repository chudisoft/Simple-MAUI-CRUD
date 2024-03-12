using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SimpleMAUICRUD.Classes;
using SimpleMAUICRUD.factories;
using SimpleMAUICRUD.pages;
using SimpleMAUICRUD.pages.tabs;

namespace SimpleMAUICRUD
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<AppDb>();
            builder.Services.AddSingleton<IItemPageFactory, ItemPageFactory>();
            builder.Services.AddTransient<CreateItemPage>();
            builder.Services.AddTransient<ItemDetailPage>();
            builder.Services.AddTransient<ItemListPage>();
            builder.Services.AddTransient<DashboardPage>();
            //builder.Services.AddSingleton<DashboardPage>();
            builder.Services.AddSingleton<TabPage1>();
            builder.Services.AddSingleton<TabPage2>();
            builder.Services.AddSingleton<TabPage3>();

            builder.Services.AddTransient<MainFlyoutPage>();


            builder.Services.AddSingleton<AppShell>();

            return builder.Build();
        }
    }
}
