﻿using UnityEngine;
using UnityEngine.AI;

[RequireComponent( typeof( NavMeshAgent ) )]
public class EnemyMovement : MonoBehaviour
{
    [Header( "Movement Speeds" )]
    public float walkSpeed = 1.0f;
    public float runSpeed = 4.0f;
    public float chaseRefresh = 0.3f;

    [Header( "Standard path looping" )]
    public Transform[] enemyPath;
    public bool cycle = false;

    private NavMeshAgent agent;
    private Vector3 lastPosition;
    private Transform currentTarget;
    private Transform lastPathTarget;
    private int currentIndex = 0;
    private int arrayDirection = 1;
    private bool isLoopMoving = true;
    private bool isChasingHuman = false;
    private float currentChaseRefresh = 0f;

    public void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (isLoopMoving && enemyPath.Length >= 2)
            currentTarget = enemyPath[0];
    }

    public void FixedUpdate()
    {
        if (isChasingHuman) {
            currentChaseRefresh += Time.fixedDeltaTime;
            if (currentChaseRefresh >= chaseRefresh) {
                ChaseHuman();
                currentChaseRefresh = 0;
            }
        }

       /* if (HumanIsNear() && !isChasingHuman) {
            ChaseHuman();
            return;
        }*/

        if (!isLoopMoving || enemyPath.Length <= 1)
            return;

        MovementLoop();
    }

/*    private bool HumanIsNear()
    {
        SphereCollider humanCollider = GameMaster.instance.Human.GetComponent<SphereCollider>();
        return Vector3.Distance( GameMaster.instance.Human.transform.position, transform.position ) <= humanDetectionDistance + humanCollider.radius;
    }*/

    private void MovementLoop()
    {
        agent.SetDestination( currentTarget.position );
        if (Vector3.Distance( currentTarget.position, transform.position ) < 0.4f) {
            NextPoint();
        }
        lastPathTarget = currentTarget;
        lastPosition = transform.position;
    }

    private void NextPoint()
    {
        if (cycle && currentIndex == enemyPath.Length - 1) {
            currentIndex = -1;
        } else if (( arrayDirection == 1 && currentIndex == enemyPath.Length - 1 )
            || ( arrayDirection == -1 && currentIndex == 0 )) {
            arrayDirection *= -1;
        }

        currentIndex += arrayDirection;
        currentTarget = enemyPath[currentIndex];
    }

    public void ChaseHuman()
    {
        isChasingHuman = true;
        isLoopMoving = false;
        agent.speed = runSpeed;
        currentTarget = GameMaster.instance.Human.transform;
        agent.SetDestination( currentTarget.position );
    }

    public void StopChasing()
    {
        agent.speed = walkSpeed;
        currentTarget = lastPathTarget;
        isChasingHuman = false;
        isLoopMoving = true;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if ( collision.gameObject.CompareTag("Human") )
            GameMaster.instance.LooseLevel();
    }

    public bool IsLoopMoving
    {
        get {
            return isLoopMoving;
        }

        set {
            isLoopMoving = value;
        }
    }
}
