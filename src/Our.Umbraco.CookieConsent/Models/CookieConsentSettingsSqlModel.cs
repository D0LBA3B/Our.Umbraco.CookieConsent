﻿namespace Our.Umbraco.CookieConsent.Models;

public class CookieConsentSettingsSqlModel
{
    public int Id { get; set; }
    public string SettingsJson { get; set; }
    public DateTime LastUpdated { get; set; }
}