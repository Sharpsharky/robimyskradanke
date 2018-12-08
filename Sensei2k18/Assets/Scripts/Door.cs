using UnityEngine;

// KLASA DO DRZWI, NADPISZ METODY ZAMYKANIA OTWIERANIA DRZWI NIE USUWAJĄC KODU base.OnActive() i base.OnDeactive()
public class Door : Triggered
{

    public override void OnActive()
    {
        base.OnActive();

        //NATOMIAST tutaj koniecznie po base.OnActive()
        EnableAnimator( true );
        Debug.Log("Otwieram drzwi");
    }

    public override void OnDeactive()
    {
        base.OnDeactive();

        //NATOMIAST tutaj koniecznie po base.OnDeactive()
        EnableAnimator( false );
        Debug.Log( "Zamykam drzwi" );
    }

}
