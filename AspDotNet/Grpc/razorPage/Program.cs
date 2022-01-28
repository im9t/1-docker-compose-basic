using razorPage.Models;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<TodoContext>(opt =>
               opt.UseNpgsql(builder.Configuration.GetConnectionString("psqldev")));
}
else
{
    builder.Services.AddDbContext<TodoContext>(opt =>
               opt.UseNpgsql(builder.Configuration.GetConnectionString("psql")));
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

try
{
    using var serviceScop = app.Services.CreateScope();
    var context = serviceScop.ServiceProvider.GetRequiredService<TodoContext>();
    if(context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }

}
catch
{
}

app.Run();
//dotnet-aspnet-codegenerator razorpage -m TodoItem -dc TodoContext -udl -outDir Pages/TodoItems --referenceScriptLibraries -sqlite