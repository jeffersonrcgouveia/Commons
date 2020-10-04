using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TopDownMedieval.Plugins.Commons.Utils
{
    public static class TransformUtils
    {
        /*----------------------------------------------------------------------------------------*
         * Static Methods
         *----------------------------------------------------------------------------------------*/

        public static int IndexOfInChildrenWithTag(Transform transform, string tag, Transform element)
        {
            List<Transform> children = FindAllInChildrenWithTag(transform, tag);
            for (int i = 0; i < children.Count; i++)
                if (children[i] == element)
                    return i;
            return -1;
        }

        public static Transform FindInChildren(Transform transform, string name)
        {
            if (transform == null) return null;
            int count = transform.childCount;
            for (int i = 0; i < count; i++)
            {
                Transform child = transform.GetChild(i);
                if (child.name == name) return child;
                Transform subChild = FindInChildren(child, name);
                if (subChild != null) return subChild;
            }

            return null;
        }

        public static Transform FindFirstEmptyTransformInChildren(Transform transform)
        {
            if (!transform) return null;
            int count = transform.childCount;
            for (int i = 0; i < count; i++)
            {
                Transform child = transform.GetChild(i);
                if (child.childCount == 0) return child;
            }

            return null;
        }

        public static List<GameObject> FindAllGameObjectInChildren(Transform transform, string name)
        {
            return FindAllGameObjectInChildren(transform, property => property.name == name);
        }

        public static List<GameObject> FindAllGameObjectInChildrenWithTag(Transform transform, string tag)
        {
            return FindAllGameObjectInChildren(transform, property => property.CompareTag(tag));
        }

        public static List<GameObject> FindAllGameObjectInChildren(Transform transform,
            CompareProperty<GameObject> compare)
        {
            List<GameObject> children = new List<GameObject>();
            if (transform == null) return null;
            int count = transform.childCount;
            for (int i = 0; i < count; i++)
            {
                Transform child = transform.GetChild(i);
                if (compare.Invoke(child.gameObject)) children.Add(child.gameObject);
                children.AddRange(FindAllGameObjectInChildren(child, compare));
            }

            return children;
        }

        public static List<Transform> FindAllInChildren(Transform transform, string name)
        {
            return FindAllInChildren(transform, property => property.name == name);
        }

        public static List<Transform> FindAllInChildrenWithTag(Transform transform, string tag)
        {
            return FindAllInChildren(transform, property => property.CompareTag(tag));
        }

        public static List<Transform> FindAllInChildren(Transform transform, CompareProperty<Transform> compare)
        {
            List<Transform> children = new List<Transform>();
            if (transform == null) return null;
            int count = transform.childCount;
            for (int i = 0; i < count; i++)
            {
                Transform child = transform.GetChild(i);
                if (compare.Invoke(child)) children.Add(child);
                children.AddRange(FindAllInChildren(child, compare));
            }

            return children;
        }

        public static Transform FindFirstEmptyChildInChildren(Transform transform)
        {
            if (!transform) return null;
            int count = transform.childCount;
            for (int i = 0; i < count; i++)
            {
                Transform child = transform.GetChild(i);
                if (child.childCount == 0) return child;
            }

            return null;
        }

        public static void ResetLocalTransform(Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }

        public static void ResetTransformOffset(RectTransform rectTransform)
        {
            rectTransform.offsetMin = new Vector2(0, 0);
            rectTransform.offsetMax = new Vector2(0, 0);
        }

        public static void SetAnchorPresetStretchXY(RectTransform rectTransform)
        {
            rectTransform.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            rectTransform.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
        }

        public static void ClearChildren(this Transform transform)
        {
	        for (int i = transform.childCount - 1; i >= 0; i--)
	        {
		        GameObject.Destroy(transform.GetChild(i).gameObject);
	        }
        }

        public static void RebuildLayoutImmediate(this Transform transform)
        {
	        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform) transform);
        }

        /*----------------------------------------------------------------------------------------*
         * Inner Classes and Delegates
         *----------------------------------------------------------------------------------------*/

        public delegate bool CompareProperty<in T>(T property);
    }
}