using UnityEngine;

namespace Commons.Extensions
{
	public static class Vector3Extensions
	{
		public static Vector3 ScaleDistance(this Vector3 posA, Vector3 posB, float percent)
		{
			float distance = Vector3.Distance(posA, posB);
			float scaledDistance = distance * percent;
			Vector3 direction = posB - posA;
 
			return posA + direction.normalized * scaledDistance;
		}
	}
}