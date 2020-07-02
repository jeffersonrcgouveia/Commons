namespace TopDownMedieval.Plugins.Commons.Utils
{
    public static class ArrayUtils
    {
        /*----------------------------------------------------------------------------------------*
         * Static Methods
         *----------------------------------------------------------------------------------------*/

        public static T FindFirstEmptyElement<T>(T[] array) where T : class
        {
            return FindFirstEmptyElement(array, element => element == null);
        }

        public static T FindFirstEmptyElement<T>(T[] array, EmptyElementCallbackHandler<T> callbackHandler)
            where T : class
        {
            int emptyIndex = FindFirstEmptyIndex(array, callbackHandler);
            return emptyIndex > -1 ? array[emptyIndex] : null;
        }

        public static int FindFirstEmptyIndex<T>(T[] array)
        {
            return FindFirstEmptyIndex(array, element => element == null);
        }

        public static int FindFirstEmptyIndex<T>(T[] array, EmptyElementCallbackHandler<T> callbackHandler)
        {
            for (int i = 0; i < array.Length; i++)
                if (callbackHandler.Invoke(array[i]))
                    return i;

            return -1;
        }

        /*----------------------------------------------------------------------------------------*
         * Inner Classes and Delegates
         *----------------------------------------------------------------------------------------*/
        
        public delegate bool EmptyElementCallbackHandler<in T>(T element);
        
    }
}