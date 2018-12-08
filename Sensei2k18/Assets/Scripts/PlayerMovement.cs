using UnityEngine;

[RequireComponent( typeof( PlayerInput ) )]
[RequireComponent( typeof( Rigidbody ) )]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 500f;
    public float triggerInteractionRange = 0.5f;

    private PlayerInput input;
    private Rigidbody rigidBody;

    public void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        Movement();
        if ( input.IsActionDown() )
            Action();
    }

    public void Movement()
    {     
        float x = input.GetXAxis() * Time.fixedDeltaTime * moveSpeed;
        float y = input.GetYAxis() * Time.fixedDeltaTime * moveSpeed;
        rigidBody.velocity = new Vector3( x, 0, y );
    }

    public void Action()
    {
        foreach ( Trigger trigger in GameMaster.instance.InteractionTriggers) {
            if (Vector3.Distance(transform.position, trigger.transform.position) <= triggerInteractionRange ) {
                trigger.TriggerSwitch();
                return;
            }
        }
    }
}
