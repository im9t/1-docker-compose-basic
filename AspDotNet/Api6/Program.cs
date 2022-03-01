using Microsoft.EntityFrameworkCore;
using Api6.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TodoContext>(opt =>
               opt.UseNpgsql(builder.Configuration.GetConnectionString("psqllocal")));
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
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
app.UseCors();
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
