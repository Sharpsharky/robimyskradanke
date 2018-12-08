using UnityEngine;
using UnityEngine.AI;

[RequireComponent( typeof( NavMeshAgent ) )]
public class EnemyMovement : MonoBehaviour
{
    [Header( "Human Detection/Catch" )]
    public float humanCatchDistance = 0.3f;
    public float humanDetectionDistance = 1.2f;

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

    public void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (isLoopMoving && enemyPath.Length >= 2)
            currentTarget = enemyPath[0];
    }

    public void FixedUpdate()
    {

       /* if (HumanIsCatchable()) {
            GameMaster.instance.LooseLevel();
            return;
        }

        if (HumanIsNear()) {
            currentTarget = GameMaster.instance.Human.transform;

            return;
        }*/


        if (!isLoopMoving || enemyPath.Length <= 1)
            return;

        MovementLoop();
    }

    private bool HumanIsNear()
    {
        return Vector3.Distance( GameMaster.instance.Human.transform.position, transform.position ) <= humanDetectionDistance;
    }

    private bool HumanIsCatchable()
    {
        return Vector3.Distance( GameMaster.instance.Human.transform.position, transform.position ) <= humanCatchDistance;
    }

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
        agent.SetDestination( currentTarget.position );
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
