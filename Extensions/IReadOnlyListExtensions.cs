using System.Collections.Generic;
using UnityEngine;

namespace Commons.Extensions
{
    public static class IReadOnlyListExtensions
    {
        public static T GetRandomValue<T>(this IReadOnlyList<T> list) => list[Random.Range(0, list.Count)];
    }
}
