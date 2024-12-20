using UserService.Extensions;
using UserService.Middlewares;
using UserService.ServiceConfiguration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureAllServices(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod() 
            .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllPolicy");

app.RegisterApiRoutes();
app.UseMiddleware<ExceptionMiddleware>();

app.Run();