using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        using HttpClient client = new HttpClient();

        try
        {
            string url = "http://api.quotable.io/random";

            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Throw if not 200 OK

            string responseBody = await response.Content.ReadAsStringAsync();

            // Parse JSON
            using JsonDocument doc = JsonDocument.Parse(responseBody);
            JsonElement root = doc.RootElement;

            string content = root.GetProperty("content").GetString();
            string author = root.GetProperty("author").GetString();

            Console.WriteLine($"\n\"{content}\"\n— {author}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Something went wrong: {e.Message}");
        }
    }
}
