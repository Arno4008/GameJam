using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class PlayerController2 : MonoBehaviour
{
    public Slider sliderUlti;
    public PlayerController1[] playerController;
    public Slider sliderHP;
    public GameObject[] animator;
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
    public bool move;
    public bool anim;
    private void Start()
    {
        animator[0].SetActive(true);
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
        float moveHorizontal = Input.GetAxis("Horizontal2");
        if (!manager.Walls)
        {
            moveVertical = Input.GetAxis("Vertical2");
        } else
        {
            moveVertical = 0.0f;
        }
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if (option == false)
        {
            transform.Translate(movement * speed * Time.deltaTime);
            if (moveHorizontal > 0.0f || moveHorizontal < 0.0f && anim == false)
            {
                move = true;
                StartCoroutine(PlayAnimation(animator[2], 1f));
            }
        }
        if (manager.inFight)
        {
            AttackTimer += Time.deltaTime;
        }
        if (Input.GetButtonDown("Fire1." + joystickNumber) && AttackTimer >= AttackCooldown && Attack == true && option == false)
        {
            StartCoroutine(PlayAnimation(animator[3], 1f));
            foreach (var player in playerController)
            {
                player.health -= damage;
            }
            UltiXp_Current += 5;
        }
        if (Input.GetButtonDown("Fire3." + joystickNumber) && option == false && move == false && anim == false)
        {
            StartCoroutine(PlayAnimation(animator[1], 1f));
            UltiXp_Current += 10;
        }
        if (Input.GetButtonDown("LB" + joystickNumber) && UltiXp_Current >= UltiXp_Need && option == false && move == false && anim == false)
        {
            foreach (var player in playerController)
            {
                player.health -= damage * 2;
            }
            UltiXp_Current = 0;
        }
        if (health <= 0)
        {
            manager.End(2);
            Destroy(gameObject, 0.1f);
        }
    }
    IEnumerator PlayAnimation(GameObject animation, float delay)
    {
        anim = true;
        animator[0].SetActive(false);
        animation.SetActive(true);
        yield return new WaitForSeconds(delay);
        animation.SetActive(false);
        animator[0].SetActive(true);
        anim = false;
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