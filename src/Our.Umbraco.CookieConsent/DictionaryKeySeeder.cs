using Microsoft.Extensions.Logging;
using Our.Umbraco.CookieConsent.Models;
using Our.Umbraco.CookieConsent.Services;
using Umbraco.Cms.Core.Services;

namespace Our.Umbraco.CookieConsent;

public class DictionaryKeySeeder
{
    private readonly ILocalizationService _localizationService;
    private readonly ILogger<CookieConsentService> _logger;

    public DictionaryKeySeeder(ILocalizationService localizationService, ILogger<CookieConsentService> logger)
    {
        _localizationService = localizationService;
        _logger = logger;
    }

    public void CreateBaseKeys()
    {
        var parentKey = CreateKey(Translations.NAMESPACE, "-");
        CreateKey(Translations.ConsentModal.Title, "Consent Modal Title", parentKey);
        CreateKey(Translations.ConsentModal.Description, "Consent Modal Description", parentKey);
        CreateKey(Translations.ConsentModal.CloseIconLabel, "Close Icon Label", parentKey);
        CreateKey(Translations.ConsentModal.AcceptAll, "Accept All Button", parentKey);
        CreateKey(Translations.ConsentModal.RejectAll, "Reject All Button", parentKey);
        CreateKey(Translations.ConsentModal.ManagePreferences, "Manage Preferences Button", parentKey);
        CreateKey(Translations.ConsentModal.Footer, "Consent Modal Footer", parentKey);

        CreateKey(Translations.PreferencesModal.Title, "Preferences Modal Title", parentKey);
        CreateKey(Translations.PreferencesModal.CloseIconLabel, "Preferences Close Icon Label", parentKey);
        CreateKey(Translations.PreferencesModal.AcceptAll, "Accept All Button", parentKey);
        CreateKey(Translations.PreferencesModal.RejectAll, "Reject All Button", parentKey);
        CreateKey(Translations.PreferencesModal.Save, "Save Preferences Button", parentKey);
        CreateKey(Translations.PreferencesModal.ServiceCounterLabel, "Service Counter Label", parentKey);

        CreateKey(Translations.PreferencesModal.Sections.Title, "Section Title", parentKey);
        CreateKey(Translations.PreferencesModal.Sections.Description, "Section Description", parentKey);
        CreateKey(Translations.PreferencesModal.Sections.LinkedCategory, "Linked Category Dropdown", parentKey);
        CreateKey(Translations.PreferencesModal.Sections.Delete, "Delete Section Button", parentKey);
        CreateKey(Translations.PreferencesModal.Sections.Add, "Add Section Button", parentKey);
    }

    private Guid? CreateKey(string key, string value, Guid? parentKey = null)
    {
        try
        {
            var existingItem = _localizationService.GetDictionaryItemByKey(key);
            if (existingItem == null)
            {
                var dict = _localizationService.CreateDictionaryItemWithIdentity(key, parentId: parentKey, value);
                Console.WriteLine($"Created translation key: {key}");
                return dict?.Key;
            }
            Console.WriteLine($"Key already exists: {key}");
            return existingItem?.Key;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating key {key}: {ex.Message}");
            return null;
        }
    }
}