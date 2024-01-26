using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTests;

public class AzureFunctionsTestcontainersFixture : IAsyncLifetime
{
    private readonly IFutureDockerImage _azureFunctionsDockerImage;
    public IContainer AzureFunctionsContainerInstance { get; private set; }
    public AzureFunctionsTestcontainersFixture()
    {
        _azureFunctionsDockerImage = new ImageFromDockerfileBuilder()
            .WithDockerfileDirectory(CommonDirectoryPath.GetSolutionDirectory(), string .Empty)
            .WithDockerfile(Path.Combine("Api/Dockerfile"))
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _azureFunctionsDockerImage.CreateAsync();

        AzureFunctionsContainerInstance = new ContainerBuilder()
            .WithImage(_azureFunctionsDockerImage)
            .WithPortBinding(80, true)
            .WithWaitStrategy(
                Wait.ForUnixContainer()
                .UntilHttpRequestIsSucceeded(r => r.ForPort(80)))
            .Build();
        await AzureFunctionsContainerInstance.StartAsync();
    }
    public Task DisposeAsync()
        => Task.CompletedTask;
}
