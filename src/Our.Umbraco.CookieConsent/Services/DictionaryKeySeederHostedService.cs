using Microsoft.Extensions.Hosting;
using Our.Umbraco.CookieConsent;

public class DictionaryKeySeederHostedService : IHostedService
{
    private readonly DictionaryKeySeeder _generator;

    public DictionaryKeySeederHostedService(DictionaryKeySeeder generator)
    {
        _generator = generator;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _generator.CreateBaseKeys();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}