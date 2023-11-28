using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using TrilhasBrasil.API.Data;
using TrilhasBrasil.API.Profiles;
using TrilhasBrasil.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .MinimumLevel.Information()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//habilitar o uso do jwt bear no swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "TrilhasBrasilApi", Version = "v1" });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = JwtBearerDefaults.AuthenticationScheme
            },
            Scheme = "Oauth2",
            Name = JwtBearerDefaults.AuthenticationScheme,
            In = ParameterLocation.Header
        },
        new List<string>()
        }
    });
});

builder.Services.AddDbContext<TrilhasBrasilDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TrilhasBrasilConnectionString"))); //injeção de contexto do banco de dados
builder.Services.AddDbContext<TrilhasBrasilAuthDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TrilhasBrasilAuthConnectionString")));
builder.Services.AddScoped<IEstadoRepository, EstadosRepository>(); //injeção do respoitório de estados
builder.Services.AddScoped<ITrilhasRepository, TrilhasRepository>();//injeção do respositório de trilhas
builder.Services.AddScoped<ITokenRepository, TokenRepository>();

builder.Services.AddAutoMapper(typeof(AutorMapperProfiles));

builder.Services.AddIdentityCore<IdentityUser>() // injeção do identity para roles de usuários
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("TrilhasBrasil")
    .AddEntityFrameworkStores<TrilhasBrasilAuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 5;
    options.Password.RequiredUniqueChars = 0;
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
