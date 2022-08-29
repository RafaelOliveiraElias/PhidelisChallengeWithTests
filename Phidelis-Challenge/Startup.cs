using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Phidelis_Challenge.Context;
using Microsoft.EntityFrameworkCore;
using Phidelis_Challenge.HostedService;
using Phidelis_Challenge;

namespace Phidelis_Challenge
{
    public class Startup {
    public IConfiguration configRoot {
        get;
    }
    public Startup(IConfiguration configuration) {
        configRoot = configuration;
    }
    public void ConfigureServices(IServiceCollection services) {
        services.AddDbContext<StudentsContext>(options => 
            options.UseSqlServer("Server=tcp:servidordesafio.database.windows.net,1433;Initial Catalog=Students;Persist Security Info=False;User ID=adminDesafio;Password=mMgE@6KCsJvnh4n;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));
        services.AddScoped<IScopedService, MyScopedService>();
        services.AddHostedService<MyBackgroundService>();
        services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
    }
    public void Configure(WebApplication app, IWebHostEnvironment env) {
        if (!app.Environment.IsDevelopment()) {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
    }
    }
}