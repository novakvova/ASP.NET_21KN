using Microsoft.EntityFrameworkCore;
using WebATB.Data;
using WebATB.Data.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MyContextATB>(opt => 
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Main}/{action=Index}/{id?}")
    .WithStaticAssets();

//Seed Categories items
//Хочу додати, якісь дані, щоб були у табличці tblCategories
//На потрібно отримати MyContextATB - який є налаштований вище
using var scope = app.Services.CreateScope(); //отримав scope
var myDbContext = scope.ServiceProvider.GetService<MyContextATB>(); //отримав context database

if (myDbContext != null) // якщо ми отримали КОНТЕКСТ і він не пустий
{
    if (!myDbContext.Categories.Any()) //Якщо в БД відсутні записи
    {
        List<CategoryEntity> items = new();
        items.Add(
            new() {
                Name="Морозвиво",
                Image="https://src.zakaz.atbmarket.com/cache/category/334-morozivo.webp",
                Slug="morozivo"
            });
        items.Add(new() {
                Name="Заморожені продукти",
                Image="https://src.zakaz.atbmarket.com/cache/category/%D0%97%D0%B0%D0%BC%D0%BE%D1%80%D0%BE%D0%B6%D0%B5%D0%BD%D1%96%20%D0%B2%D0%B8%D1%80%D0%BE%D0%B1%D0%B8.webp",
                Slug="zamorozheni-produkti"
        });

        items.Add(new() {
            Name="Напої безалкогольні",
            Image="https://src.zakaz.atbmarket.com/cache/category/%D0%91%D0%B5%D0%B7%D0%B0%D0%BB%D0%BA%D0%BE%D0%B3%D0%BE%D0%BB%D1%8C%D0%BD%D1%96%20%D0%BD%D0%B0%D0%BF%D0%BE%D1%96%CC%88.webp",
            Slug="napoi-bezalkogol-ni"
        });

        myDbContext.Categories.AddRange(items);
        myDbContext.SaveChanges();
    }
}

app.Run();
