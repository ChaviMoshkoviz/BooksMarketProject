using books;
using Books.core;
using Books.core.Repositories;
using Books.core.Service;
using Books.data;
using Books.service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "נא להזין טוקן בפורמט ה-JWT בלבד (ללא המילה Bearer, המערכת מוסיפה אותה)"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
//builder.Services.AddSingleton<IDataContext, DataContext>();--
builder.Services.AddDbContext<DataContext>();

builder.Services.AddScoped<IBooksService, BooksService>();
builder.Services.AddScoped<IBooksRepository, BooksRepository>();

builder.Services.AddScoped<IListingsService, ListingsService>();
builder.Services.AddScoped<IListingsRepository, ListingsRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})


.AddJwtBearer(options =>
{
    // חילוץ המפתח מהקונפיגורציה עם הגנה (Fallback)
    var jwtKey = builder.Configuration["JWT:Key"];
    if (string.IsNullOrEmpty(jwtKey) || jwtKey.Length < 16)
    {
        // מפתח ברירת מחדל למקרה חירום (מומלץ שיהיה באורך 16 תווים לפחות)
        jwtKey = "DefaultSecretKey_PleaseChangeInSettings123!";
    }

    var keyBytes = Encoding.UTF8.GetBytes(jwtKey);
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"] ?? "DefaultIssuer",
        ValidAudience = builder.Configuration["JWT:Audience"] ?? "DefaultAudience",
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
    };
});
// פה הוספתי את מה שהיה חסר: הגדרת הרשאות (Authorization) - מה מותר לו
builder.Services.AddAuthorization(options =>
{
    // פוליסה שדורשת תפקיד מנהל בלבד
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));

    // פוליסה שמאפשרת גם למשתמש רשום וגם למנהל (לפעולות משותפות)
    options.AddPolicy("RegisteredUser", policy => policy.RequireRole("Registered", "Admin"));
});
var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseMiddleware<books.Middleware.InputSanitizationMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
