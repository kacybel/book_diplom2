using book_diplom2.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ��������� ��������� ������������
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
    });

// ���������� EF Core � PostgreSQL
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

// ���������� ����������� �����
app.UseStaticFiles();

app.UseCors("AllowAll");

// ����������� ��������
app.UseRouting();

// �������� MapControllers � MapGet
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

// ��� ���������� ����
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