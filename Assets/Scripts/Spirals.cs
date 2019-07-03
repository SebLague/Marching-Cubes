using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpiralIterators
{
    public static IEnumerable Flat(Vector2Int max, Vector2Int start, Vector2Int startDir)
    {
        //thanks: https://stackoverflow.com/a/398302/782170
        Vector2Int spiralPos = start;
        Vector2Int diff = startDir;
        int maxLoop = (max.x * 2 + 1) * (max.y * 2 + 1);
        for (int i=0; i< maxLoop; i++)
        {
            yield return spiralPos;

            if (spiralPos.x == max.x && spiralPos.y == max.y)
            {
                break;
            }

            if (
                (spiralPos.x <= 0 && (spiralPos.x == -spiralPos.y || spiralPos.x == spiralPos.y)) ||
                (spiralPos.x > 0 && (spiralPos.x == 1+spiralPos.y || spiralPos.x==-spiralPos.y)))
            {
                var x = diff.x;
                diff.x = diff.y;
                diff.y = -x;
            }

            spiralPos.x += diff.x;
            spiralPos.y += diff.y;


        }
    }
    public static IEnumerable Flat(Vector2Int max)
    {
        return Flat(max, new Vector2Int(0, 0), new Vector2Int(0, 1));
    }

    public static IEnumerable Cube(Vector3Int max)
    {
        Vector3Int spiralPos = new Vector3Int(0, 0, 0);
        Vector3Int diff = new Vector3Int(0, 0, 0);

        Vector2Int max2d = new Vector2Int(max.x, max.y);

        for (int xy = 0; xy <= max.x; xy++)
        {
            Vector2Int xyvec = new Vector2Int(xy, xy);
            for (int z = 0; z <= xy*2; z++)
            {
                int znew;
                if (z % 2 == 0)
                {
                    znew = z / 2;
                }
                else
                {
                    znew = -((z + 1) / 2);
                }
                
                for (int xy2 = xy-Mathf.Abs(znew); xy2 <= xy; xy2++)
                {
                    Vector2Int xy2vec = new Vector2Int(xy2, xy2);
                    int yStart = xy2;
                    if (xy2 > 0)
                    {
                        yStart -= 1;
                    }
                    foreach (Vector2Int spi in Flat(xy2vec, new Vector2Int(xy2, yStart), new Vector2Int(1, 0)))
                    {
                        yield return new Vector3Int(spi.x, spi.y, znew);
                    }
                }
            }
        }
    }
}
