using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class CharacterSelection1 : MonoBehaviour
{
    public GameObject[] characters;
    public GameObject[] Slots;
    public int selectedCharacter = 0;
    public int SelectedSlots = 0;
    public int joystickNumber;
    public GameManager manager;
    public bool isAxisInUse;
    public bool BRefresh;
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal1");
        if (horizontalInput < 0)
        {
            if (isAxisInUse == false)
            {
                selectedCharacter--;
                SelectedSlots--;
                if (selectedCharacter < 0)
                {
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
                if (selectedCharacter == characters.Length)
                {
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
                BRefresh = false;
            }
        }
        if (Input.GetButtonDown("Fire1." + joystickNumber))
        {
            manager.StartFight();
        }
    }
    void Refresh()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].SetActive(false);
        }
        BRefresh = true;
    }
}