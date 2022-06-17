using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace ExploreCaching.Controllers
{
    [ApiController]
    [Route("greeting")]
    public class HelloWorldController
    {
        private readonly IMemoryCache memoryCache;

        public HelloWorldController(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        [Route("{name}")]
        public string Get(string name)
        {
            if (memoryCache.TryGetValue<string>(name, out var greeting))
            {
                Console.WriteLine($"{name} is already in cache... fetching from there.");
                return greeting;
            }
            else
            {
                Console.WriteLine($"{name} is NOT in cache. Getting greeting from the database.");

                greeting = GetGreetingFromDatabase(name);

                memoryCache.Set(name, greeting);
                return greeting;
            }
        }

        private string GetGreetingFromDatabase(string name)
        {
            // This method is a pretend database interaction
            // or any other resource intensive operation
            Thread.Sleep(5000);
            return $"Hello, {name}! I was created at {DateTime.Now.ToString("HH:mm:ss")}.";
        }
    }
}
