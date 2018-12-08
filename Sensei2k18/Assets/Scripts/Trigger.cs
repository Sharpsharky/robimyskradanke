
using UnityEngine;

public class Trigger : MonoBehaviour
{

    public enum TriggerType
    {
        Interaction, OnTrigger
    }

    public Triggered[] triggeredObjects;
    public PlayerType playerType;
    public TriggerType triggerType;

   // private GameObject 

   /* public void Awake()
    {
        
    }*/

    public void EnableAButton(bool enable)
    {
        if ( enable ) {

        }
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (triggerType.Equals( TriggerType.OnTrigger )) {
            PlayerInput playerInput = other.GetComponent<PlayerInput>();
            if (playerInput != null) {
                TriggerSwitch( playerInput.playerType );
            }
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (triggerType.Equals( TriggerType.OnTrigger )) {
            PlayerInput playerInput = other.GetComponent<PlayerInput>();
            if (playerInput != null) {
                TriggerSwitch( playerInput.playerType );
            }
        }
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

    public virtual void TriggerSwitch(PlayerType playerType)
    {
        if (playerType == this.playerType) {

            foreach (Triggered triggered in triggeredObjects) {
                if (triggered.isActivated)
                    triggered.OnDeactive();
                else
                    triggered.OnActive();
            }

        }

    }

}
