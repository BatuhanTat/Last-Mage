using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    [Header("Player stats")]
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float attackRate;

    [Space]
    [SerializeField] HealthBar healthBar;

    [Space(2)]
    [SerializeField] Animation_Handler animation_Handler;

    InGameUI_Handler inGameUI_HandlerScript;

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    SpriteFlash flashEffect;
    SpriteRenderer spriteRenderer;

    float currentHealth;
    bool isAlive = true;


    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        myRigidbody = GetComponent<Rigidbody2D>();
        flashEffect = GetComponent<SpriteFlash>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        inGameUI_HandlerScript = GameObject.Find("Canvas").GetComponent<InGameUI_Handler>();
        //CalculateStats();
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
        if (moveInput != Vector2.zero)
        {
            animation_Handler.Animation_2_Run();
        }
        else
        {
            animation_Handler.Animation_1_Idle();
        }
        Run();
        FlipSprite();
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * movementSpeed, moveInput.y * movementSpeed);
        myRigidbody.velocity = playerVelocity;
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed && Time.timeScale != 0)
        {
            //transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
            if (Mathf.Sign(myRigidbody.velocity.x) < 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
    }

    void OnPause(InputValue value)
    {
        /*  if (Time.timeScale == 0)
         {
             Time.timeScale = 1;
             inGameUI_HandlerScript.PausePanel();
         }
         else
         {
             Time.timeScale = 0;
             inGameUI_HandlerScript.PausePanel();
         } */
        inGameUI_HandlerScript.PausePanel();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Upgrade"))
        {
            Destroy(other.gameObject);
            inGameUI_HandlerScript.ChestPanel();
        }
    }

    // Timer for decreasing health on continuous touch with an enemy. 
    float timer = 0.0f;
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            flashEffect.Flash();
            timer += Time.deltaTime;
            // Reduce health on per 0.2f.
            if (timer > 0.2f)
            {
                //Debug.Log("Inside timer");
                DecreaseHealth();
                timer = 0.0f;
            }
        }
    }

    public void DecreaseHealth()
    {
        currentHealth -= 1.0f;
        // Debug.Log("Player health: " + currentHealth);
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            //Game over UI 
            inGameUI_HandlerScript.GameOverPanel();
            Destroy(gameObject);
        }
    }

    public float MovementSpeed
    {
        get { return movementSpeed; }
        set { movementSpeed = value; }
    }

    public float MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public float AttackRate
    {
        get { return attackRate; }
        set { attackRate = value; }
    }

    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            if (value <= maxHealth)
            {
                currentHealth = value;
                healthBar.SetHealth(currentHealth);
            }
        }
    }
}
