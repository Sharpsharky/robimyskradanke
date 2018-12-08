
using UnityEngine;

public class Trigger : MonoBehaviour
{

    public enum TriggerType
    {
        Interaction, OnStand
    }

    public Triggered[] triggeredObjects;
    public PlayerType playerType;
    public TriggerType triggerType;

    protected string tag;

    public void Awake()
    {
        switch (playerType) {
            case PlayerType.Human:
                tag = "Human";
                break;
            case PlayerType.Shadow:
                tag = "Shadow";
                break;
        }
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (triggerType != TriggerType.OnStand || !other.CompareTag( tag ))
            return;
        TriggerOn();
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (triggerType != TriggerType.OnStand || !other.CompareTag( tag ))
            return;
        TriggerOff();
    }

    public virtual void TriggerOn()
    {
        foreach (Triggered triggered in triggeredObjects) {
            triggered.OnActive();
        }
    }

    public virtual void TriggerOff()
    {
        foreach (Triggered triggered in triggeredObjects) {
            triggered.OnDeactive();
        }
    }

    public virtual void TriggerSwitch()
    {
        foreach (Triggered triggered in triggeredObjects) {
            if (triggered.isActivated)
                triggered.OnDeactive();
            else
                triggered.OnActive();
        }
    }

}
