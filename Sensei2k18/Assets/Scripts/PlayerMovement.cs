using UnityEngine;

[RequireComponent( typeof( PlayerInput ) )]
[RequireComponent( typeof( Rigidbody ) )]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 500f;
    public float triggerInteractionRange = 1f;
    public float defaultTriggerLook = 0.5f;

    private PlayerInput input;
    private Rigidbody rigidBody;
    private Trigger currentTrigger;
    private float triggerLookCountdown = 0f;

    public void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        Movement();

        triggerLookCountdown += Time.fixedDeltaTime;
        if (triggerLookCountdown >= defaultTriggerLook) {
            ShowATriggerButton();
        }

        if (currentTrigger != null) {
            currentTrigger.ShowAButton( true );
            if (input.IsActionDown()) {
                Action();
            }
        }

    }

    public void ShowATriggerButton()
    {
        Trigger oldTrigger = currentTrigger;
        if (oldTrigger != null) {
            oldTrigger.ShowAButton( false );
        }
        currentTrigger = GetFirstClosestTrigger();
    }

    public void Movement()
    {
        float x = input.GetXAxis() * Time.fixedDeltaTime * moveSpeed;
        float y = input.GetYAxis() * Time.fixedDeltaTime * moveSpeed;
        rigidBody.velocity = new Vector3( x, 0, y );
    }

    public void Action()
    {
        currentTrigger.TriggerSwitch(input.playerType);
    }

    // ZWRACA PIERWSZY ZNALEZIONY I BLISKI GRACZOWI TRIGGER
    public Trigger GetFirstClosestTrigger()
    {
        foreach (Trigger trigger in GameMaster.instance.InteractionTriggers) {
            if (Vector3.Distance( transform.position, trigger.transform.position ) - 1f <= triggerInteractionRange) {
                if (trigger.playerType == input.playerType) {
                    return trigger;
                }
            }
        }
        return null;
    }
}
