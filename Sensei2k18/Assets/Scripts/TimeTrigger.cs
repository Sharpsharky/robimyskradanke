using UnityEngine;

public class TimeTrigger : Trigger
{

    public float timer = 5.0f;

    private float countdown = 0f;
    private bool hasChanged = false;

    public void FixedUpdate()
    {
        if (!hasChanged)
            return;

        countdown += Time.fixedDeltaTime;

        if (countdown >= timer) {
            Debug.Log( "Zmiana stanu po czasie..." );
            base.TriggerSwitch( playerType );
            hasChanged = false;
            countdown = 0;
        }

    }

    public override void OnTriggerEnter(Collider other)
    {
        if (!hasChanged && triggerType == TriggerType.OnTrigger) {
            PlayerInput playerInput = other.GetComponent<PlayerInput>();
            if (playerInput != null && playerInput.playerType == playerType) {
                TriggerSwitch( playerInput.playerType );
            }
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        // NIC, TOTALNIE NIC
    }

    public override void TriggerSwitch(PlayerType playerType)
    {
        if (!hasChanged) {
            base.TriggerSwitch( playerType );
            hasChanged = true;
        }
    }
}
