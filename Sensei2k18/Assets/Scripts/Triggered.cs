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

    public abstract void OnActive();

    public abstract void OnDeactive();

}
