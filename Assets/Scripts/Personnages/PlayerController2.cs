using UnityEngine.UI;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public Slider sliderUlti;
    public PlayerController1 playerController;
    public Slider sliderHP;
    public GameManager manager;
    public float speed = 1.0f;
    public int joystickNumber;
    public float AttackTimer;
    public float AttackCooldown;
    public int UltiXp_Current;
    public int UltiXp_Need;
    public int health;
    public int damage;
    private void Start()
    {
        SetMaxValueUlti(UltiXp_Need, UltiXp_Current);
        SetMaxValueHP(health, health);
    }
    void Update()
    {
        SetValueHP(health);
        SetValueUlti(UltiXp_Current);
        float moveHorizontal = Input.GetAxis("Horizontal2");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        transform.Translate(movement * speed * Time.deltaTime);
        if (manager.inFight)
        {
            AttackTimer += Time.deltaTime;
        }
        if (Input.GetButtonDown("Fire1." + joystickNumber) && AttackTimer >= AttackCooldown)
        {
            Debug.Log("Attack 2eme perso");
            playerController.health -= damage;
            UltiXp_Current += 5;
        }
        if (Input.GetButtonDown("LB" + joystickNumber) && UltiXp_Current >= UltiXp_Need)
        {
            Debug.Log("Ulti 2eme perso");
            playerController.health -= damage * 2;
            UltiXp_Current = 0;
        }
        if (health <= 0)
        {
            Destroy(gameObject, 0.1f);
        }
    }
    public void SetValueUlti(int value)
    {
        sliderUlti.value = value;
    }
    public void SetMaxValueUlti(int max, int min)
    {
        sliderUlti.maxValue = max;
        sliderUlti.value = min;
    }
    public void SetValueHP(float value)
    {
        sliderHP.value = value;
    }
    public void SetMaxValueHP(float max, float min)
    {
        sliderHP.maxValue = max;
        sliderHP.value = min;
    }
}