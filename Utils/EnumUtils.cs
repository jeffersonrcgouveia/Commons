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

        public static T ToEnum<T>(int enumValueIndex) where T : Enum => (T) Enum.GetValues(typeof(T)).GetValue(enumValueIndex);

	    public static int ToEnumValueIndex<T>(T enumObj) where T : Enum => Convert.ToInt32(enumObj);

	    /*----------------------------------------------------------------------------------------*
         * Inner Classes and Delegates
         *----------------------------------------------------------------------------------------*/
        
        public delegate void Iteration<in T>(T enumValue);
    }
}