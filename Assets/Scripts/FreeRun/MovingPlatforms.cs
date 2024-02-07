using UnityEngine;
using System.Collections;

public class MovingPlatforms : MonoBehaviour
{
    public Transform[] Waypoints;
    public float maxSpeed = 3;
    public float minSpeed = 3;
    public float moveSpeed;

    public int CurrentPoint;

    private void Start()
    {
        moveSpeed = Random.Range(minSpeed, maxSpeed);
        CurrentPoint = Random.Range(0, Waypoints.Length);
    }

    void FixedUpdate()
    {
        if (transform.position != Waypoints[CurrentPoint].transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, Waypoints[CurrentPoint].transform.position, moveSpeed * Time.fixedDeltaTime);
        }

        if (transform.position == Waypoints[CurrentPoint].transform.position)
        {
            CurrentPoint += 1;
        }
        if (CurrentPoint >= Waypoints.Length)
        {
            CurrentPoint = 0;
        }
    }
}