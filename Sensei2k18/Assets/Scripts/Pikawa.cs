using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pikawa : MonoBehaviour {

    public GameObject targetGate;
    

    

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Shadow")
        {
            targetGate.GetComponent<gateAnimator>().anim.SetBool("cienStoiNaPikawie", true);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Shadow")
        {
            targetGate.GetComponent<gateAnimator>().anim.SetBool("cienStoiNaPikawie", false);
        }
    }

}
