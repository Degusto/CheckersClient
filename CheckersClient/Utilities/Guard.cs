using System;

namespace CheckersClient.Utilities
{
    internal static class Guard
    {
        internal static T NotNull<T>(this T @object, string name = null)
            where T : class
        {
            return @object ?? throw new ArgumentNullException(name ?? nameof(@object));
        }
    }
}
