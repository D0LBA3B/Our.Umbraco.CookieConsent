namespace Our.Umbraco.CookieConsent.Models;

public class CookieConsentSettingsModel
{
    //TODO: finish integrating all properties (https://playground.cookieconsent.orestbida.com/)
    public CookieCategoriesModel Categories { get; set; }
    public Dictionary<string, bool> Translations { get; set; }
    public string DefaultLanguage { get; set; }
    public GuiOptionsModel GuiOptions { get; set; }
    public string Theme { get; set; }
    public MiscOptionsModel MiscOptions { get; set; }
}