using LedMagazineBack.Context;
using LedMagazineBack.Helpers;
using LedMagazineBack.Models.TelegramModel;
using LedMagazineBack.Seeders;
using LedMagazineBack.Services.TelegramServices;
using LedMagazineBack.Services.TelegramServices.Abstract;
using LedMagazineBack.Static;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    WebRootPath = "wwwroot"
});

var connectionString = builder.Configuration.GetConnectionString("MagazineDb");
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Description = "JWT Bearer. : \"Authorization: Bearer { token } \"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[]{}
        }
    });
});
builder.Services.AddDbContext<MagazineDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddHttpContextAccessor();
builder.Services.AddStaticRepositories();
builder.Services.AddMemoryCache();
builder.Services.Configure<TelegramSettings>(builder.Configuration.GetSection("TelegramSettings"));
builder.Services.AddControllers();
builder.Services.AddHttpClient<ITelegramService, TelegramService>();
builder.Services.AddStaticServices();
builder.WebHost.UseWebRoot("wwwroot"); 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    var jwtParam = builder.
        Configuration.
        GetSection("JwtParameters").
        Get<JwtParameters>();
    var key = System.Text.Encoding.UTF32.GetBytes(jwtParam.Key);
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = jwtParam.Issuer,
        ValidateIssuer = true,
        ValidAudience = jwtParam.Audience,
        ValidateAudience = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = false,
        
    };

    

    options.Events = new JwtBearerEvents()
    {
        OnMessageReceived = context =>
        {
            var token = context.Token;

            if (string.IsNullOrEmpty(token))
            {
                token = context.Request.Query["token"];

                if (!string.IsNullOrEmpty(token))
                {
                    context.Token = token;
                }

            }

            return Task.CompletedTask;
        }
    };


});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
await DbSeeder.SeedAdminAsync(app.Services);
app.UseHttpsRedirection();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

app.Run();
