using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public float guardsChaseRadius = 5f;
    public float standardChaseTime = 5f;
    public float timeToChase = 1.5f;

    public static GameMaster instance;

    private LinkedList<Guard> guards;
    private LinkedList<FieldOfView> sourceOfLights;
    private LinkedList<FieldOfView> enemiesLights;
    private LinkedList<Trigger> interactionTriggers;
    private Human human;
    private GameObject shadow;
    private bool humanIsBeingChased = false;
    private float countdownToStopChase = 0f;
    private float countdownToStartChase = 0f;

    public void Awake()
    {
        if (instance != null)
            return;
        instance = this;

        guards = new LinkedList<Guard>();
        sourceOfLights = new LinkedList<FieldOfView>();
        enemiesLights = new LinkedList<FieldOfView>();
        interactionTriggers = new LinkedList<Trigger>();

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag( "Enemy" )) {
            guards.AddLast( enemy.GetComponent<Guard>() );
            FieldOfView view = enemy.GetComponent<FieldOfView>();
            if (view != null) {
                EnemiesLights.AddLast( view );
            }
        }

        foreach (GameObject light in GameObject.FindGameObjectsWithTag( "CameraLight" )) {
            sourceOfLights.AddLast( light.GetComponent<FieldOfView>() );
        }

        foreach (GameObject trigger in GameObject.FindGameObjectsWithTag( "Trigger" )) {
            Trigger trg = trigger.GetComponent<Trigger>();
            if (trg != null) {
                if (trg.triggerType == Trigger.TriggerType.Interaction)
                    interactionTriggers.AddLast( trg );
            }
        }
    }

    internal bool IsInPlayersShadow()
    {
        float radius = shadow.GetComponent<SphereCollider>().radius * shadow.transform.localScale.x;
        return GetProperDistance( shadow.transform.position, human.transform.position ) <= radius;
    }

    public void Start()
    {
        human = GameObject.FindGameObjectWithTag( "Human" ).GetComponent<Human>();
        shadow = GameObject.FindGameObjectWithTag( "Shadow" );
        StartCoroutine( "CheckLights", 0.25f );
    }

    public void FixedUpdate()
    {
        if (!humanIsBeingChased)
            return;

        if (IsInPlayersShadow() || human.IsInGuardFieldOfView()) {
            countdownToStopChase = 0f;
            Debug.Log( "Timer zresetowany" );
            return;
        }

        countdownToStopChase += Time.fixedDeltaTime;
        if (countdownToStopChase >= standardChaseTime) {
            StopChasing();
        }
    }

    public bool CheckCameraLight()
    {
        foreach (FieldOfView field in sourceOfLights) {
            if (field == null)
                continue;
            if (field.FindVisibleTargets()) {
                return true;
            }
        }
        return false;
    }

    public bool CheckEnemyLight()
    {
        foreach (FieldOfView field in enemiesLights) {
            if (field == null)
                continue;
            if (field.FindVisibleTargets()) {
                return true;
            }
        }
        return false;
    }

    IEnumerator CheckLights(float delay)
    {
        while (true) {

            bool enemy = CheckEnemyLight();
            bool camera = CheckCameraLight();

            if (enemy) {
                // JAK GO NIE GONIĄ TO ZACZNĄ

                if (!humanIsBeingChased) {
                    PokeEnemiesToChaseHuman();
                }
            }

            bool isInShadow = IsInPlayersShadow();
            Debug.Log( "Is in shadow: " + isInShadow );
            if (camera) {

                if (!SceneAudioManager.instance.IsPlayingBad)
                    SceneAudioManager.instance.ChangeOnColissionenter();

                countdownToStartChase += delay;
                Debug.Log( "Countdown: " + countdownToStartChase );

                if (!humanIsBeingChased && countdownToStartChase >= timeToChase) {
                    PokeEnemiesToChaseHuman();
                }
            } else {
                countdownToStartChase -= delay;
                if (countdownToStartChase < 0f)
                    countdownToStartChase = 0f;
            }

            if ( !camera && !enemy && SceneAudioManager.instance.IsPlayingBad) {
                SceneAudioManager.instance.ChangeOnColissionExit();
            }

            if (humanIsBeingChased) {
                if (camera || enemy) {
                    // REFRESHUJE CZAS DO POZOSTAWIENIA W SPOKOJU JEŻELI GO GONIĄ
                    countdownToStopChase = 0f;
                } else {
                    // DOADJE CZAS JEŻELI NIE BYŁ W ŻADNYM ŚWIETLE
                    countdownToStopChase += delay;
                    if (countdownToStopChase >= standardChaseTime) {
                        StopChasing();
                    }
                }
            }

            yield return new WaitForSeconds( delay );
        }
    }

    public void PopHasPlayer(FieldOfView fieldOfView)
    {
        if (fieldOfView.gameObject.CompareTag( "Enemy" )) {
            if (!humanIsBeingChased) {
                PokeEnemiesToChaseHuman();
                Debug.Log( "Widziałem skurwiela, gonimy go!" );
            } else {
                countdownToStopChase = 0f;
            }
        }

    }

    public void PokeEnemiesToChaseHuman()
    {
        int chasingEnemies = 0;
        humanIsBeingChased = true;
        foreach (Guard enemy in guards) {
            if (Vector3.Distance( enemy.transform.position, human.transform.position ) <= guardsChaseRadius) {
                enemy.ChaseHuman();
                chasingEnemies++;
            }
        }
        if (chasingEnemies == 0) {
            humanIsBeingChased = false;
        }
    }

    public void StopChasing()
    {
        humanIsBeingChased = false;
        countdownToStopChase = 0f;
        foreach (Guard enemy in guards)
            enemy.StopChasing();
        Debug.Log( "Dobra, chuj z nim" );
    }

    public static Vector3 GetProperVectorY(Vector3 vectorToChange)
    {
        return new Vector3( vectorToChange.x, 0, vectorToChange.z );
    }

    public static float GetProperDistance(Vector3 vec1, Vector3 vec2)
    {
        return Vector3.Distance( GetProperVectorY( vec1 ), GetProperVectorY( vec2 ) );
    }


    public void LooseLevel()
    {
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );
    }

    public GameObject Shadow
    {
        get {
            return shadow;
        }
    }

    public Human Human
    {
        get {
            return human;
        }
    }

    public LinkedList<Guard> Guards
    {
        get {
            return guards;
        }
    }

    public bool HumanIsBeingChased
    {
        get {
            return humanIsBeingChased;
        }
    }

    public LinkedList<FieldOfView> SourceOfLights
    {
        get {
            return sourceOfLights;
        }
    }

    public LinkedList<FieldOfView> EnemiesLights
    {
        get {
            return enemiesLights;
        }
    }

    public LinkedList<Trigger> InteractionTriggers
    {
        get {
            return interactionTriggers;
        }

    }
}
