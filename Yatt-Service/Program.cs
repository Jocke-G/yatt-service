using Keycloak.AuthServices.Authentication;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Yatt_Service;
using Yatt_Service.Repositories;
using Yatt_Service.RepositoryInterfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddKeycloakWebApiAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services.AddDbContext<YattDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IToDoItemRepository, ToDoItemRepository>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Keycloak", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Implicit = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri("http://localhost:8082/realms/yatt/protocol/openid-connect/auth"),
                Scopes = new Dictionary<string, string>
                {
                    { "openid", "openid" },
                    { "profile", "profile" },
                    { "yatt_service.all", "yatt_service.all" },
                }
            }
        }
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Keycloak",
                    Type = ReferenceType.SecurityScheme,
                },
                In = ParameterLocation.Header,
                Name = "Bearer",
                Scheme = "Bearer",
            },
            Array.Empty<string>()
        },
    });

    //options.OperationFilter<Swashbuckle.AspNetCore.Filters.SecurityRequirementsOperationFilter>();
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

if (builder.Configuration.GetValue("UseSwagger", false))
{
    var option = new RewriteOptions();
    option.AddRedirect("^$", "swagger");
    app.UseRewriter(option);
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.yaml", "Yet Anothes Template Thing");
        options.OAuthClientId("yatt-service");
    });
    app.UseSwagger();
}

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
