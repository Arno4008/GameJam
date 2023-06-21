using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CharacterSelection1 player1;
    public PlayerController1 playercontroller1;
    public CharacterSelection2 player2;
    public PlayerController2 playercontroller2;
    public bool inFight;
    public GameObject Fight;
    public GameObject UI;
    public GameObject CharacterSelection;
    string[] joystickNames;
    private int b;

    void Start()
    {
        joystickNames = Input.GetJoystickNames();

        if (joystickNames.Length > 0)
        {
            playercontroller1.joystickNumber = 1;
            player1.joystickNumber = 1;
        }

        if (joystickNames.Length > 1)
        {
            playercontroller2.joystickNumber = 2;
            player2.joystickNumber = 2;
        }
        Fight.SetActive(false);
        UI.SetActive(false);
    }
    public void End(int i)
    {
        if (i == 1)
        {
            Debug.Log("Victoire du Joueur 2");
        }
        if (i == 2)
        {
            Debug.Log("Victoire du Joueur 1");
        }
    }
    public void StartFight(int i)
    {
        b += i;
        Debug.Log("B= " + b + "I= " + i);
        Debug.Log("JoystickNames Lenght= " + joystickNames.Length);
        if (b >= joystickNames.Length)
        {
            CharacterSelection.SetActive(false);
            Fight.SetActive(true);
            UI.SetActive(true);
            inFight = true;
        }
    }
}
