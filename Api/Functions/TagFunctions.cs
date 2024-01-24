using System.Net;
using HotChocolate.AzureFunctions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using MimeMapping;
using Newtonsoft.Json;
using Shared;

namespace Api;

public class TagFunctions
{
    private readonly ILogger<TagFunctions> _logger;
    private readonly Context _context;
    private readonly IGraphQLRequestExecutor _executor;

    public TagFunctions(ILogger<TagFunctions> logger, Context context, IGraphQLRequestExecutor executor)
    {
        _logger = logger;
        _context = context;
        _executor = executor;
    }

    [Function("GraphQLHttpFunction")]
    public Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "graphql/{**slug}")] HttpRequestData request)
		=> _executor.ExecuteAsync(request);
    
    
    [Function("Add")]
    [OpenApiOperation("Function-Add", "Add", Description = "Adicina uma nova tag")]
    [OpenApiRequestBody(KnownMimeTypes.Json, typeof(TagDto))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, KnownMimeTypes.Json, typeof(Tag))]
    public async  Task<HttpResponseData> Add([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
    {
        _logger.LogInformation("Add Tag chamado");

        var body = await new StreamReader(req.Body).ReadToEndAsync();

        var dto = JsonConvert.DeserializeObject<TagDto>(body);
        if(dto == null)
            return req.CreateResponse(HttpStatusCode.BadRequest);

        var model = (Tag)dto;
        _context.Tags.Add(model);
        _context.SaveChanges();


        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(model, HttpStatusCode.Created);

        return response;
    }
}
