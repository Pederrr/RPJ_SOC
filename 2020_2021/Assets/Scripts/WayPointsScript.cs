using UnityEngine;

public class WayPointsScript : MonoBehaviour
{
    private static Transform[] points;
    [SerializeField] private LineRenderer path;

    public static Transform GetPoint(int index)
    {
        return points[index];
    }

    public static int GetPointsLength()
    {
        return points.Length;
    }

    private void Awake()
    {
        points = new Transform[transform.childCount];

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }

        path.positionCount = points.Length;
        for (int j = 0; j < path.positionCount; j++)
        {
            path.SetPosition(j, points[j].position);
        }
    }
}
