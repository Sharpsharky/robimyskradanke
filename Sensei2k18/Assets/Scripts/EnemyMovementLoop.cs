
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float moveSpeed = 1.5f;
    public Transform[] enemyPath;

    private Vector3 lastPosition;
    private Transform currentPoint;
    private int currentIndex = 0;
    private int arrayDirection = 1;
    private bool isLoopMoving = true;

    public void Update()
    {
        if (!isLoopMoving || enemyPath.Length <= 1)
            return;

        MovementLoop();
    }

    private void MovementLoop()
    {
        Vector3 direction = ( currentPoint.position - transform.position ).normalized;
        transform.TransformDirection( direction * moveSpeed * Time.deltaTime );

        if (Vector3.Distance( currentPoint.position, transform.position ) < 0.5f) {
            NextPoint();
        }

        lastPosition = transform.position;
    }

    private void NextPoint()
    {

        if (currentIndex == enemyPath.Length - 1 || currentIndex == 0) {
            arrayDirection *= -1;
        }

        currentIndex += arrayDirection;
        currentPoint = enemyPath[currentIndex];
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
