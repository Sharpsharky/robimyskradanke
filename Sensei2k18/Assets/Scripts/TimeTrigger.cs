using UnityEngine;

public class TimeTrigger : Trigger {

    public float timer = 5.0f;

    private float countdown = 0f;
    private bool hasChanged = false;

    public void FixedUpdate()
    {
        if (!hasChanged)
            return;

        countdown += Time.fixedDeltaTime;
        if (countdown >= timer) {
            TriggerSwitch();
            hasChanged = false;
            countdown = 0;
        }
            
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (hasChanged || triggerType != TriggerType.OnStand || !other.CompareTag( tag ))
            return;
        TriggerSwitch();
    }

    public override void OnTriggerExit(Collider other)
    {
        // NIC, TOTALNIE NIC
    }

    public override void TriggerSwitch()
    {
        base.TriggerSwitch();
        hasChanged = true;
    }
}
