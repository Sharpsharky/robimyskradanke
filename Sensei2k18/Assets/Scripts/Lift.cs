using UnityEngine;

public class Lift : MonoBehaviour
{
    public float exitLevelTime = 3.0f;

    private bool hasHuman = false;
    private bool hasShadow = false;
    private bool isCountingToExitLevel = false;
    private float countdown = 0f;

    public void Update()
    {
        if (!isCountingToExitLevel)
            return;
        countdown += Time.deltaTime;
        if (countdown >= exitLevelTime)
            ExitLevel();
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag( "Human" )) {
            hasHuman = true;
            Debug.Log( "Human sie wbil" );
        } else if (collider.CompareTag( "Shadow" )) {
            hasShadow = true;
            Debug.Log( "Shadow sie wbil" );
        }

        if (HasBothPlayers()) {
            isCountingToExitLevel = true;
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag( "Human" )) {
            hasHuman = false;
            Debug.Log( "Human wypierdolil" );
        } else if (collider.CompareTag( "Shadow" )) {
            hasShadow = false;
            Debug.Log( "Shadow wypierdolil" );
        }

        isCountingToExitLevel = false;
        countdown = 0f;
    }

    public bool HasBothPlayers()
    {
        if (hasHuman && hasShadow)
            return true;
        return false;
    }

    public void ExitLevel()
    {
        Debug.Log("Wypierdalamy stad");
    }

}
