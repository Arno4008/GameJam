using UnityEngine;

public class CharacterSelection2 : MonoBehaviour
{
    public GameObject[] characters;
    public GameObject[] Slots;
    public GameObject[] charactersBack;
    public int selectedCharacter = 0;
    public int selectedCharacterBack = 0;
    public int SelectedSlots = 0;
    public int joystickNumber;
    public bool SelectedCharacter;
    public GameManager manager;
    public bool isAxisInUse;
    public bool BRefresh;
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal2");
        if (horizontalInput < 0)
        {
            if (isAxisInUse == false)
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
        }
        else if (horizontalInput > 0)
        {
            if (isAxisInUse == false)
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
        }
        if (horizontalInput == 0)
        {
            isAxisInUse = false;
        }
        if (!BRefresh)
        {
            Refresh();
        }
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
        if (Input.GetButtonDown("Fire1." + joystickNumber))
        {
            if (SelectedCharacter == false)
            {
                SelectedCharacter = true;
                manager.StartFight(selectedCharacter, 1, 1);
            }
            
        }
    }
    void Refresh() 
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].SetActive(false);
            charactersBack[i].SetActive(false);
        }
        BRefresh = true;
    }
}