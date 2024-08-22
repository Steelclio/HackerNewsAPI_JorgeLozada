using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackerNewsAPI.Models;
using HackerNewsAPI.Services;

namespace HackerNewsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HackerNewsController : ControllerBase
    {
        private readonly HackerNewsService _hackerNewsService;

        // Inyectar el servicio HackerNewsService a través del constructor
        public HackerNewsController(HackerNewsService hackerNewsService)
        {
            _hackerNewsService = hackerNewsService;
        }

        // Endpoint para obtener las mejores historias
        [HttpGet("beststories")]
        public async Task<IActionResult> GetBestStories([FromQuery] int n = 10)
        {
            // Obtener los IDs de las mejores historias
            var storyIds = await _hackerNewsService.GetBestStoriesIdsAsync();

            // Obtener los detalles de las historias, limitando al número especificado
            var tasks = storyIds.Take(n).Select(id => _hackerNewsService.GetStoryDetailsAsync(id)).ToList();
            var stories = await Task.WhenAll(tasks);

            // Ordenar las historias por puntuación en orden descendente
            var sortedStories = stories.OrderByDescending(s => s.Score).ToList();

            // Devolver las historias en la respuesta de la API
            return Ok(sortedStories);
        }
    }
}
