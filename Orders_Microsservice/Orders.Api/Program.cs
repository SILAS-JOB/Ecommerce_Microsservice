using BusinessLogicLayer;
using DataAcessLayer;
using FluentValidation.AspNetCore;
using Orders.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAcessLayer(builder.Configuration);
builder.Services.AddBusinessLogicLayer(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});
var app = builder.Build();


app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseExceptionHandlerMiddleware();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();


app.Run();
