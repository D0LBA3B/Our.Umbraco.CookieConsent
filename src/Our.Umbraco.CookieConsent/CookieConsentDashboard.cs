using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Dashboards;

namespace Our.Umbraco.CookieConsent;

[Weight(11)]
public class CookieConsentDashboard : IDashboard
{
    public string Alias => "Our.Umbraco.CookieConsent";
    public string View => "/App_Plugins/Our.Umbraco.CookieConsent/CookieConsentDashboard.html";

    public string[] Sections => new[]
    {
            global::Umbraco.Cms.Core.Constants.Applications.Settings
        };

    public IAccessRule[] AccessRules => Array.Empty<IAccessRule>();
}