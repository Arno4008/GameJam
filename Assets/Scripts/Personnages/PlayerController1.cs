using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class PlayerController1 : MonoBehaviour
{
    public Slider sliderUlti;
    public PlayerController2 playerController;
    public GameObject[] animator;
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
    private bool Attack;
    public bool option;
    public float compt;
    private void Start()
    {
        StartCoroutine(PlayAnimation(animator[1], 1f));
        SetMaxValueUlti(UltiXp_Need, UltiXp_Current);
        SetMaxValueHP(health, health);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Attack = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Attack = false;
        }
    }
    void Update()
    {
        float moveVertical;
        SetValueHP(health);
        SetValueUlti(UltiXp_Current);
        float moveHorizontal = Input.GetAxis("Horizontal1");
        if (!manager.Walls)
        {
            moveVertical = Input.GetAxis("Vertical1");
        } else
        {
            moveVertical = 0.0f;
        }
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if (option == false)
        {
            transform.Translate(movement * speed * Time.deltaTime);
            if (moveHorizontal > 0.0f || moveHorizontal < 0.0f)
            {
                StartCoroutine(PlayAnimation(animator[2], 1f));
            }
        }
        if (manager.inFight)
        {
            AttackTimer += Time.deltaTime;
        }
        if (Input.GetButtonDown("Fire1." + joystickNumber) && AttackTimer >= AttackCooldown && Attack == true && option == false)
        {
            playerController.health -= damage;
            UltiXp_Current += 5;
        }
        if (Input.GetButtonDown("Fire3." + joystickNumber) && option == false)
        {
            StartCoroutine(PlayAnimation(animator[1], 1f));
            UltiXp_Current += 10;
        }
        if (Input.GetButtonDown("LB" + joystickNumber) && UltiXp_Current >= UltiXp_Need && option == false)
        {
            playerController.health -= damage * 2;
            UltiXp_Current = 0;
        }
        if (health <= 0)
        {
            manager.End(1);
            Destroy(gameObject, 0.1f);
        }
    }
    IEnumerator PlayAnimation(GameObject animation, float delay)
    {
        animator[0].SetActive(false);
        animation.SetActive(true);
        yield return new WaitForSeconds(delay);
        animation.SetActive(false);
        animator[0].SetActive(true);
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