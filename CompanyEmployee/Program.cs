using AutoMapper;
using CompanyEmployee.Extentions;
using Contracts;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().
    AddApplicationPart(typeof(CompanyEmployeePresentation.
    AssemplyReference).Assembly);
builder.Services.ConfiguerCors();
builder.Services.ConfigurIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositoryManger();
builder.Services.ConfigureServiceManger();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));




var app = builder.Build();

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(),
     "/Logs/nlog.config"));

// Configure the HTTP request pipeline.
var logger= app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);
if (app.Environment.IsProduction())
    app.UseHsts();


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseForwardedHeaders( new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
