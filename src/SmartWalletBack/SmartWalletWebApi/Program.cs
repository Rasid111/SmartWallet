var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddDbContext<AppDbContext>(options =>
// {
//     var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//     options.UseNpgsql(connectionString);
// });

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.UseHttpsRedirection();

app.Run();
