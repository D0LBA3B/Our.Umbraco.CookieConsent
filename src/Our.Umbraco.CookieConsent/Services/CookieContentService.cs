using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NPoco;
using Our.Umbraco.CookieConsent.Interfaces;
using Our.Umbraco.CookieConsent.Models;
using Umbraco.Cms.Infrastructure.Scoping;

namespace Our.Umbraco.CookieConsent.Services
{
    public class CookieConsentService : ICookieConsentService
    {
        private readonly IScopeProvider _scopeProvider;
        private readonly ILogger<CookieConsentService> _logger;

        private const string DefaultSettingsSql = @"SELECT TOP(1) 
                                [Id],
                                [SettingsJson],
                                [LastUpdated]
                             FROM [CookieConsentSettings]
                             ORDER BY [LastUpdated] DESC";

        private const string DefaultSettingsSqlLite = @"SELECT 
                                [Id],
                                [SettingsJson],
                                [LastUpdated]
                             FROM [CookieConsentSettings]
                             ORDER BY [LastUpdated] DESC
                             LIMIT 1";

        private const string InsertSettingsSql = @"INSERT INTO [CookieConsentSettings] 
                                ([SettingsJson], [LastUpdated])
                             VALUES (@settingsJson, @lastUpdated)";

        public CookieConsentService(IScopeProvider scopeProvider, ILogger<CookieConsentService> logger)
        {
            _scopeProvider = scopeProvider;
            _logger = logger;
        }

        public CookieConsentSettingsModel GetSettings()
        {
            try
            {
                using (var scope = _scopeProvider.CreateScope())
                {
                    var sql = scope.Database.DatabaseType == DatabaseType.SQLite
                        ? DefaultSettingsSqlLite
                        : DefaultSettingsSql;

                    var settings = scope.Database.FirstOrDefault<CookieConsentSettingsSqlModel>(sql);

                    return CookieConsentMapper.MapToCookieModel(settings) ?? GetDefaultSettings();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while fetching cookie consent settings.");
                return GetDefaultSettings();
            }
        }

        public void SaveSettings(CookieConsentSettingsModel settings)
        {
            if (settings == null)
            {
                _logger.LogWarning("Attempted to save empty or null settings.");
                throw new ArgumentException("Settings cannot be null.", nameof(settings));
            }

            try
            {
                using (var scope = _scopeProvider.CreateScope())
                {
                    var settingsJson = JsonConvert.SerializeObject(settings);
                    scope.Database.Execute(InsertSettingsSql, new
                    {
                        settingsJson,
                        lastUpdated = DateTime.UtcNow
                    });

                    scope.Complete();
                }

                _logger.LogInformation("Cookie consent settings saved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while saving cookie consent settings.");
            }
        }

        private CookieConsentSettingsModel GetDefaultSettings()
        {
            return new CookieConsentSettingsModel
            {
                ApplicableCategories = new CookieCategoriesModel()
                {
                    Necessary = true,
                    Functionality = false,
                    Analytics = false,
                    Marketing = false
                },
                Translations = new Dictionary<string, bool>
                {
                    { "en", true },
                    { "fr", false },
                    { "de", false }
                },
                LanguageOptions = new LanguageOptionsModel()
                {
                    AutoDectect = true,
                    DefaultLanguage = "en",
                    DetectionMethod = LanguageDetectionMethod.Browser
                } ,
                GuiOptions = new GuiOptionsModel
                {
                    ConsentModalLayout = ConsentModalLayout.Box,
                    ConsentModalPosition = ConsentModalPosition.BottomLeft,
                    PreferencesModalLayout = PreferencesModalLayout.Box,
                    PreferencesModalPosition = PreferencesModalPosition.Right
                },
                Theme = "light",
                MiscOptions = new MiscOptionsModel
                {
                    EnableDarkMode = false,
                    DisableTransitions = false,
                    DisablePageInteraction = false
                }
            };
        }
    }
}