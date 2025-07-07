using Microsoft.EntityFrameworkCore; 
using RockCampinas.Api.Data;       
using Microsoft.AspNetCore.Builder; 
using Microsoft.Extensions.DependencyInjection; 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers(); // Habilita o uso de controladores 

// Configuração do Swagger/OpenAPI (para testar  APIs)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build(); 


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
  
    dbContext.Database.Migrate();
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI();
}

// Redireciona requisições HTTP para HTTPS 
app.UseHttpsRedirection();


app.UseRouting(); 




app.MapControllers();




app.Run(); 