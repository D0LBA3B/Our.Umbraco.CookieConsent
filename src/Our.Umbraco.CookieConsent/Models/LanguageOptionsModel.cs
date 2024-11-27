namespace Our.Umbraco.CookieConsent.Models;

public class LanguageOptionsModel
{
    public string DefaultLanguage { get; set; }
    public bool AutoDectect { get; set; }
    public LanguageDetectionMethod DetectionMethod { get; set; }
}

public enum LanguageDetectionMethod
{
    Browser,
    Document
}