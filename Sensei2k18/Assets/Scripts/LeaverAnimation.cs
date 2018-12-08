using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaverAnimation : MonoBehaviour {

    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("Wajcha open");
            anim.SetBool("Pociagnieta", true);
        }
        if (Input.GetKeyUp("space"))
        {
            Debug.Log("Wajcha close");
            anim.SetBool("Pociagnieta", false);
        }
    }
}
