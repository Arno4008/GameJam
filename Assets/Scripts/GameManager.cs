using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController1 player1;
    public PlayerController2 player2;
    public bool inFight;

    void Start()
    {
        string[] joystickNames = Input.GetJoystickNames();

        if (joystickNames.Length > 0)
        {
            player1.joystickNumber = 1;
        }

        if (joystickNames.Length > 1)
        {
            player2.joystickNumber = 2;
        }
    }
    private void Update()
    {
        if (!inFight) 
        { 
            inFight = true;
        }
    }
}
