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
            var lang = context.Request.Cookies["Lang"] ?? "en";
            context.Items["Lang"] = lang;
            await _next(context);
        }
    }

}
