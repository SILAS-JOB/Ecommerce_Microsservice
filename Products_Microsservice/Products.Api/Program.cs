using BusinessLogicLayer;
using DataAcessLayer;
using FluentValidation.AspNetCore;
using Products.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAcessLayer(builder.Configuration);
builder.Services.AddBusinessLogicLayer();


builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

app.UseExceptionHandlerMiddleware();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
