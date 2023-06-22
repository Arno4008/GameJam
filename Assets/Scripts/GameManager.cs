using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CharacterSelection1 player1;
    public AudioSource InMainMenu;
    public AudioSource InFight;
    public PlayerController1 playercontrollerClovis1;
    public PlayerController1 playercontrollerEnzo1;
    public CharacterSelection2 player2;
    public PlayerController2 playercontrollerClovis2;
    public PlayerController2 playercontrollerEnzo2;
    public GameObject[] charactersJ1;
    public GameObject[] charactersJ2;
    public Vector3 position_J1;
    public Vector3 position_J2;
    public bool inFight;
    public GameObject Fight;
    public GameObject Option;
    public GameObject MainMenu;
    public GameObject UI;
    public GameObject CharacterSelection;
    string[] joystickNames;
    private int b;
    public bool Walls;
    public int o;

    void Start()
    {
        joystickNames = Input.GetJoystickNames();

        if (joystickNames.Length > 0)
        {
            playercontrollerClovis1.joystickNumber = 1;
            playercontrollerEnzo1.joystickNumber = 1;
            player1.joystickNumber = 1;
        }

        if (joystickNames.Length > 1)
        {
            playercontrollerClovis2.joystickNumber = 2;
            playercontrollerEnzo2.joystickNumber = 2;
            player2.joystickNumber = 2;
        }
        Fight.SetActive(false);
        UI.SetActive(false);
        MainMenu.SetActive(true);
        Option.SetActive(false);
        player1.MainMenu = true;
    }
    private void Update()
    {
        if (player1.MainMenu == true)
        {
            InFight.Stop();
            InMainMenu.Play();
        }
        if (inFight == true)
        {
            InMainMenu.Stop();
            InFight.Play();
        }
        Walls = player1.Walls;
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
    public void StartFight(int selectedCharacter, int i, int compt)
    {
        b += compt;
        if (i == 0)
        {
            for (int x = 0; x <= (charactersJ1.Length - 1); x++)
            {
                charactersJ1[x].SetActive(false);
                if (x == selectedCharacter)
                {
                    charactersJ1[x].SetActive(true);
                }
            }
        }
        if (i == 1)
        {
            for (int x = 0; x <= (charactersJ2.Length - 1); x++)
            {
                if (x == selectedCharacter)
                {
                    charactersJ2[x].SetActive(false);
                    if (x == selectedCharacter)
                    {
                        charactersJ2[x].SetActive(true);
                    }
                }
            }
        }
        if (b >= joystickNames.Length)
        {
            CharacterSelection.SetActive(false);
            Fight.SetActive(true);
            UI.SetActive(true);
            inFight = true;
        }
    }
    public void ChangeMenu(int i)
    {
        if (i == 0)
        {
            MainMenu.SetActive(false);
            CharacterSelection.SetActive(true);
        }
        if (i == 1)
        {
            MainMenu.SetActive(false);
            Option.SetActive(true);
        }
        if (i == 2)
        {
            Debug.Log("Quit");
        }
    }
}
