namespace Our.Umbraco.CookieConsent.Models;

public class GuiOptionsModel
{
    public ConsentModalLayout ConsentModalLayout { get; set; }
    public ConsentModalPosition ConsentModalPosition { get; set; }
    public PreferencesModalLayout PreferencesModalLayout { get; set; }
    public PreferencesModalPosition PreferencesModalPosition { get; set; }
}

public enum ConsentModalLayout
{
    Box,
    BoxInline,
    BoxWide,
    Cloud,
    CloudInline,
    Bar,
    BarInline
}

public enum PreferencesModalLayout
{
    Box,
    Bar,
    BarWide
}

public enum ConsentModalPosition
{
    TopLeft,
    TopCenter,
    TopRight,
    MiddleLeft,
    MiddleCenter,
    MiddleRight,
    BottomLeft,
    BottomCenter,
    BottomRight
}

public enum PreferencesModalPosition
{
    Left,
    Right
}