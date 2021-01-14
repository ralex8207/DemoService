using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoService.Extensions {

    public static class MethodExtensions {

        public static bool IsOneOf<T>(this T self, params T[] values) =>
            values.Contains(self);

        public static bool HasSome<T, U>(this T self, Func<T, IEnumerable<U>> property) =>
            property(self)?.Any() ?? false;

        public static bool HasNo<T, U>(this T self, Func<T, IEnumerable<U>> property) =>
            !HasSome(self, property);
    }

}