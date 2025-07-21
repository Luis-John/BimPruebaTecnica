using BIM.PruebaTecnica.Entities.Options;
using BIM.PruebaTecnica.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapis
//builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddBimServices(
    dbOptions =>
    builder.Configuration.GetSection(DBOptions.SectionKey).Bind(dbOptions),
    aesOptions =>
    builder.Configuration.GetSection(AesOptions.SectionKey).Bind(aesOptions)
);

builder.Services.AddControllers();

#region Authentication Scheme Configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ABCDEF1234567890ABCDEF1234567890ABCDEF1234567890ABCDEF1234567890")),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidAudience = "BIM.com",
        ValidIssuer = "BIM.com",
        ClockSkew = TimeSpan.Zero
    };
});
#endregion
#region Swagger Configuration
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("BearerToken",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = "Proporciona el valor del Token",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer"
        });

    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "BearerToken"
                    }
                },
                Array.Empty<string>()
            }
        });
});
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthentication();
app.UseAuthorization();
app.UseCors();

app.MapControllers();
app.Run();

