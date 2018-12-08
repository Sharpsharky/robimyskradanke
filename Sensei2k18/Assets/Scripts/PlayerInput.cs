using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public enum PlayerType
    {
        Human, Shadow
    }

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
        int amountOfJoys = Input.GetJoystickNames().Length;
        Debug.Log( amountOfJoys );

        int playerNumber = 0;
        if (playerType == PlayerType.Human) {
            playerNumber = 1;
            /*  // Jak nie ma padów podłączonych to człowiekowi przypisuje klawiature
              if (amountOfJoys == 0) {
                  SetAxisNames( "X-Axis-KB", "Y-Axis-KB", "Action-KB", playerNumber );
                  return;
              }*/
        } else {
            playerNumber = 2;
            /* // Jak jest tylko jeden pad lub ich nie ma to cieniowi przypisuje klawiature
             if (amountOfJoys <= 1) {

                 return;
             }*/

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
