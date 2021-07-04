using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class UrlHelpers
{
    public static string ToQueryString(object request)
    {
        if (request == null)
        {
            throw new ArgumentNullException("request");
        }

        List<string> queries = new List<string>();
        foreach (var field in request.GetType().GetFields(BindingFlags.Instance |
                                                 BindingFlags.NonPublic |
                                                 BindingFlags.Public))
        {
            queries.Add(string.Concat(
                Uri.EscapeDataString(field.Name),
                "=",
                Uri.EscapeDataString(field.GetValue(request).ToString())
            ));
        }

        string result = string.Join("&", queries);

        return string.IsNullOrEmpty(result) ? "" : "?" + result;
    }

    public static WWWForm ToWWWForm(object request)
    {
        if (request == null)
        {
            throw new ArgumentNullException("request");
        }

        WWWForm form = new WWWForm();
        foreach (var field in request.GetType().GetFields(BindingFlags.Instance |
                                                 BindingFlags.NonPublic |
                                                 BindingFlags.Public))
        {
            form.AddField(field.Name, field.GetValue(request).ToString());
        }
        return form;
    }
}