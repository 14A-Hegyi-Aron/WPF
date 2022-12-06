using Microsoft.EntityFrameworkCore;
using ReceptionHour.Data;
using System.Configuration;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ReceptionHourDbContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("db"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("db")));
});

#if DEBUG
builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS",
        builder => builder.SetIsOriginAllowed(origin => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .Build());
});
#endif
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#if DEBUG
app.UseCors("EnableCORS");
#endif
app.UseAuthorization();

app.MapControllers();

app.Run();
