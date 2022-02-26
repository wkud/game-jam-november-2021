using UnityEngine;

public static class VectorUtility
{
    public static Vector3 ToVector3(this Vector2 vector2, float z = 0)
    {
        return new Vector3(vector2.x, vector2.y, z);
    }
}
