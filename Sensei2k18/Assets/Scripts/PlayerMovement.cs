using UnityEngine;

[RequireComponent( typeof( PlayerInput ) )]
[RequireComponent( typeof( Rigidbody ) )]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 500f;
    public float triggerInteractionRange = 1f;

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
        if (input.IsActionDown())
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
        foreach (Trigger trigger in GameMaster.instance.InteractionTriggers) {
            if (trigger.transform.Find("A").gameObject != null)
            {
                trigger.transform.Find("A").gameObject.SetActive(true);
            }
            Debug.Log( Vector3.Distance( transform.position, trigger.transform.position ) );
            if (Vector3.Distance( transform.position, trigger.transform.position ) - 1f <= triggerInteractionRange) {
                if(trigger.transform.Find("A").gameObject != null)
                {
                    trigger.transform.Find("A").gameObject.SetActive(false);
                }
                trigger.TriggerSwitch(input.playerType);
                return;
            }
        }
    }
}
