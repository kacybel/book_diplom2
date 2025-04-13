using book_diplom2.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Добавляем поддержку контроллеров
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
    });

// Подключаем EF Core с PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Подключаем статические файлы
app.UseStaticFiles();

app.UseCors("AllowAll");

// Настраиваем маршруты
app.UseRouting();

// Заменяем MapControllers и MapGet
app.MapControllers();
app.MapGet("/", async context =>
{
    if (!context.Request.IsHttps)
    {
        var httpsUrl = $"https://{context.Request.Host}/index.html";
        context.Response.Redirect(httpsUrl);
        return;
    }
    context.Response.Redirect("/index.html");
});

// Для отключения кэша
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append("Cache-Control", "no-cache, no-store, must-revalidate");
        ctx.Context.Response.Headers.Append("Pragma", "no-cache");
        ctx.Context.Response.Headers.Append("Expires", "0");
    }
});

app.Run();