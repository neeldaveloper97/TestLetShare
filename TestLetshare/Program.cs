using TestLetshare.API.Extensions;
using FluentValidation.AspNetCore;
using TestLetshare.Application;
using TestLetshare.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddControllers();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(configuration);
builder.Services.AddAuthenticationAndJwt(configuration);
builder.Services.AddSwaggerWithJwt();
builder.Services.AddCorsPolicy();



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
