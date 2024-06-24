using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TT2.API.Interfaces;
using TT2.API.Services;
using TT2.Engine.DbContexts;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TestTaskV2DbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("default"),
        b => { b.MigrationsHistoryTable("__EFMMigrationsHistory", "TestTaskV2"); });
});


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ElectronicJournal API", Version = "v1" });
    c.EnableAnnotations();
});

builder.Services.AddScoped<IEventService, EventService>();


builder.Services.AddControllers();

builder.Services.AddSwaggerGenNewtonsoftSupport();


var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "ElectronicJournal API"); });
app.UseRouting();
app.MapControllers();
app.Run();