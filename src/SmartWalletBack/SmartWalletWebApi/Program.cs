using Microsoft.EntityFrameworkCore;
using SmartWalletWebApi.DB;
using SmartWalletWebApi.Repositories;
using SmartWalletWebApi.Repositories.Base;
using SmartWalletWebApi.Service;
using SmartWalletWebApi.Service.Base;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

builder.Services.AddScoped<IincomeRepository, IncomeRepository>();
builder.Services.AddScoped<IincomeService, IncomeService>();

builder.Services.AddControllers();
builder.Services.AddHttpClient<OpenRouterService>();
builder.Services.AddScoped<AiAnalysisService>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(connectionString);
});


builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "FinancialAssistantPolicy",
        policyBuilder =>
        {
            policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        }
    );
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.UseHttpsRedirection();

app.UseCors("FinancialAssistantPolicy");

app.Run();
