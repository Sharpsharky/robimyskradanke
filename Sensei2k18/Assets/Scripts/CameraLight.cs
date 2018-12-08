
using UnityEngine;

//KLASA DO DZIAŁANIA KAMERY PRZY TRIGGERZE, NADPISZ METODY NIE USUWAJĄC BASE...
public class CameraLight : Triggered
{
    public new void Awake()
    {
        // JAK COŚ CHCESZ ZROBIĆ W AWAKE TO TUTAJ PRZED base.Awake();
        base.Awake();
    }

    public override void OnActive()
    {
        base.OnActive();
        //NATOMIAST tutaj koniecznie po base.OnActive()
    }

    public override void OnDeactive()
    {
        base.OnDeactive();
        //NATOMIAST tutaj koniecznie po base.OnDeactive()
    }
}
