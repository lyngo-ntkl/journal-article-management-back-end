namespace API.Utils {
    public static class HttpContextUtils {
        public static IHeaderDictionary? GetRequestHeaders(this IHttpContextAccessor httpContextAccessor) {
            var headers = httpContextAccessor.HttpContext?.Request.Headers;
            return headers;
        }

        public static string? GetRequestHeader(this IHttpContextAccessor httpContextAccessor, string key) {
            return httpContextAccessor.HttpContext?.Request.Headers.FirstOrDefault(header => header.Key == key).Value;
        }

        public static string? GetAuthorizationHeader(this IHttpContextAccessor httpContextAccessor) {
            return httpContextAccessor.HttpContext?.Request.Headers.Authorization;
        }
    }
}