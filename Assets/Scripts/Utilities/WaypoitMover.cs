using UnityEngine;

public class WaypoitMover : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed = 3f;

    private int currentPosition = 0;
    private int direction = 1; // 1 Forward  -1 Backwards


    // GAME //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    void Update()
    {
        if (waypoints.Length == 0) return;

        Transform target = waypoints[currentPosition];

        // Movement towards waypoint
        transform.position = Vector3.MoveTowards( transform.position, target.position, speed * Time.deltaTime );

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentPosition += direction;
            SwitchDirections();
        }
    }


    // FUNCTIONS //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    private void SwitchDirections()
    {
        if (currentPosition >= waypoints.Length)
        {
            currentPosition = waypoints.Length - 2;
            direction = -1;
        }
        else if (currentPosition < 0)
        {
            currentPosition = 1;
            direction = 1;
        }
    }
}