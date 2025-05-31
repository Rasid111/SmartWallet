using Microsoft.EntityFrameworkCore;
using SmartWalletWebApi.DB;
using SmartWalletWebApi.Repositories;
using SmartWalletWebApi.Repositories.Base;
using SmartWalletWebApi.Service;
using SmartWalletWebApi.Service.Base;

var builder = WebApplication.CreateBuilder(args);

<<<<<<< HEAD
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddDbContext<AppDbContext>(options =>
// {
//     var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//     options.UseNpgsql(connectionString);
// });

var app = builder.Build();

=======

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(connectionString);
});
var app = builder.Build();


>>>>>>> 1b56dd6681ac02e24041607335ce0b2dfa94e1e2
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.UseHttpsRedirection();

<<<<<<< HEAD
app.Run();
=======

app.Run();


>>>>>>> 1b56dd6681ac02e24041607335ce0b2dfa94e1e2
