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
        GetComponent<Rigidbody2D>().drag = 100.0f;
        input = GetComponent<PlayerInput>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    public void Movement()
    {
        float x = input.GetXAxis() * Time.deltaTime * moveSpeed;
        float y = input.GetYAxis() * Time.deltaTime * moveSpeed;

        rigidBody.velocity = new Vector2(x, y);
    }
}
