using UnityEngine;

public abstract class Triggered : MonoBehaviour
{
    public bool isActivated = false;

    private Animator anim;

    public void Awake()
    {
        anim = GetComponent<Animator>();
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

    public void EnableAnimator(bool enabled)
    {
        if (anim != null) {
            anim.SetBool( "open", enabled );
        }
    }

}
