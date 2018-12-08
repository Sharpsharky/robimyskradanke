using UnityEngine;
using UnityEngine.AI;

[RequireComponent( typeof( NavMeshAgent ) )]
public class Guard : MonoBehaviour
{
    [Header( "Movement Speeds" )]
    public float walkSpeed = 1.0f;
    public float runSpeed = 6.0f;

    [Header("Timers")]
    public float chaseRefresh = 0.3f;
    public float alertRefresh = 0.5f;

    [Header("Range Alert")]
    public float rangeAlertOtherGuards = 2.5f;

    [Header( "Standard path looping" )]
    public Transform[] enemyPath;
    public bool cycle = false;

    private NavMeshAgent agent;
    private Transform currentTarget;
    private Transform lastPathTarget;
    private int currentIndex = 0;
    private int arrayDirection = 1;
    private bool isLoopMoving = true;
    private bool isChasingHuman = false;
    private float currentChaseRefresh = 0f;
    private float alertCountdown = 0f;

    public void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (isLoopMoving && enemyPath.Length >= 2)
            currentTarget = enemyPath[0];
    }

    public void FixedUpdate()
    {
        if (isChasingHuman) {
            alertCountdown += Time.fixedDeltaTime;
            if ( alertCountdown >= alertRefresh) {
                alertCountdown = 0f;
                PokeNearGuards();
            }

            currentChaseRefresh += Time.fixedDeltaTime;
            if (currentChaseRefresh >= chaseRefresh) {
                ChaseHuman();
                currentChaseRefresh = 0;
            }
        }

        if (!isLoopMoving || enemyPath.Length <= 1)
            return;

        MovementLoop();
    }

    private void MovementLoop()
    {
        agent.SetDestination( currentTarget.position );
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

    public void PokeNearGuards()
    {
        foreach ( Guard guard in GameMaster.instance.Guards ) {
            if (guard.isChasingHuman)
                continue;
            if ( GameMaster.GetProperDistance(guard.transform.position, transform.position) <= rangeAlertOtherGuards  ) {
                guard.ChaseHuman();
            }
        }
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
