using Microsoft.AspNetCore.Mvc;

namespace AymanProject.Controllers
{
    public class BaseController : Controller
    {
        protected string CurrentLanguage => HttpContext.Items["Lang"]?.ToString() ?? "en";
        protected bool IsArabic => HttpContext.Items["IsArabic"] as bool? ?? false;
        protected string Direction => HttpContext.Items["Direction"]?.ToString() ?? "ltr";

        public override void OnActionExecuting(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext context)
        {
            // Automatically set ViewData for all actions
            ViewData["Lang"] = CurrentLanguage;
            ViewData["IsArabic"] = IsArabic;
            ViewData["Direction"] = Direction;

            base.OnActionExecuting(context);
        }

        // Helper method to redirect with language preserved
        protected IActionResult RedirectToActionWithLang(string action, string controller = null, object routeValues = null)
        {
            var routes = new Dictionary<string, object> { ["lang"] = CurrentLanguage };

            if (routeValues != null)
            {
                foreach (var prop in routeValues.GetType().GetProperties())
                {
                    routes[prop.Name] = prop.GetValue(routeValues);
                }
            }

            return RedirectToAction(action, controller, routes);
        }

        // Helper method for language switching
        [HttpPost]
        public virtual IActionResult SetLanguage(string lang, string returnUrl = null)
        {
            if (lang == "ar" || lang == "en")
            {
                Response.Cookies.Append("Lang", lang, new CookieOptions
                {
                    Expires = DateTime.Now.AddYears(1),
                    IsEssential = true,
                    SameSite = SameSiteMode.Lax
                });
            }

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                // Remove lang parameter from return URL to avoid conflicts
                var uri = new Uri(returnUrl, UriKind.RelativeOrAbsolute);
                if (!uri.IsAbsoluteUri)
                {
                    uri = new Uri(Request.Scheme + "://" + Request.Host + returnUrl);
                }

                var query = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(uri.Query);
                query.Remove("lang");

                var newQuery = Microsoft.AspNetCore.WebUtilities.QueryHelpers.AddQueryString("", query);
                var newUrl = uri.GetLeftPart(UriPartial.Path) + newQuery;

                return Redirect(newUrl + (string.IsNullOrEmpty(newQuery) ? "?" : "&") + "lang=" + lang);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}