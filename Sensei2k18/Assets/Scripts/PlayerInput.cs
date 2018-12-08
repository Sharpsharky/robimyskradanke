using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public PlayerType playerType;
    public bool usesJoystick = false;

    private string x_axis;
    private string y_axis;
    private string action;

    public void Awake()
    {
        SetControllerAxis();
    }

    public void SetControllerAxis()
    {
        int playerNumber = 0;

        if (playerType == PlayerType.Human) {
            playerNumber = 1;
        } else {
            playerNumber = 2;
        }

        if (usesJoystick)
            SetAxisNames( "X-Axis-Pad", "Y-Axis-Pad", "Action-Pad", playerNumber );
        else
            SetAxisNames( "X-Axis-KB", "Y-Axis-KB", "Action-KB", playerNumber );

    }

    public void SetAxisNames(string x, string y, string a, int number)
    {
        x_axis = x + number;
        y_axis = y + number;
        action = a + number;
    }

    public float GetXAxis()
    {
        return Input.GetAxis( x_axis );
    }

    public float GetYAxis()
    {
        return Input.GetAxis( y_axis );
    }

    public bool IsActionDown()
    {
        return Input.GetButton( action );
    }


}
