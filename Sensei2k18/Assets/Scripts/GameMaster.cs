using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;

    private LinkedList<EnemyMovement> enemies;
    private GameObject human;
    private GameObject shadow;
    private bool humanIsBeingChased = false;

    public void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }

    public void Start()
    {
        human = GameObject.FindGameObjectWithTag( "Human" );
        shadow = GameObject.FindGameObjectWithTag( "Shadow" );
    }

    public void ChaseHuman()
    {
        humanIsBeingChased = true;
        foreach (EnemyMovement enemy in enemies)
            enemy.ChaseHuman();
    }

    public void StopChasing()
    {
        humanIsBeingChased = false;
        foreach (EnemyMovement enemy in enemies)
            enemy.StopChasing();
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

    public GameObject Human
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
}
