using Api;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Execution.Processing;
using Microsoft.Extensions.DependencyInjection;

namespace ApiTests;

public class GraphQLServerFixture
{
    private readonly IServiceProvider _services;
    private readonly RequestExecutorProxy _executor;

    public GraphQLServerFixture()
    {
        
        _services = new ServiceCollection()
            .AddGraphQLServer()
            .AddQueryType<Query>()
            .Services
            .AddSingleton(
                sp => new RequestExecutorProxy(
                    sp.GetRequiredService<IRequestExecutorResolver>(),
                    Schema.DefaultName))
            .BuildServiceProvider();

        _executor = _services.GetRequiredService<RequestExecutorProxy>();
    }

    public async Task<IExecutionResult> SendRequest(Action<IQueryRequestBuilder> configureRequest, CancellationToken cancellationToken = default)

    {
        await using var scope = _services.CreateAsyncScope();
        var requestBuilder = new QueryRequestBuilder();
        requestBuilder.SetServices(scope.ServiceProvider);
        configureRequest(requestBuilder);
        var request = requestBuilder.Create();

        await using var result = await _executor.ExecuteAsync(request, cancellationToken);

        result.ExpectQueryResult();

        return result;
    }

    
        
}
