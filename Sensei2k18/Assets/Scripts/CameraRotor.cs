
using UnityEngine;

public class CameraRotor : MonoBehaviour
{

    public enum Direction
    {
        Left, Right
    }

    public float startAngle = 0f;
    public float angleRotation = 180f;
    public float rotateSpeed = 0.2f;

    private Quaternion startDestination;
    private Quaternion endDestination;
    private Quaternion currentDestination;
    private bool isEndNext = false;

    public void Start()
    {
        transform.rotation = Quaternion.Euler( 0f, startAngle, 0f );
        startDestination = transform.rotation;
        endDestination = Quaternion.Euler( 0f, startAngle + angleRotation, 0f );
        currentDestination = endDestination;
    }

    public void Update()
    {
        transform.rotation = Quaternion.Slerp( transform.rotation, currentDestination, rotateSpeed );
        if ( transform.rotation == currentDestination ) {
            NextDestination();
        }
      
    }

    public void NextDestination()
    {
        if ( isEndNext ) {
            currentDestination = endDestination;
            isEndNext = !isEndNext;
        } else {

        }
    }
}
