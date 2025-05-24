using System.Globalization;

namespace AymanProject.Models
{
    public class LanguageMiddleware
    {
        private readonly RequestDelegate _next;

        public LanguageMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Check for language in query parameters first (for switching)
            var langFromQuery = context.Request.Query["lang"].FirstOrDefault();

            // If language is provided in query, set it in cookie
            if (!string.IsNullOrEmpty(langFromQuery) && (langFromQuery == "en" || langFromQuery == "ar"))
            {
                context.Response.Cookies.Append("Lang", langFromQuery, new CookieOptions
                {
                    Expires = DateTime.Now.AddYears(1),
                    IsEssential = true,
                    SameSite = SameSiteMode.Lax
                });
            }

            // Get language from cookie, query, or default to English
            var lang = langFromQuery ?? context.Request.Cookies["Lang"] ?? "en";

            // Ensure only valid languages
            if (lang != "ar" && lang != "en")
                lang = "en";

            // Set in context items for use throughout the application
            context.Items["Lang"] = lang;
            context.Items["IsArabic"] = lang == "ar";
            context.Items["Direction"] = lang == "ar" ? "rtl" : "ltr";

            // Set culture for proper formatting
            var culture = lang == "ar" ? new CultureInfo("ar-SA") : new CultureInfo("en-US");
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;

            await _next(context);
        }
    }
}