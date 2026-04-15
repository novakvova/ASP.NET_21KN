using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebATB.Data;
using WebATB.Data.Entities;
using WebATB.Data.Entities.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MyContextATB>(opt => 
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//Додаємо налаштування для UserManager і RoleManager і SigninManager - займається cookies
builder.Services.AddIdentity<UserEntity, RoleEntity>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
})
    .AddEntityFrameworkStores<MyContextATB>()
    .AddDefaultTokenProviders();

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
var roleManager = scope.ServiceProvider.GetService<RoleManager<RoleEntity>>(); //
var userManager = scope.ServiceProvider.GetService<UserManager<UserEntity>>(); //


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

    //Якщо у БД немає ролей
    if(!roleManager.Roles.Any())
    {
        RoleEntity[] roles = {
            new () { Name = "Admin" },
            new () { Name = "Manager" },
            new () { Name = "User" }
            };
        foreach(var role in roles)
            await roleManager.CreateAsync(role);
    }

    if(!userManager.Users.Any())
    {
        var admin = new UserEntity
        {
            Email = "admin@gmail.com",
            UserName = "admin@gmail.com",
            FirstName = "Іван",
            LastName = "Мельник",
            Image = "default.jpg"
        };
        var result = await userManager.CreateAsync(admin, "123456");
        if (result.Succeeded)
            await userManager.AddToRoleAsync(admin, "Admin");
    }
}

app.Run();
