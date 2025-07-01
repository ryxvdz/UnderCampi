using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Para usar ToListAsync, FindAsync, etc.
using RockCampinas.Api.Data;         // Para acessar o ApplicationDbContext
using RockCampinas.Api.Models;       // Para usar a classe NoticiaShow

namespace RockCampinas.Api.Controllers
{
    [Route("api/[controller]")] // Define a rota base para este controlador: /api/NoticiasShows
    [ApiController]             // Indica que esta classe é um controlador de API
    public class NoticiasShowsController : ControllerBase
    {
        private readonly ApplicationDbContext _context; // Campo para o contexto do banco de dados

        // Construtor: O ASP.NET Core injetará o ApplicationDbContext aqui
        public NoticiasShowsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/NoticiasShows
        // Retorna todas as notícias de shows
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoticiaShow>>> GetNoticiasShows()
        {
            // Verifica se a tabela NoticiasShows tem dados, se não, retorna NotFound
            if (_context.NoticiasShows == null)
            {
                return NotFound("Nenhuma notícia de show encontrada.");
            }
            // Retorna todas as notícias como uma lista assíncrona
            return await _context.NoticiasShows.ToListAsync();
        }

        // GET: api/NoticiasShows/5
        // Retorna uma notícia de show específica pelo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<NoticiaShow>> GetNoticiaShow(int id)
        {
            if (_context.NoticiasShows == null)
            {
                return NotFound("O conjunto de notícias de shows não está disponível.");
            }

            // Busca a notícia pelo ID
            var noticiaShow = await _context.NoticiasShows.FindAsync(id);

            if (noticiaShow == null)
            {
                return NotFound($"Notícia de show com ID {id} não encontrada."); // Retorna 404 se não encontrar
            }

            return noticiaShow; // Retorna a notícia encontrada
        }

        // POST: api/NoticiasShows
        // Cria uma nova notícia de show
        [HttpPost]
        public async Task<ActionResult<NoticiaShow>> PostNoticiaShow(NoticiaShow noticiaShow)
        {
            if (_context.NoticiasShows == null)
            {
                return Problem("Entidade 'NoticiasShows' é nula.");
            }

            // Adiciona a nova notícia ao contexto do banco de dados
            _context.NoticiasShows.Add(noticiaShow);
            // Salva as mudanças no banco de dados
            await _context.SaveChangesAsync();

            // Retorna 201 Created e a URL para acessar a nova notícia
            return CreatedAtAction(nameof(GetNoticiaShow), new { id = noticiaShow.Id }, noticiaShow);
        }

        // PUT: api/NoticiasShows/5
        // Atualiza uma notícia de show existente
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNoticiaShow(int id, NoticiaShow noticiaShow)
        {
            // Verifica se o ID na rota corresponde ao ID da notícia no corpo da requisição
            if (id != noticiaShow.Id)
            {
                return BadRequest("O ID da rota não corresponde ao ID da notícia."); // Retorna 400 Bad Request
            }

            // Marca a notícia como modificada no contexto do banco de dados
            _context.Entry(noticiaShow).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(); // Tenta salvar as mudanças
            }
            catch (DbUpdateConcurrencyException)
            {
                // Se a notícia não existir (ou já tiver sido deletada), retorna 404
                if (!_context.NoticiasShows.Any(e => e.Id == id))
                {
                    return NotFound($"Notícia de show com ID {id} não encontrada para atualização.");
                }
                else
                {
                    throw; // Re-lança outras exceções de concorrência
                }
            }

            return NoContent(); // Retorna 204 No Content (sucesso sem retorno de corpo)
        }

        // DELETE: api/NoticiasShows/5
        // Deleta uma notícia de show
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNoticiaShow(int id)
        {
            if (_context.NoticiasShows == null)
            {
                return NotFound("O conjunto de notícias de shows não está disponível.");
            }

            // Busca a notícia pelo ID para deletar
            var noticiaShow = await _context.NoticiasShows.FindAsync(id);
            if (noticiaShow == null)
            {
                return NotFound($"Notícia de show com ID {id} não encontrada para exclusão.");
            }

            // Remove a notícia do contexto e salva as mudanças
            _context.NoticiasShows.Remove(noticiaShow);
            await _context.SaveChangesAsync();

            return NoContent(); // Retorna 204 No Content (sucesso sem retorno de corpo)
        }
    }
}