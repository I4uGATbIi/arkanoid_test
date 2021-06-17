using UnityEngine;

namespace Extensions
{
    public static class ColliderExtensions
    {
        public static Vector3 GetRandomPointInside(this Collider collider)
        {
            Vector3 extents = collider.bounds.extents;
            Vector3 point = new Vector3(
                Random.Range(-extents.x, extents.x),
                Random.Range(-extents.y, extents.y),
                Random.Range(-extents.z, extents.z)
            ) + collider.bounds.center;
            return collider.transform.TransformPoint(point);
        }
    }
}