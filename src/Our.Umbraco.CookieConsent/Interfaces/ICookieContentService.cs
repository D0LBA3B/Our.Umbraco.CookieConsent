using Our.Umbraco.CookieConsent.Models;

namespace Our.Umbraco.CookieConsent.Interfaces;

public interface ICookieConsentService
{
    public CookieConsentSettingsModel GetSettings();
    public void SaveSettings(CookieConsentSettingsModel settings);
    public void ResetSettings();
}