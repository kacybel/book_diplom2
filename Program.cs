var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
// Hello GitHub
// hello 2, hello 3!

app.Run();
