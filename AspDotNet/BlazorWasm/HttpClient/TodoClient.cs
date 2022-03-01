using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorWasm.Models;

public class TodoClient
{
    private readonly HttpClient http;

    public TodoClient(HttpClient http)
    {
        this.http = http;
    }

    public async Task<WeatherForecast[]> GetForecastAsync()
    {
        try
        {
            var resp = await http.GetAsync(
                "WeatherForecast");
            var str = await resp.Content.ReadAsStringAsync();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<WeatherForecast[]>(str);
        }
        catch
        {

            return new WeatherForecast[0];
        }
    }
}