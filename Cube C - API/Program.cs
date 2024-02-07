using System.Text;
using Cube_C___API;
using Cube_C___API.Models;
using Cube_C___API.Repositories;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

Env.Load();
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT" // spécifie le format du token
    });

    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    };

    c.AddSecurityRequirement(securityRequirement);
});


builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
    options.UseMySQL(Environment.GetEnvironmentVariable("connectionString")));
builder.Services.AddScoped<IRepositoryData<Role>, RoleRepository>();
builder.Services.AddScoped<IRepositoryData<Customer>, CustomerRepository>();
builder.Services.AddScoped<CustomerRepository>();

builder.Services.AddScoped<CartRepository>();
builder.Services.AddScoped<JwtAuthenticationService>();
builder.Services.AddScoped<IRepositoryData<Product>, ProductRepository>();
builder.Services.AddScoped<IRepositoryData<Supplier>, SupplierRepository>();
builder.Services.AddScoped<IRepositoryData<User>, UserRepository>();
builder.Services.AddScoped<UserRepository>();


builder.Services.AddSession();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("Jwt-secret")))
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
        policy => { policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod(); });
});

builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseSession();

app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

app.Run();