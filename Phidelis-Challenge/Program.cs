using Phidelis_Challenge.Context;
using Microsoft.EntityFrameworkCore;
using Phidelis_Challenge.HostedService;
using Phidelis_Challenge;
using Microsoft.OpenApi.Models;
using System.Reflection;

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
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Phidelis Students Api",
        Description = "API Crud de estudantes matriculados na escola, que simula as matrículas realizadas na instituição instantaneamente. \n\nPeriodicamente, a API adiciona 5 estudantes com dados aleatórios no banco de dados do Azure. A periodicidade pode ser editada no '/Students/SetTimer'.  \n\n **Informações importantes**: \n\n **-** Timer setado para um valor maior que 600000 segundos faz com que a aplicação não atualize o banco de dados automaticamente.\n\n **-** Atualização automática possui um valor máximo de 100 estudantes no banco de dados.",
        Contact= new OpenApiContact
        {
            Name = "Contato do autor:",
            Url = new Uri("https://www.linkedin.com/in/rafael-oliveira-elias/")
        },
    });

    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


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
