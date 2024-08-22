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

        public HackerNewsController(HackerNewsService hackerNewsService)
        {
            _hackerNewsService = hackerNewsService;
        }

        [HttpGet("beststories")]
        public async Task<IActionResult> GetBestStories([FromQuery] int n = 10)
        {
            var storyIds = await _hackerNewsService.GetBestStoriesIdsAsync();

            var tasks = storyIds.Take(n).Select(id => _hackerNewsService.GetStoryDetailsAsync(id)).ToList();
            var stories = await Task.WhenAll(tasks);

            var sortedStories = stories.OrderByDescending(s => s.Score).ToList();

            return Ok(sortedStories);
        }
    }
}
