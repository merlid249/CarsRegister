using Microsoft.AspNetCore.Authentication;
using RentalBooking.Tools;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication("CustomBearer")
   .AddScheme<AuthenticationSchemeOptions, CustomBearerAuthenticationHandler>("CustomBearer", null);



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
