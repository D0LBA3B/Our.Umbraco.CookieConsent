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

public readonly struct Translations
{
    public const string NAMESPACE = "Our.Umbraco.CookieConsent";

    public readonly struct ConsentModal
    {
        private const string PREFIX = NAMESPACE + ".ConsentModal.";
        public const string Title = PREFIX + "Title";
        public const string Description = PREFIX + "Description";
        public const string CloseIconLabel = PREFIX + "CloseIconLabel";
        public const string AcceptAll = PREFIX + "AcceptAll";
        public const string RejectAll = PREFIX + "RejectAll";
        public const string ManagePreferences = PREFIX + "ManagePreferences";
        public const string Footer = PREFIX + "Footer";
    }

    public readonly struct PreferencesModal
    {
        private const string PREFIX = NAMESPACE + ".PreferencesModal.";
        public const string Title = PREFIX + "Title";
        public const string CloseIconLabel = PREFIX + "CloseIconLabel";
        public const string AcceptAll = PREFIX + "AcceptAll";
        public const string RejectAll = PREFIX + "RejectAll";
        public const string Save = PREFIX + "Save";
        public const string ServiceCounterLabel = PREFIX + "ServiceCounterLabel";

        public readonly struct Sections
        {
            private const string SECTIONS_PREFIX = PREFIX + "Sections.";
            public const string Title = SECTIONS_PREFIX + "Title";
            public const string Description = SECTIONS_PREFIX + "Description";
            public const string LinkedCategory = SECTIONS_PREFIX + "LinkedCategory";
            public const string Delete = SECTIONS_PREFIX + "Delete";
            public const string Add = SECTIONS_PREFIX + "Add";
        }
    }
}
