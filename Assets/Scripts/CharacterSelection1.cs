using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class CharacterSelection1 : MonoBehaviour
{
    public Text WallsTXT;
    public Text WallsSelectedTXT;
    public Text SoundTXT;
    public Text SoundSelectedTXT;
    public GameObject[] characters;
    public GameObject[] Slots;
    public GameObject[] charactersBack;
    public GameObject[] OptionsButton;
    public GameObject[] OptionsButtonSelected;
    public GameObject InvisibleWalls;
    public PlayerController1 player1;
    public PlayerController2 player2;
    public int selectedCharacter = 0;
    public int selectedCharacterBack = 0;
    public int SelectedSlots = 0;
    public int SelectedOption = 0;
    public int SelectedOptionButtonS = 0;
    public int joystickNumber;
    public GameManager manager;
    public bool isAxisInUse;
    public bool BRefresh;
    public bool option;
    public bool Walls;
    public bool Sound;
    const string format = "{0}";
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal1");
        if (horizontalInput < 0)
        {
            if (isAxisInUse == false && option == false)
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
            if (isAxisInUse == false && option == true)
            {
                SelectedOptionButtonS--;
                SelectedOption--;
                if (SelectedOption < 0)
                {
                    SelectedOption = OptionsButton.Length;
                    SelectedOptionButtonS = OptionsButtonSelected.Length;
                }
                isAxisInUse = true;
            }
        }
        else if (horizontalInput > 0)
        {
            if (isAxisInUse == false && option == false)
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
            if (isAxisInUse == false && option == true)
            {
                SelectedOption++;
                SelectedOptionButtonS++;
                if (selectedCharacter == characters.Length)
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
        if (Input.GetButtonDown("Fire1." + joystickNumber) && option == false)
        {
            manager.StartFight(1);
        }
        if (Input.GetButtonDown("Fire1." + joystickNumber) && option == true)
        {
            if (SelectedOptionButtonS == 0 && Walls == true)
            {
                WallsTXT.text = string.Format(format, "OFF");
                WallsSelectedTXT.text = string.Format(format, "OFF");
                Walls = false;
                InvisibleWalls.SetActive(false);
            } else if (SelectedOptionButtonS == 0 && Walls == false)
            {
                WallsTXT.text = string.Format(format, "ON");
                WallsSelectedTXT.text = string.Format(format, "ON");
                Walls = true;
                InvisibleWalls.SetActive(true);
            } else if (SelectedOptionButtonS == 1 && Sound == true)
            {
                SoundTXT.text = string.Format(format, "OFF");
                SoundSelectedTXT.text = string.Format(format, "OFF");
                Sound = false;
            } else if (SelectedOptionButtonS == 1 && Sound == false)
            {
                SoundTXT.text = string.Format(format, "ON");
                SoundSelectedTXT.text = string.Format(format, "ON");
                Sound = true;
            }
        }
        if (Input.GetButtonDown("Start"))
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
                } else
                {
                    option = true;
                    manager.CharacterSelection.SetActive(false);
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
        BRefresh = true;
    }
}