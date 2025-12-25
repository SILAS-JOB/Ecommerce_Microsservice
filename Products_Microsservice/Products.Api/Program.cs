using BusinessLogicLayer;
using DataAcessLayer;
using FluentValidation.AspNetCore;
using Products.Api.ApiEndopoints;
using Products.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAcessLayer(builder.Configuration);
builder.Services.AddBusinessLogicLayer();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseExceptionHandlerMiddleware();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.MapProductApiEndpoints();

app.Run();
