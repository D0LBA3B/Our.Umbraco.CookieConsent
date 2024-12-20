using Microsoft.Extensions.DependencyInjection;
using Our.Umbraco.CookieConsent.Interfaces;
using Our.Umbraco.CookieConsent.Services;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace Our.Umbraco.CookieConsent;

public class Composer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.AddScoped<ICookieConsentService, CookieConsentService>();
        builder.Services.AddScoped<DictionaryKeySeeder>();
        builder.AddComponent<CookieConsentComponent>();
        builder.AddDashboard<CookieConsentDashboard>();
        builder.Services.AddTransient<DictionaryKeySeeder>();
        builder.Services.AddHostedService<DictionaryKeySeederHostedService>();
    }
}