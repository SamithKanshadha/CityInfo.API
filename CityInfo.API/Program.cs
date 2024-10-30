using System.Xml.Schema;
using Microsoft.Extensions.Options;
using Serilog;

//loging to file using serilog
Log.Logger = new LoggerConfiguration().
    MinimumLevel.Debug().
    WriteTo.Console().
    WriteTo.File("logs/cityinfo.text", rollingInterval: RollingInterval.Day).
    CreateLogger();
    
var builder = WebApplication.CreateBuilder(args);
// builder.Logging.ClearProviders();
// builder.Logging.AddConsole();
builder.Host.UseSerilog();
// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();

//exception handling
builder.Services.AddProblemDetails();

//manipulate error responses  
// builder.Services.AddProblemDetails(options =>
// {
//     options.CustomizeProblemDetails = ctx =>
//     {
//         ctx.ProblemDetails.Extensions.Add("Additional Information", "additional information"); // this gives additional details about the error
//         ctx.ProblemDetails.Extensions.Add("server", Environment.MachineName);// this gives server name
//     };
// });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();