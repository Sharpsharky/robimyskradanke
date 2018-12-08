using UnityEngine;

public class Human : MonoBehaviour
{

    public float timeToChase = 1.5f;

    private float countdown = 0f;

    public void FixedUpdate()
    {
        if (GameMaster.instance.HumanIsBeingChased)
            return;

        if (IsInLight()) {
            countdown += Time.fixedDeltaTime;
            if (countdown >= timeToChase) {
                GameMaster.instance.PokeEnemiesToChaseHuman();
                countdown = 0f;
                Debug.Log( "Gonimy skurwiela!" );
            }
        } else if (IsInGuardFieldOfView()) {
            GameMaster.instance.PokeEnemiesToChaseHuman();
            Debug.Log( "Widziałem skurwiela, gonimy go!" );
        } else {
            countdown -= Time.fixedDeltaTime;
        }
    }

    public bool IsInGuardFieldOfView()
    {
        foreach (FieldOfView light in GameMaster.instance.EnemiesLights) {
            if (light.visibleTargets.Count >= 1)
                return true;
        }
        return false;
    }

    public bool IsInLight()
    {
        foreach (FieldOfView light in GameMaster.instance.SourceOfLights) {
            if (light.visibleTargets.Count >= 1)
                return true;
        }
        return false;
    }



}
