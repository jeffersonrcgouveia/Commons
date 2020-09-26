using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace TopDownMedieval.Plugins.Commons.Utils
{
    public static class DictionaryExtensions
    {
        /*----------------------------------------------------------------------------------------*
         * Static Methods
         *----------------------------------------------------------------------------------------*/

        public static T AddNextEnumKey<T, V>(this IDictionary<T, V> dictionary, V value) where T : Enum
        {
	        T[] values = EnumUtils.GetValues<T>();
	        foreach (T enumValue in values)
	        {
		        if (dictionary.ContainsKey(enumValue)) continue;
		        dictionary.Add(enumValue, value);
		        return enumValue;
	        }

	        throw new Exception("All enum values was already added as dictionary key");
        }
    }
}