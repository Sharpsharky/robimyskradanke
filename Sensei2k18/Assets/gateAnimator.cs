using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateAnimator : MonoBehaviour {

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKey("space"))
        {
            Debug.Log("OPEN THE GATE");
            anim.SetBool("cienStoiNaPikawie", true);
        }
        if (Input.GetKeyUp("space"))
        {
            Debug.Log("CLOSE THE GATE");
            anim.SetBool("cienStoiNaPikawie", false);
        }
    }

}
