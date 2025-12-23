using Ecommerce.Api.Middlewares;
using Ecommerce.Core;
using Ecommerce.Core.Mappers;
using Ecommerce.Infrastructure;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using System.Data;
using Npgsql;


var builder = WebApplication.CreateBuilder(args);
var AllowedOrigins = "_AllowedOrigins";

builder.Services.AddInfraestructure();
builder.Services.AddCore();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddAutoMapper(typeof(ApplicationUserMappingProfile));

builder.Services.AddFluentValidationAutoValidation();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("https://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});


var app = builder.Build();

app.UseExceptionHandlerMiddleware();


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();
app.Run();
