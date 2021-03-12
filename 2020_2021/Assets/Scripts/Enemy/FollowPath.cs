using UnityEngine;

public class FollowPath : MonoBehaviour
{
    private Transform target;
    private int wayPointIndex;
    private int defaultWayPointIndex = 0;
    [SerializeField] private Enemy enemy;

    private void OnEnable()
    {
        wayPointIndex = defaultWayPointIndex;
        target = WayPointsScript.GetPoint(wayPointIndex);
    }

    private void FixedUpdate() //Nekonečný cyklus, opakuje sa v pravidelnych intervaloch
    {
        Vector2 direction = target.position - transform.position; 
        transform.Translate(direction.normalized * enemy.GetSpeed()/10 * Time.fixedDeltaTime, Space.World);

        if (Vector2.Distance(transform.position, target.position) <= 0.04)
        {
            wayPointIndex++;
            if (WayPointsScript.GetPointsLength() - 1 >= wayPointIndex)
            {
                target = WayPointsScript.GetPoint(wayPointIndex); //cieľom sa stane ďalší bod, ak existuje
            }
        } 
        enemy.ResetSpeed();
    }

    public void SetWayPointIndex(int index)
    {
        defaultWayPointIndex = index;
    }

    public int GetWayPointIndex()
    {
        return wayPointIndex;
    }
}
