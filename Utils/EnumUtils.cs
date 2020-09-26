using System;

namespace TopDownMedieval.Plugins.Commons.Utils
{
    public static class EnumUtils
    {
        /*----------------------------------------------------------------------------------------*
         * Static Methods
         *----------------------------------------------------------------------------------------*/

        public static void Iterate<T>(Iteration<T> iteration) where T : Enum
        {
            Type enumType = typeof(T);
            foreach (string socketString in Enum.GetNames(enumType))
            {
                T enumValue = (T) Enum.Parse(enumType, socketString);
                iteration.Invoke(enumValue);
            }
        }

        public static T[] GetValues<T>() where T : Enum
        {
	        Type enumType = typeof(T);
	        string[] names = Enum.GetNames(enumType);
	        T[] values = new T[names.Length];
	        for (int i = 0; i < names.Length; i++)
	        {
		        string socketString = names[i];
		        T enumValue = (T) Enum.Parse(enumType, socketString);
		        values[i] = enumValue;
	        }

	        return values;
        }

        /*----------------------------------------------------------------------------------------*
         * Inner Classes and Delegates
         *----------------------------------------------------------------------------------------*/
        
        public delegate void Iteration<in T>(T enumValue);
    }
}