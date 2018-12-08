using UnityEngine;

[RequireComponent( typeof( PlayerInput ) )]
[RequireComponent( typeof( Rigidbody2D ) )]
public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 10f;

    private PlayerInput input;
    private Rigidbody2D rigidBody;

    public void Awake()
    {
        input = GetComponent<PlayerInput>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        float x = input.GetXAxis() * Time.deltaTime * moveSpeed;
        float y = input.GetYAxis() * Time.deltaTime * moveSpeed;

        rigidBody.velocity.Set( x, y );

        Vector3 moveDirection = new Vector3( x, y, 0 ) * Time.deltaTime * moveSpeed;
        transform.position = transform.position + moveDirection;

        if (moveDirection.magnitude != 0) {
            //   Quaternion rotation = Quaternion.Slerp( transform.rotation, Quaternion.LookRotation( moveDirection, transform.forward ), 0.8f );
            // transform.rotation.Set( rotation.x);
        }

    }
}
