using UnityEngine;

public class Shadow : MonoBehaviour
{

    public float timeToDie = 2.5f;

    private float countdown = 0f;
    private GameMaster gameMaster;

    public void Start()
    {
        gameMaster = GameMaster.instance;
    }

    void FixedUpdate()
    {
        if (!IsInAnySourceOfLight()) {
            countdown += Time.deltaTime;
            if (countdown >= timeToDie)
                gameMaster.LooseLevel();
        }
    }

    public bool IsInAnySourceOfLight()
    {
        foreach (FieldOfView light in gameMaster.SourceOfLights) {
            if (light.HasShadow())
                return true;
        }

        foreach (FieldOfView light in gameMaster.EnemiesLights) {
            if (light.HasShadow())
                return true;
        }
        return false;
    }
}
