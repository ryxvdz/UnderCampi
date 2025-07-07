using Microsoft.EntityFrameworkCore; 
using RockCampinas.Api.Data;       
using Microsoft.AspNetCore.Builder; 
using Microsoft.Extensions.DependencyInjection; 

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", 
        builder => builder.AllowAnyOrigin() 
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});



builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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


app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins"); 



app.UseRouting();

app.MapControllers();
app.Run();