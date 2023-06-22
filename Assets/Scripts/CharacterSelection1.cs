using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class CharacterSelection1 : MonoBehaviour
{
    public Text WallsTXT;
    public Text SoundTXT;
    public GameObject[] characters;
    public GameObject[] Slots;
    public GameObject[] charactersBack;
    public GameObject[] OptionsButton;
    public GameObject[] OptionsButtonSelected;
    public GameObject[] MainMenuButton;
    public GameObject[] MainMenuButtonSelected;
    public GameObject InvisibleWalls;
    public PlayerController1 player1;
    public PlayerController2 player2;
    public int selectedCharacter = 0;
    public int SelectMainMenu = 0;
    public int SelectMainMenuButton = 0;
    public int selectedCharacterBack = 0;
    public int SelectedSlots = 0;
    public int SelectedOption = 0;
    public int SelectedOptionButtonS = 0;
    public int joystickNumber;
    public GameManager manager;
    public bool isAxisInUse;
    public bool BRefresh;
    public bool option;
    public bool MainMenu;
    public bool Walls;
    public bool Sound;
    const string format = "{0}";
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal1");
        if (horizontalInput < 0)
        {
            if (isAxisInUse == false && option == false && MainMenu == false)
            {
                selectedCharacter--;
                SelectedSlots--;
                selectedCharacterBack--;
                if (selectedCharacter < 0)
                {
                    selectedCharacterBack = charactersBack.Length;
                    selectedCharacter = characters.Length - 1;
                    SelectedSlots = Slots.Length - 1;
                }
                isAxisInUse = true;
            }
            if (isAxisInUse == false && MainMenu == true)
            {
                SelectMainMenuButton--;
                SelectMainMenu--;
                if (SelectMainMenu < 0)
                {
                    SelectMainMenu = MainMenuButton.Length - 1;
                    SelectMainMenuButton = MainMenuButtonSelected.Length - 1;
                }
                isAxisInUse = true;
            }
            if (isAxisInUse == false && option == true)
            {
                SelectedOptionButtonS--;
                SelectedOption--;
                if (SelectedOption < 0)
                {
                    SelectedOption = OptionsButton.Length - 1;
                    SelectedOptionButtonS = OptionsButtonSelected.Length - 1;
                }
                isAxisInUse = true;
            }
        }
        else if (horizontalInput > 0)
        {
            if (isAxisInUse == false && option == false && MainMenu == false)
            {
                selectedCharacter++;
                SelectedSlots++;
                selectedCharacterBack++;
                if (selectedCharacter == characters.Length)
                {
                    selectedCharacterBack = 0;
                    selectedCharacter = 0;
                    SelectedSlots = 0;
                }
                isAxisInUse = true;
            }
            if (isAxisInUse == false && MainMenu == true)
            {
                SelectMainMenuButton++;
                SelectMainMenu++;
                if (SelectMainMenu >= MainMenuButton.Length)
                {
                    SelectMainMenu = 0;
                    SelectMainMenuButton = 0;
                }
                isAxisInUse = true;
            }
            if (isAxisInUse == false && option == true && MainMenu == false)
            {
                SelectedOption++;
                SelectedOptionButtonS++;
                if (SelectedOption >= OptionsButton.Length)
                {
                    SelectedOption = 0;
                    SelectedOptionButtonS = 0;
                }
                isAxisInUse = true;
            }
        }
        if (horizontalInput == 0)
        {
            isAxisInUse = false;
        }
        if (!BRefresh)
        {
            Refresh();
        }
        if (option)
        {
            for (int i = 0; i < OptionsButton.Length; i++)
            {
                if (i == SelectedOption)
                {
                    OptionsButton[i].SetActive(true);
                    OptionsButtonSelected[i].SetActive(true);
                    BRefresh = false;
                }
            }
        }
        if (MainMenu)
        {
            for (int i = 0; i < MainMenuButton.Length; i++)
            {
                if (i == SelectMainMenu)
                {
                    MainMenuButton[i].SetActive(true);
                    MainMenuButtonSelected[i].SetActive(true);
                    BRefresh = false;
                }
            }
        }
        if (!option) 
        {
            for (int i = 0; i < characters.Length; i++)
            {
                if (i == selectedCharacter)
                {
                    characters[i].SetActive(true);
                    Slots[i].SetActive(true);
                    charactersBack[i].SetActive(true);
                    BRefresh = false;
                }
            }
        }
        if (Input.GetButtonDown("Fire1." + joystickNumber) && option == false && MainMenu == false)
        {
            manager.StartFight(selectedCharacter, i);
        }
        if (Input.GetButtonDown("Fire1." + joystickNumber) && MainMenu == true)
        {
            if (SelectMainMenu == 0 && MainMenu == true)
            {
                manager.ChangeMenu(0);
                MainMenu = false;
            }
            if (SelectMainMenu == 1 && MainMenu == true)
            {
                manager.ChangeMenu(1);
                MainMenu = false;
                option = true;
            }
            if (SelectMainMenu == 2 && MainMenu == true)
            {
                manager.ChangeMenu(2);
            }
        }
        if (Input.GetButtonDown("Fire2") && option == true)
        {
            manager.Option.SetActive(false);
            manager.MainMenu.SetActive(true);
            option = false;
            MainMenu = true;
        }
        if (Input.GetButtonDown("Fire2") && option == false && MainMenu == false && manager.inFight == false)
        {
            manager.Fight.SetActive(false);
            manager.MainMenu.SetActive(true);
            MainMenu = true;
        }
        if (Input.GetButtonDown("Fire1." + joystickNumber) && option == true)
        {
            if (SelectedOptionButtonS == 0 && Walls == true)
            {
                WallsTXT.text = string.Format(format, "OFF");
                Walls = false;
                InvisibleWalls.SetActive(false);
            } else if (SelectedOptionButtonS == 0 && Walls == false)
            {
                WallsTXT.text = string.Format(format, "ON");
                Walls = true;
                InvisibleWalls.SetActive(true);
            } else if (SelectedOptionButtonS == 1 && Sound == true)
            {
                SoundTXT.text = string.Format(format, "OFF");
                Sound = false;
            } else if (SelectedOptionButtonS == 1 && Sound == false)
            {
                SoundTXT.text = string.Format(format, "ON");
                Sound = true;
            }
        }
        if (Input.GetButtonDown("Start") && MainMenu == false && manager.inFight == false)
        {
            if (option)
            {
                if (manager.inFight)
                {
                    option = false;
                    manager.Option.SetActive(false);
                } else
                {
                    option = false;
                    manager.CharacterSelection.SetActive(true);
                    manager.Option.SetActive(false);
                }

            } else if (!option)
            {
                if (manager.inFight)
                {
                    option = true;
                    manager.Option.SetActive(true);
                }
            }
        }
        player1.option = option;
        player2.option = option;
    }
    void Refresh()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].SetActive(false);
            charactersBack[i].SetActive(false);
        }
        for (int i = 0; i < OptionsButtonSelected.Length; i++)
        {
            OptionsButtonSelected[i].SetActive(false);
        }
        for (int i = 0; i < MainMenuButtonSelected.Length; i++)
        {
            MainMenuButtonSelected[i].SetActive(false);
        }
        BRefresh = true;
    }
}