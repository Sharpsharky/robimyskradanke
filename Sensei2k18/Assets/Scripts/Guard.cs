using UnityEngine;
using UnityEngine.AI;

[RequireComponent( typeof( NavMeshAgent ) )]
public class Guard : MonoBehaviour
{
    [Header( "Movement Speeds" )]
    public float walkSpeed = 1.0f;
    public float runSpeed = 6.0f;

    [Header( "Timers" )]
    public float chaseRefresh = 0.3f;
    public float alertRefresh = 0.5f;

    [Header( "Range Alert" )]
    public float rangeAlertOtherGuards = 2.5f;

    [Header( "Standard path looping" )]
    public Transform[] enemyPath;
    public bool cycle = false;
    public float loopRefresh = 1f;

    private NavMeshAgent agent;
    private Transform currentTarget;
    private Transform lastPathTarget;

    private int currentIndex = 0;
    private int arrayDirection = 1;
    private bool isLoopMoving = true;
    private bool isChasingHuman = false;
    private float currentChaseRefresh = 0f;
    private float alertCountdown = 0f;

    private float movementLoopCountdown = 0f;

    public void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (isLoopMoving && HasPath()) {
            currentTarget = enemyPath[0];
            agent.SetDestination( currentTarget.position );
        }
            
    }

    public void FixedUpdate()
    {
        if (isChasingHuman) {
            alertCountdown += Time.fixedDeltaTime;
            if (alertCountdown >= alertRefresh) {
                alertCountdown = 0f;
                PokeNearGuards();
            }

            currentChaseRefresh += Time.fixedDeltaTime;
            if (currentChaseRefresh >= chaseRefresh) {
                ChaseHuman();
                currentChaseRefresh = 0;
            }
        }

        if (!isLoopMoving || !HasPath())
            return;

        movementLoopCountdown += Time.fixedDeltaTime;
        if (movementLoopCountdown >= loopRefresh) {
            MovementLoop();
            movementLoopCountdown = 0f;
        }

    }

    private void MovementLoop()
    {
        if (Vector3.Distance( currentTarget.position, transform.position ) < 0.4f) {
            NextPoint();
        }
        lastPathTarget = currentTarget;
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
        agent.SetDestination( currentTarget.position );
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
        isChasingHuman = false;
        agent.speed = walkSpeed;
        currentTarget = lastPathTarget;
        isLoopMoving = true;
        if ( HasPath() ) {
            agent.SetDestination( currentTarget.position );
        }
    }

    public bool HasPath()
    {
        return enemyPath.Length > 1;
    }

    public void PokeNearGuards()
    {
        foreach (Guard guard in GameMaster.instance.Guards) {
            if (guard.isChasingHuman)
                continue;
            if (GameMaster.GetProperDistance( guard.transform.position, transform.position ) <= rangeAlertOtherGuards) {
                guard.ChaseHuman();
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag( "Human" ))
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
