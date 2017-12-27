using System;

namespace CheckersCommon.Utilities
{
    public static class Guard
    {
        public static T NotNull<T>(this T @object, string name = null)
            where T : class
        {
            return @object ?? throw new ArgumentNullException(name ?? nameof(@object));
        }
    }
}
