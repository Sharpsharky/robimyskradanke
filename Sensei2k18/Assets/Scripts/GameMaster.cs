using System;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public float guardsChaseRadius = 5f;
    public float standardChaseTime = 5f;

    public static GameMaster instance;

    private LinkedList<Guard> guards;
    private LinkedList<FieldOfView> sourceOfLights;
    private LinkedList<FieldOfView> enemiesLights;
    private LinkedList<Trigger> interactionTriggers;
    private Human human;
    private GameObject shadow;
    private bool humanIsBeingChased = false;
    private float countdownToStopChase = 0f;

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
            EnemiesLights.AddLast( enemy.GetComponent<FieldOfView>() );
        }

        foreach (GameObject light in GameObject.FindGameObjectsWithTag( "CameraLight" )) {
            sourceOfLights.AddLast( light.GetComponent<FieldOfView>() );
        }

        foreach (GameObject trigger in GameObject.FindGameObjectsWithTag( "Trigger" )) {
            Trigger trg = trigger.GetComponent<Trigger>();
            if (trg.triggerType == Trigger.TriggerType.Interaction)
                interactionTriggers.AddLast( trg );
        }
    }

    internal bool IsInPlayersShadow()
    {
        float radius = shadow.GetComponent<SphereCollider>().radius;
        return GetProperDistance(shadow.transform.position, human.transform.position) <= radius;
    }

    public void Start()
    {
        human = GameObject.FindGameObjectWithTag( "Human" ).GetComponent<Human>();
        shadow = GameObject.FindGameObjectWithTag( "Shadow" );
    }

    public void FixedUpdate()
    {
        if (!humanIsBeingChased)
            return;

        if ( human.IsInLight()) {
            countdownToStopChase = 0f;
            Debug.Log( "Timer zresetowany" );
            return;
        }

        countdownToStopChase += Time.fixedDeltaTime;
        if (countdownToStopChase >= standardChaseTime) {
            StopChasing();
        }
    }

    public void PopHasPlayer(FieldOfView fieldOfView)
    {
        if (fieldOfView.gameObject.CompareTag( "Enemy" )) {
            if ( !humanIsBeingChased ) {
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
        return new Vector3(vectorToChange.x, 0, vectorToChange.z);
    }

    public static float GetProperDistance(Vector3 vec1, Vector3 vec2)
    {
        return Vector3.Distance(GetProperVectorY(vec1), GetProperVectorY(vec2));
    }
 

    public void LooseLevel()
    {
        Debug.Log( "Przegrałeś" );
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
