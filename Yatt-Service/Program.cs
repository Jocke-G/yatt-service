using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Yatt_Service;
using Yatt_Service.Repositories;
using Yatt_Service.RepositoryInterfaces;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddSwaggerGen();

var app = builder.Build();

if(builder.Configuration.GetValue<bool>("UseSwagger", false))
{
    var option = new RewriteOptions();
    option.AddRedirect("^$", "swagger");
    app.UseRewriter(option);
    app.UseSwaggerUI(x =>
    {
        x.SwaggerEndpoint("/swagger/v1/swagger.yaml", "Yet Anothes Template Thing");
    });
    app.UseSwagger();
}

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
