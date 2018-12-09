
using UnityEngine;

public class CameraRotor : MonoBehaviour
{

    public float angleRotation = 180f;
    public float rotateSpeed = 10f;

    private Quaternion startDestination;
    private Quaternion endDestination;
    private Quaternion currentDestination;
    private bool isEndNext = false;

    public void Start()
    {
        startDestination = transform.rotation;
        endDestination = Quaternion.Euler( 0f, startDestination.eulerAngles.y + angleRotation, 0f );
        currentDestination = endDestination;
    }

    public void Update()
    {
        transform.rotation = Quaternion.RotateTowards( transform.rotation, currentDestination, rotateSpeed * Time.deltaTime);
        if ( IsNearDestination()  ) {
           NextDestination();
        }
      
    }

    private bool IsNearDestination()
    {
        Vector3 euler1 = transform.rotation.eulerAngles;
        Vector3 euler2 = currentDestination.eulerAngles;
        return Mathf.Abs( euler1.y - euler2.y ) < 5.0f;
    }

    public void NextDestination()
    {
        if ( isEndNext ) {
            currentDestination = endDestination;
            isEndNext = !isEndNext;
        } else {
            currentDestination = startDestination;
            isEndNext = !isEndNext;
        }
    }
}
