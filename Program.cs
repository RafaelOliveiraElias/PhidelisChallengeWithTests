using Phidelis_Challenge.Context;
using Microsoft.EntityFrameworkCore;
using Phidelis_Challenge.HostedService;
using Phidelis_Challenge;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<StudentsContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexãoPadrão")));

builder.Services.AddScoped<IScopedService, MyScopedService>();
builder.Services.AddHostedService<MyBackgroundService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
