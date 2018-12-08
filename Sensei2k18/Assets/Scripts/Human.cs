using UnityEngine;

public class Human : MonoBehaviour
{

    public float timeToChase = 1.5f;

    private float countdown = 0f;
    private GameMaster gameMaster;

    public void Start()
    {
        gameMaster = GameMaster.instance;
    }

    public void FixedUpdate()
    {
        if (GameMaster.instance.HumanIsBeingChased)
            return;

        if (IsInLight()) {
            countdown += Time.fixedDeltaTime;
            if (countdown >= timeToChase) {
                GameMaster.instance.PokeEnemiesToChaseHuman();
                countdown = 0f;
                Debug.Log( "Gonimy skurwiela na kamerze!" );
            }
        } else if (IsInGuardFieldOfView()) {
            GameMaster.instance.PokeEnemiesToChaseHuman();
            Debug.Log( "Widziałem skurwiela, gonimy go!" );
        } else {
            countdown -= Time.fixedDeltaTime;
            if (countdown <= 0)
                countdown = 0;
        }
    }

    public bool IsInGuardFieldOfView()
    {
        foreach (FieldOfView light in gameMaster.EnemiesLights) {
            if (light.HasHuman())
                return true;
        }
        return false;
    }

    public bool IsInLight()
    {
        if (gameMaster.IsInPlayersShadow())
            return false;
        foreach (FieldOfView light in gameMaster.SourceOfLights) {
            if (light.HasHuman())
                return true;
        }
        return false;
    }



}
