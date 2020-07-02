using UnityEngine;

namespace TopDownMedieval.Plugins.Commons.Utils
{
    public static class CanvasUtils
    {
        /*----------------------------------------------------------------------------------------*
         * Static Methods
         *----------------------------------------------------------------------------------------*/

        public static void ShowHide(CanvasGroup canvasGroup)
        {
            ShowHide(canvasGroup, canvasGroup.alpha < 1f);
        }

        public static void Show(CanvasGroup canvasGroup)
        {
            ShowHide(canvasGroup, true);
        }

        public static void Hide(CanvasGroup canvasGroup)
        {
            ShowHide(canvasGroup, false);
        }

        public static void ShowHide(CanvasGroup canvasGroup, bool show)
        {
            if (show)
            {
                canvasGroup.alpha = 1f;
                canvasGroup.blocksRaycasts = true;
            }
            else
            {
                canvasGroup.alpha = 0f;
                canvasGroup.blocksRaycasts = false;
            }
        }
    }
}