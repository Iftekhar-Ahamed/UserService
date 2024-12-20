using UserService.ApiEndPoints;
using UserService.Middlewares;
using UserService.ServiceConfiguration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureAllServices(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGroup("api/User")
    .MapUserApis()
    .WithTags("User API");
app.MapGroup("api/Auth")
    .MapAuthApis()
    .WithTags("Auth API");

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<ValidationMiddleware>();

app.Run();