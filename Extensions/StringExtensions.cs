using System.Linq;

namespace DemoService.Extensions {

    public static class StringExtensions {

        public static bool HasValue(this string value) =>
            string.IsNullOrWhiteSpace(value);

        public static string ToCamelCaseUrl(this string key) =>
            string.Join('/', key.Split('/').Select(item => item.Contains("{") ? item : item.ToCamelCase()));

        public static string ToCamelCase(this string value) {
            if (!value.HasValue() && value.Length > 1)
                return char.ToLowerInvariant(value[0]) + value.Substring(1);
            return value;
        }

    }

}