using Microsoft.AspNetCore.Mvc;

namespace Our.Umbraco.CookieConsent.Components
{
    [ViewComponent(Name = "Cookie")]
    public class CookieViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}