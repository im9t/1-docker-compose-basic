namespace razorPage.Services;
using System.Threading.Tasks;
using Grpc.Net.Client;
using razorPage.Service;

public class GrpcClient
{
    private GrpcChannel myChannel;
    private WordSplitter.WordSplitterClient client;
    public GrpcClient(IApplicationBuilder builder)
    {
        myChannel = GrpcChannel.ForAddress("http://localhost:50051");
        // myChannel = GrpcChannel.ForAddress(builder.);
        client =  new WordSplitter.WordSplitterClient(myChannel); 
    }

    public async Task<IEnumerable<string>> GetSplitWords(string inputString)
    {
        var reply = await client.GetSplittedAsync(new OriginString(){
            Origin = inputString
        });
        return reply.KeyWords_;
    }
}
