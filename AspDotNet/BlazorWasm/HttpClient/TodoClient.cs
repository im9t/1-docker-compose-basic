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

    public async Task AddTodoItem(string content)
    {
        await http.PostAsync("api/TodoItems", JsonContent.Create<TodoItem>(new TodoItem{
            Name = content
        }));
    }
    public async Task UpdateItem(TodoItem todo)
    {
        object[] list = new object[2]{todo.Id, todo};
        await http.PutAsync("api/TodoItems", JsonContent.Create<Object[]>(list));
    }
    public async Task<List<TodoItem>> GetTodoList()
    {
        try
        {
            var resp = await http.GetAsync("api/TodoItems");
            var str = await resp.Content.ReadAsStringAsync();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<TodoItem>>(str);

        }
        catch
        {
            return new List<TodoItem>();
        }
    }


}