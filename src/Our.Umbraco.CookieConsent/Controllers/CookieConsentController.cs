using Microsoft.AspNetCore.Mvc;
using Our.Umbraco.CookieConsent.Interfaces;
using Our.Umbraco.CookieConsent.Models;
using Our.Umbraco.CookieConsent.Services;
using Umbraco.Cms.Web.BackOffice.Controllers;
using Umbraco.Cms.Web.BackOffice.Filters;
using Umbraco.Cms.Web.Common.Attributes;

namespace Our.Umbraco.CookieConsent.Controllers;

[IsBackOffice]
[JsonCamelCaseFormatter]
public class CookieConsentController : UmbracoAuthorizedJsonController
{
    private readonly ICookieConsentService _cookieConsentService;

    public CookieConsentController(ICookieConsentService cookieConsentService)
    => _cookieConsentService = cookieConsentService;

    [HttpGet]
    public CookieConsentSettingsModel GetSettings()
    => _cookieConsentService.GetSettings();

    [HttpPost]
    public IActionResult SaveSettings([FromBody] CookieConsentSettingsModel settings)
    {
        if (settings == null)
            return BadRequest("Invalid settings provided.");

        _cookieConsentService.SaveSettings(settings);
        return Ok("Settings have been successfully saved.");
    }

    [HttpGet]
    public CookieConsentSettingsModel ResetSettings()
    {
        //TODO _cookieConsentService.ResetSettings();
        return _cookieConsentService.GetSettings();
    }
}