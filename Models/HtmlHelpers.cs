using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

//public static class HtmlHelpers
//{
//    public static IHtmlContent DisplayNameFor<TModel, TValue>(
//        this IHtmlHelper<TModel> html,
//        Expression<Func<TModel, TValue>> expression,
//        string lang)
//    {
//        // Get the property name from the expression
//        var memberExpression = expression.Body as MemberExpression;
//        var propertyName = memberExpression?.Member.Name ?? string.Empty;

//        // Translation dictionary
//        var translations = new Dictionary<string, Dictionary<string, string>>
//        {
//            ["Title"] = new() { ["en"] = "Title", ["ar"] = "العنوان" },
//            ["Location"] = new() { ["en"] = "Location", ["ar"] = "الموقع" },
//            ["Description"] = new() { ["en"] = "Description", ["ar"] = "الوصف" },
//            ["SubmittedOn"] = new() { ["en"] = "Submitted On", ["ar"] = "تاريخ التقديم" },
//            ["EndOn"] = new() { ["en"] = "End On", ["ar"] = "تاريخ الانتهاء" }
//        };

//        var displayName = translations.TryGetValue(propertyName, out var langTranslations)
//            ? langTranslations.GetValueOrDefault(lang, propertyName)
//            : propertyName;

//        return new HtmlString(displayName);
//    }
//}