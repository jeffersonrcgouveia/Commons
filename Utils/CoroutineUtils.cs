using System;
using System.Collections;
using UnityEngine;

namespace TopDownMedieval.Plugins.Commons.Utils
{
    public static class CoroutineUtils {
 
        /*----------------------------------------------------------------------------------------*
         * Static Methods
         *----------------------------------------------------------------------------------------*/
    
        /**
         * Usage: StartCoroutine(CoroutineUtils.WaitForSeconds(action, delay))
         * For example:
         *     StartCoroutine(CoroutineUtils.WaitForSeconds(
         *         () => DebugUtils.Log("2 seconds past"),
         *         2);
         */
        public static IEnumerator WaitForSeconds(Action action, float delay)
        {
            yield return new WaitForSeconds(delay);
            action();
        }

        public static IEnumerator WaitForSeconds(float delay)
        {
            yield return new WaitForSeconds(delay);
        }

        public static IEnumerator WaitForNextFrame(Action action)
        {
            yield return null;
            action();
        }

        public static IEnumerator DoAndWaitForNextFrame(Action action)
        {
            action();
            yield return null;
        }

        public static IEnumerator WaitForEndOfFrame(Action action)
        {
            yield return new WaitForEndOfFrame();
            action();
        }
    }
}