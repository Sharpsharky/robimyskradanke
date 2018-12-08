using UnityEngine;

[RequireComponent( typeof( PlayerInput ) )]
public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 10f;
    public PlayerInput input;

    public void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        float x = input.GetXAxis();
        float y = input.GetYAxis();

        Vector3 moveDirection = new Vector3( x, y, 0 ) * Time.deltaTime * moveSpeed;
        transform.position = transform.position + moveDirection;

        if (moveDirection.magnitude != 0) {
        //   Quaternion rotation = Quaternion.Slerp( transform.rotation, Quaternion.LookRotation( moveDirection, transform.forward ), 0.8f );
           // transform.rotation.Set( rotation.x);
        }

    }
}
