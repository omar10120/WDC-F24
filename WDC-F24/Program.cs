using WDC_F24.Application.Interfaces;
using WDC_F24.infrastructure;
using WDC_F24.infrastructure.Data;
using WDC_F24.infrastructure.Repositories;
using WDC_F24.Application;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();



builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<IProductService, Productervice>();

//builder.Services.AddSwaggerGen(option =>
//{
//    option.SwaggerDoc("UserApp", new OpenApiInfo { Title = "User App API", Version = "2.0" });
//});


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger/index.html");
    return Task.CompletedTask;
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
