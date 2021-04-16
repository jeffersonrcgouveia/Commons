using System.Collections.Generic;
using UnityEngine;

public static class IReadOnlyListExtensions
{
    public static T GetRandomValue<T>(this IReadOnlyList<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }
}
