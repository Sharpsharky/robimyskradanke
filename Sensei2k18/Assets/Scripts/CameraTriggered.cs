using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriggered : Triggered {

    private GameObject rest;

    public new void Awake()
    {
        rest = transform.Find( "Rest" ).gameObject;
        base.Awake();
    }

    public override void OnActive()
    {
        base.OnActive();
        rest.SetActive(true);
    }

    public override void OnDeactive()
    {
        base.OnDeactive();
        rest.SetActive( false );
    }

}
