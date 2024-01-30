using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Model.Common;
using Ductus.FluentDocker.Model.Compose;
using Ductus.FluentDocker.Services;
using Ductus.FluentDocker.Services.Impl;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTests;

public class DockerComposeHostFixture : DockerComposeTestBase
{


    private Uri _baseAddress = new Uri("http://localhost:4040/api/");

    public HttpClient GetHttpClient()
    {

       
        var httpClient = new HttpClient
        {
            BaseAddress = _baseAddress,

        };
        return httpClient;
    }


    protected override ICompositeService Build()
    {
        var file = Path.Combine(Directory.GetCurrentDirectory(),
            (TemplateString)"Fixtures/docker-compose.yml");
        
        return new DockerComposeCompositeService(
            _dockerHost,
            new DockerComposeConfig
            {
                ComposeFilePath = new List<string> { file },
                ForceRecreate = true,
                RemoveOrphans = true,
                StopOnDispose = true,
                ComposeVersion = ComposeVersion.V2
            });
    }
}
