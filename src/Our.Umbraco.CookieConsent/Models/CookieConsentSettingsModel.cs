namespace Our.Umbraco.CookieConsent.Models;

public class CookieConsentSettingsModel
{
    //TODO: finish integrating all properties (https://playground.cookieconsent.orestbida.com/)
    public CookieCategoriesModel ApplicableCategories { get; set; }
    //Store translations as umbraco dictionary items?
    public Dictionary<string, bool> Translations { get; set; }
    public LanguageOptionsModel LanguageOptions { get; set; }
    public GuiOptionsModel GuiOptions { get; set; }
    public MiscOptionsModel MiscOptions { get; set; }
    public string Theme { get; set; }
}