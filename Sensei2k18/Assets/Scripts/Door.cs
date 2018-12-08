using UnityEngine;


// KLASA DO DRZWI, NADPISZ METODY ZAMYKANIA OTWIERANIA DRZWI NIE USUWAJĄC KODU base.OnActive() i base.OnDeactive()
public class Door : Triggered
{
    // To jest przykład dla drzwi który działa dobrze... powinien xD
    private Animator anim;

    public new void Awake()
    {
        // JAK COŚ CHCESZ ZROBIĆ W AWAKE TO TUTAJ PRZED base.Awake() jak np przypisanie komponentu
        anim = GetComponent<Animator>();
        base.Awake();     
    }

    public override void OnActive()
    {
        base.OnActive();
        //NATOMIAST tutaj koniecznie po base.OnActive()
        anim.SetBool( "cienStoiNaPikawie", true );
        Debug.Log("Otwieram drzwi");
    }

    public override void OnDeactive()
    {
        base.OnDeactive();
        anim.SetBool( "cienStoiNaPikawie", false );
        //NATOMIAST tutaj koniecznie po base.OnDeactive()
        Debug.Log( "Zamykam drzwi" );
    }

}
