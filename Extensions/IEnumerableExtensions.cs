using System.Collections.Generic;

public static class IEnumerableExtensions
{
	public static bool IsEmpty<T>(this IEnumerable<T> list)
    {
        foreach (T obj in list)
        {
            if (obj != null) return false;
        }

        return true;
    }
}
