using System;
using UnityEngine;
using UnityEngine.UI;

namespace TopDownMedieval.Plugins.Commons.Utils
{
    public static class ImageExtensions
    {
        /*----------------------------------------------------------------------------------------*
         * Static Methods
         *----------------------------------------------------------------------------------------*/

        public static void ToggleAlpha(this Graphic image)
        {
	        Color previewColor = image.color;
	        previewColor.a = previewColor.a == 0 ? 1f : 0;
	        image.color = previewColor;
        }

    }
}