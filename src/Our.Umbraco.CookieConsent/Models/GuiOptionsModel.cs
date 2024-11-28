using System.ComponentModel.DataAnnotations;

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
    [Display(Name = "box")]
    Box,
    [Display(Name = "box inline")]
    BoxInline,
    [Display(Name = "box wide")]
    BoxWide,
    [Display(Name = "cloud")]
    Cloud,
    [Display(Name = "cloud inline")]
    CloudInline,
    [Display(Name = "bar")]
    Bar,
    [Display(Name = "bar inline")]
    BarInline
}

public enum PreferencesModalLayout
{
    [Display(Name = "box")]
    Box,
    [Display(Name = "bar")]
    Bar,
    [Display(Name = "bar wide")]
    BarWide
}

public enum ConsentModalPosition
{
    [Display(Name = "top left")]
    TopLeft,
    [Display(Name = "top center")]
    TopCenter,
    [Display(Name = "top right")]
    TopRight,
    [Display(Name = "middle left")]
    MiddleLeft,
    [Display(Name = "middle center")]
    MiddleCenter,
    [Display(Name = "middle right")]
    MiddleRight,
    [Display(Name = "bottom left")]
    BottomLeft,
    [Display(Name = "bottom center")]
    BottomCenter,
    [Display(Name = "bottom right")]
    BottomRight
}

public enum PreferencesModalPosition
{
    [Display(Name = "left")]
    Left,
    [Display(Name = "right")]
    Right
}