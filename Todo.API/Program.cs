using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Todo.Busines.Services;
using Todo.Data;
using Todo.Data.Interfaces;
using Todo.Data.Models;
using Todo.Data.Models.Auth;
using Todo.Services.Interfaces;
using Todo.Services.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                            ?? throw new InvalidOperationException("Connection string not found.");

builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options => options.UseSqlServer(connectionString));

// TODO: add cors

// Identity Configuration
builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>(config =>
    {
        config.Password.RequiredLength = 4;
        config.Password.RequireDigit = false;
        config.Password.RequireNonAlphanumeric = false;
        config.Password.RequireUppercase = false;
        config.SignIn.RequireConfirmedEmail = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

var authConfigRaw = builder.Configuration.GetSection("AuthConfiguration");
var authConfig = authConfigRaw?.Get<AuthConfiguration>()
                        ?? throw new InvalidOperationException("Connection string not found.");
builder.Services.Configure<AuthConfiguration>(authConfigRaw);

var key = Encoding.UTF8.GetBytes(authConfig.Secret);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        //x.IncludeErrorDetails = true;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            //NameClaimType = "username",
            ValidIssuer = authConfig.Issuer,
            //ValidAudience = authConfig.Audience,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            //ValidateIssuer = false, //This and the next parameter must be set to true during production
            //ValidateAudience = false
            ValidateIssuerSigningKey = true,
            //ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddScoped<ITodoService, TodoService>()
                .AddScoped<IAuthService, AuthService>();


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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
