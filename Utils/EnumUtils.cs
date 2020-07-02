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

        /*----------------------------------------------------------------------------------------*
         * Inner Classes and Delegates
         *----------------------------------------------------------------------------------------*/
        
        public delegate void Iteration<in T>(T enumValue);
    }
}