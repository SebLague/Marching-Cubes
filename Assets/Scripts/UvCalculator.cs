using System.Collections.Generic;
using UnityEngine;

public class UvCalculator
{
    // Thanks to: http://answers.unity.com/answers/762349/view.html
    // And : http://answers.unity.com/comments/1595208/view.html

    private enum Facing { Up, Forward, Right };

    public static List<Vector2> CalculateUVs(Vector3[] v/*vertices*/, float scale)
    {
        var uvs = new Vector2[v.Length];

        for (int i = 0; i < uvs.Length; i += 3)
        {
            int i0 = i;
            int i1 = i + 1;
            int i2 = i + 2;

            //Special handling if vertex count isn't a multiple of 3
            if (i == uvs.Length - 1)
            {
                i1 = 0;
                i2 = 1;
            }
            if (i == uvs.Length - 2)
            {
                i2 = 0;
            }

            Vector3 v0 = v[i0];
            Vector3 v1 = v[i1];
            Vector3 v2 = v[i2];

            Vector3 side1 = v1 - v0;
            Vector3 side2 = v2 - v0;
            var direction = Vector3.Cross(side1, side2);
            var facing = FacingDirection(direction);
            switch (facing)
            {
                case Facing.Forward:
                    uvs[i0] = ScaledUV(v0.x, v0.y, scale);
                    uvs[i1] = ScaledUV(v1.x, v1.y, scale);
                    uvs[i2] = ScaledUV(v2.x, v2.y, scale);
                    break;
                case Facing.Up:
                    uvs[i0] = ScaledUV(v0.x, v0.z, scale);
                    uvs[i1] = ScaledUV(v1.x, v1.z, scale);
                    uvs[i2] = ScaledUV(v2.x, v2.z, scale);
                    break;
                case Facing.Right:
                    uvs[i0] = ScaledUV(v0.y, v0.z, scale);
                    uvs[i1] = ScaledUV(v1.y, v1.z, scale);
                    uvs[i2] = ScaledUV(v2.y, v2.z, scale);
                    break;
            }
        }
        return new List<Vector2>(uvs);
    }

    private static bool FacesThisWay(Vector3 v, Vector3 dir, Facing p, ref float maxDot, ref Facing ret)
    {
        float t = Vector3.Dot(v, dir);
        if (t > maxDot)
        {
            ret = p;
            maxDot = t;
            return true;
        }
        return false;
    }

    private static Facing FacingDirection(Vector3 v)
    {
        var ret = Facing.Up;
        float maxDot = Mathf.NegativeInfinity;

        if (!FacesThisWay(v, Vector3.right, Facing.Right, ref maxDot, ref ret))
            FacesThisWay(v, Vector3.left, Facing.Right, ref maxDot, ref ret);

        if (!FacesThisWay(v, Vector3.forward, Facing.Forward, ref maxDot, ref ret))
            FacesThisWay(v, Vector3.back, Facing.Forward, ref maxDot, ref ret);

        if (!FacesThisWay(v, Vector3.up, Facing.Up, ref maxDot, ref ret))
            FacesThisWay(v, Vector3.down, Facing.Up, ref maxDot, ref ret);

        return ret;
    }

    private static Vector2 ScaledUV(float uv1, float uv2, float scale)
    {
        return new Vector2(uv1 / scale, uv2 / scale);
    }
}