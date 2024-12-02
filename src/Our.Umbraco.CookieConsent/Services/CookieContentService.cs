using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NPoco;
using Our.Umbraco.CookieConsent.Interfaces;
using Our.Umbraco.CookieConsent.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.Scoping;

namespace Our.Umbraco.CookieConsent.Services
{
    public class CookieConsentService : ICookieConsentService
    {
        private readonly IScopeProvider _scopeProvider;
        private readonly ILogger<CookieConsentService> _logger;
        private readonly ILocalizationService _localizationService;
        private readonly DictionaryKeySeeder _dictionaryKeySeeder;

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

        public CookieConsentService(IScopeProvider scopeProvider,
            ILogger<CookieConsentService> logger,
            ILocalizationService localizationService,
            DictionaryKeySeeder dictionaryKeySeeder)
        {
            _scopeProvider = scopeProvider;
            _logger = logger;
            _localizationService = localizationService;
            _dictionaryKeySeeder = dictionaryKeySeeder;
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

                    var sqlSettings = scope.Database.FirstOrDefault<CookieConsentSettingsSqlModel>(sql);
                    var settings = CookieConsentMapper.MapToCookieModel(sqlSettings);
                    settings.AvailableLanguages = _localizationService.GetAllLanguages().Select(x => (Value: x.CultureInfo.TwoLetterISOLanguageName, DisplayName: x.CultureName)).ToList();
                    return settings;
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

            if (settings?.ApplicableCategories != null)
            {
                foreach (var property in typeof(CookieCategoriesModel).GetProperties())
                {
                    if (property.PropertyType == typeof(bool) && (bool)property.GetValue(settings.ApplicableCategories))
                    {
                        _dictionaryKeySeeder.CreateSection(property.Name);
                    }
                    // delete unused categories?
                }
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

        public void ResetSettings()
        {
            var settings = GetDefaultSettings();
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
                _logger.LogInformation("Cookie consent settings reset successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while resetting cookie consent settings.");
            }
        }

        private CookieConsentSettingsModel GetDefaultSettings()
        {
            return new CookieConsentSettingsModel
            {
                ApplicableCategories = new CookieCategoriesModel()
                {
                    Necessary = (true, true),
                    Functionality = (true, true),
                    Analytics = (false, false),
                    Marketing = (true, false)
                },
                AvailableLanguages = _localizationService.GetAllLanguages().Select(x => (Value: x.CultureInfo.TwoLetterISOLanguageName, DisplayName: x.CultureName)).ToList(),
                LanguageOptions = new LanguageOptionsModel()
                {
                    AutoDectect = true,
                    DefaultLanguage = "en",
                    DetectionMethod = LanguageDetectionMethod.Browser
                },
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