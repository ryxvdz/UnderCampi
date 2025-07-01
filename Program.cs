using Microsoft.EntityFrameworkCore; // Já estava, mas só confirmando
using RockCampinas.Api.Data;       // Já estava, mas só confirmando
using Microsoft.AspNetCore.Builder; // Adicione esta linha para usar App.UseHttpsRedirection()
using Microsoft.Extensions.DependencyInjection; // Para GetRequiredService

var builder = WebApplication.CreateBuilder(args);

// Adicione o serviço do DbContext para usar PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllers(); // Habilita o uso de controladores (suas APIs)

// Configuração do Swagger/OpenAPI (para testar suas APIs)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build(); // Apenas UMA VEZ!

// Opcional: Aplica as migrações do banco de dados automaticamente na inicialização
// Útil para desenvolvimento, mas cuidado em produção.
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // Esta linha irá criar o banco de dados e as tabelas se elas não existirem
    // com base nas suas migrações e modelos.
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Habilita a interface do Swagger UI em desenvolvimento
    app.UseSwaggerUI();
}

// Redireciona requisições HTTP para HTTPS (boa prática de segurança)
app.UseHttpsRedirection();

// Habilita o roteamento para os seus controladores
app.UseRouting(); // Importante para que as rotas dos seus controladores funcionem

// Habilita a autorização (será importante para seus administradores depois)
// app.UseAuthorization(); // Adicione isso quando for implementar autenticação/autorização

// Mapeia os controladores (suas APIs)
app.MapControllers();

// O trecho do "weatherforecast" e "summaries" é do template padrão.
// Você pode remover ele para deixar seu código mais limpo, já que não será usado.
// Vou remover aqui para simplificar.

// app.MapGet("/weatherforecast", () => { /* ... */ });
// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary) { /* ... */ }


app.Run(); // Inicia a aplicação