using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace razorPage.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly Services.GrpcClient _client;

    public IndexModel(ILogger<IndexModel> logger, Services.GrpcClient client)
    {
        _client = client;
        _logger = logger;
    }

    public void OnGet()
    {
    }
    public void temp()
    {

        var temp =_client.GetSplitWords("要上青天揽明月");
        var temp2 =temp.Result;
    }
}
