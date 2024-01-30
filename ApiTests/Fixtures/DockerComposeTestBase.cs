using Ductus.FluentDocker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTests;
public abstract class DockerComposeTestBase : IDisposable
{
    protected ICompositeService _compositeService;
    protected IHostService? _dockerHost;
    public int AttempDelay { get; set; } = 1000;

    public DockerComposeTestBase()
    {
        EnsureDockerHost();

        _compositeService = Build();
        try
        {
            _compositeService.Start();
        }
        catch
        {
            _compositeService.Dispose();
            throw;
        }

        OnContainerInitialized();
    }

    public void Dispose()
    {
        OnContainerTearDown();
        var compositeService = _compositeService;
        _compositeService = null!;
        compositeService?.Dispose();
        
    }

    protected virtual void OnContainerTearDown() { }
    protected virtual void OnContainerInitialized() { }
    protected abstract ICompositeService Build();
    private void EnsureDockerHost()
    {
        do
        {
            if (_dockerHost?.State == ServiceRunningState.Running)
                return;

            var hosts = new Hosts().Discover();
            _dockerHost = hosts.FirstOrDefault(x => x.IsNative) ?? hosts.FirstOrDefault(x => x.Name == "default");

            if (_dockerHost != null)
            {
                if (_dockerHost.State != ServiceRunningState.Running)
                    _dockerHost.Start();

                return;
            }

            if (hosts.Count > 0)
                _dockerHost = hosts.First();

            if (null != _dockerHost)
                return;

            Thread.Sleep(AttempDelay);
        } while (true);

    }
}
