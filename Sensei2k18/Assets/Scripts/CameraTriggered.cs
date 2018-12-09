using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriggered : Triggered {

    private Transform mesh;
    private Transform cameraLight;

    public new void Awake()
    {
        base.Awake();
        mesh = transform.Find("CameraMesh");
        cameraLight = transform.Find("MigajaSzmata");
    }

    public override void OnActive()
    {
        base.OnActive();
        mesh.gameObject.SetActive(true);
        cameraLight.gameObject.SetActive( true );
    }

    public override void OnDeactive()
    {
        base.OnDeactive();
        mesh.gameObject.SetActive( false );
        cameraLight.gameObject.SetActive( false );
    }

}
