using UnityEngine;

public abstract class Triggered : MonoBehaviour
{

    public bool isActivated = false;

    public void Awake()
    {
        if (isActivated)
            OnActive();
        else
            OnDeactive();
    }

    public virtual void OnActive()
    {
        isActivated = true;
    }

    public virtual void OnDeactive()
    {
        isActivated = false;
    }

}
