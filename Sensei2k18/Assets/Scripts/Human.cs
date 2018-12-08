using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour {

    public float timeToChase = 1.5f;

    private float countdown = 0f;

    public void FixedUpdate()
    {
        if ( IsPlayerInLight() ) {
            if (GameMaster.instance.HumanIsBeingChased)
                return;
            countdown += Time.fixedDeltaTime;
            if ( countdown >= timeToChase ) {
                GameMaster.instance.ChaseHuman();
                countdown = 0f;
            }
        }
    }

    public bool IsPlayerInLight()
    {
        return false;
    }

}
