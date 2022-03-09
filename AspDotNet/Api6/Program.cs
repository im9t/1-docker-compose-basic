using Microsoft.EntityFrameworkCore;
using Api6.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
Console.WriteLine("============Setting connection string============");
try
{
    bool folderExists = Directory.Exists("/run/secrets/");
    Console.WriteLine($"Folder secrets exsits: {folderExists}");
    var files =  Directory.GetFiles("/run/secrets/");
    foreach(var file in files)
    {
        Console.WriteLine($"Folder secrets exsits: {file}");
    }
    var password = File.ReadAllText("/run/secrets/db_password");
    var connectionString = $"User ID=postgres;Password={password};Host=postgres;Port=5432;Database=myDataBase;";
    builder.Services.AddDbContext<TodoContext>(opt =>
                   opt.UseNpgsql(connectionString));

}
catch
{
    Console.WriteLine("============Exceptioned============");
    var connectionString = $"User ID=postgres;Password=abc123;Host=postgres;Port=5432;Database=myDataBase;";
    builder.Services.AddDbContext<TodoContext>(opt =>
                   opt.UseNpgsql(connectionString));

}
Console.WriteLine("============Setting connection string done============");
builder.Services.AddCors(options =>
{
    options.AddPolicy("testcors", policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors("testcors");
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
