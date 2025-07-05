namespace Our.Umbraco.CookieConsent.Models;

public class CookieCategoriesModel
{
    public (bool Enabled, bool ReadOnly) Necessary { get; set; }
    public (bool Enabled, bool ReadOnly) Functionality { get; set; }
    public (bool Enabled, bool ReadOnly) Analytics { get; set; }
    public (bool Enabled, bool ReadOnly) Marketing { get; set; }
}

public enum CookieCategoryType
{
    Necessary,
    Functionality,
    Analytics,
    Marketing
}