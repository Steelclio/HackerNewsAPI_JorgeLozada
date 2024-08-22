using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HackerNewsAPI.Models;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace HackerNewsAPI.Services
{
    public class HackerNewsService
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly IMemoryCache _cache;

        public HackerNewsService(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public async Task<List<int>> GetBestStoriesIdsAsync()
        {
            // Usar cache para almacenar los IDs de las mejores historias
            return await _cache.GetOrCreateAsync("BestStoriesIds", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);  // Cache por 5 minutos
                var response = await client.GetStringAsync("https://hacker-news.firebaseio.com/v0/beststories.json");
                return JsonConvert.DeserializeObject<List<int>>(response);
            });
        }

        public async Task<Story> GetStoryDetailsAsync(int storyId)
        {
            // Usar cache para almacenar los detalles de la historia
            return await _cache.GetOrCreateAsync($"Story_{storyId}", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);  // Cache por 5 minutos
                var response = await client.GetStringAsync($"https://hacker-news.firebaseio.com/v0/item/{storyId}.json");
                dynamic storyData = JsonConvert.DeserializeObject(response);

                return new Story
                {
                    Title = storyData.title,
                    Uri = storyData.url,
                    PostedBy = storyData.by,
                    Time = DateTimeOffset.FromUnixTimeSeconds((long)storyData.time).UtcDateTime,
                    Score = storyData.score,
                    CommentCount = storyData.descendants
                };
            });
        }
    }
}
