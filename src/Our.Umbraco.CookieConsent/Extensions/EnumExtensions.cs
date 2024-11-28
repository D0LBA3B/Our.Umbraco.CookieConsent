﻿using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Our.Umbraco.CookieConsent.Extensions;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum value)
    {
        return value.GetType()
            .GetMember(value.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()?
            .Name ?? value.ToString();
    }
}