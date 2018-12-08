using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public float guardsChaseRadius = 2f;
    public float standardChaseTime = 10f;

    public static GameMaster instance;

    private LinkedList<EnemyMovement> enemies;
    private LinkedList<FieldOfView> sourceOfLights;
    private LinkedList<FieldOfView> enemiesLights;
    private Human human;
    private GameObject shadow;
    private bool humanIsBeingChased = false;
    private float countdownToStopChase = 0f;

    public void Awake()
    {
        if (instance != null)
            return;
        instance = this;

        enemies = new LinkedList<EnemyMovement>();
        sourceOfLights = new LinkedList<FieldOfView>();
        enemiesLights = new LinkedList<FieldOfView>();

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag( "Enemy" )) {
            enemies.AddLast( enemy.GetComponent<EnemyMovement>() );
            EnemiesLights.AddLast( enemy.GetComponent<FieldOfView>() );
        }

        foreach (GameObject light in GameObject.FindGameObjectsWithTag( "CameraLight" )) {
            sourceOfLights.AddLast( light.GetComponent<FieldOfView>() );
        }
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
        
        if ( human.IsInGuardFieldOfView() || human.IsInLight() ) {
            countdownToStopChase = 0f;
            Debug.Log("Timer zresetowany");
            return;
        }

        countdownToStopChase += Time.fixedDeltaTime;
        if ( countdownToStopChase >= standardChaseTime ) {
            StopChasing();
        }
    }

    public void PokeEnemiesToChaseHuman()
    {
        humanIsBeingChased = true;
        foreach (EnemyMovement enemy in enemies) {
            if (Vector3.Distance( enemy.transform.position, human.transform.position ) <= guardsChaseRadius) {
                enemy.ChaseHuman();
            }
        }
    }

    public void StopChasing()
    {
        humanIsBeingChased = false;
        countdownToStopChase = 0f;
        foreach (EnemyMovement enemy in enemies)
            enemy.StopChasing();
        Debug.Log( "Dobra, chuj z nim" );
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

    public LinkedList<EnemyMovement> Enemies
    {
        get {
            return enemies;
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
}
