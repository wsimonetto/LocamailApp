using LocamailApp.Data;
using LocamailApp.Data.Repositories;
using LocamailApp.Data.Repository;
using LocamailApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurações do MongoDB
builder.Services.Configure<DataBaseSettings>(
    builder.Configuration.GetSection("Database"));

// Adiciona o contexto do MongoDB
builder.Services.AddScoped<MongoDBContext>();

// Registro das IServices
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IEmailEnviadoService, EmailEnviadoService>();
builder.Services.AddScoped<IEmailEnviadoRepository, EmailEnviadoRepository>();
builder.Services.AddScoped<IEmailRecebidoService, EmailRecebidoService>();
builder.Services.AddScoped<IEmailRecebidoRepository, EmailRecebidoRepository>();

// Adicione serviços ao contêiner.
builder.Services.AddControllers();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
