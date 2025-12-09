using CakeByHtoo.CommonService;
using CakeByHtoo.DBContent;
using CakeByHtoo.Interfaces;
using CakeByHtoo.Repositories;
using Microsoft.Extensions.Logging;

namespace CakeByHtoo
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
                });

            builder.Services.AddMauiBlazorWebView();

            // DBContext
            builder.Services.AddDbContext<CakeByHtooDBContent>();

            // Repositories
            builder.Services.AddScoped<ICategory, CategoryRepo>();
            builder.Services.AddScoped<IFlavour, FlavourRepo>();
            builder.Services.AddScoped<IProduct, ProductRepo>();
            builder.Services.AddScoped<IProductSize, ProductSizeRepo>();
            builder.Services.AddScoped<ICart, CartRepo>();
            builder.Services.AddScoped<IOrderRepo, OrderRepo>();
            builder.Services.AddScoped<IProductItem, ProductItemRepo>();

#if ANDROID
            builder.Services.AddSingleton<IPrintService, Platforms.Android.Services.AndroidReceiptPrintService>();
#endif
            // Cart notification service
            builder.Services.AddSingleton<CartStateService>();

            var app = builder.Build();

            // Auto-create DB and seed ProductItems safely
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<CakeByHtooDBContent>();
                db.Database.EnsureCreated();
                db.SeedProductItems();
            }

            return app;
        }
    }

}