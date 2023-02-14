using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [Header("Enemy stats")]
    [SerializeField] float movementSpeed;
    [SerializeField] float maxHealth;
    [SerializeField] int score;

    float currentHealth;

    Rigidbody2D myRigidbody;
    GameObject player;
    SpriteFlash flashEffect;
    SpriteRenderer spriteRenderer;
    [SerializeField] BoxCollider2D myCollider;
    [SerializeField] Material originalMaterial;

    [Space(2)]
    [SerializeField] Animation_Handler animation_Handler;

    private Coroutine waitFlashRoutine;
    private bool isDying = false;

    private bool inContact = false;

    void Start()
    {
        currentHealth = maxHealth;
        myRigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        flashEffect = GetComponent<SpriteFlash>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (GameManager.instance.isGameActive && !isDying && !inContact)
        {
            MoveTowardsPlayer();
            //MoveTowardsPlayer_AddForce();
        }
        else
            myRigidbody.velocity = new Vector2(0.0f, 0.0f);
    }

  /*   void MoveTowardsPlayer_AddForce()
    {
        Vector2 lookDirection = (player.transform.position - transform.position).normalized;
        // Limit the maximum velocity by checking velocity maginute.
        if (myRigidbody.velocity.magnitude < movementSpeed)
        {
            myRigidbody.AddForce(lookDirection * 50 * Time.deltaTime);
        }

        bool enemyHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (enemyHasHorizontalSpeed)
        {
            animation_Handler.Animation_2_Run();
            FlipSprite();
        }
        else
        {
            animation_Handler.Animation_1_Idle();
        }
    } */

    void MoveTowardsPlayer()
    {
        Vector2 lookDirection = (player.transform.position - transform.position).normalized;

        // Below approach is not recommended. Directly manipulating the velocity of the rigidbody will cause unrealistic 
        // actions, plus setting the velocity every physic update is meaningless and costly.

        // Multiplying "lookDirection" with "movementSpeed" to get the enemy velocity;
        myRigidbody.velocity = lookDirection * movementSpeed;

        bool enemyHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (enemyHasHorizontalSpeed)
        {
            animation_Handler.Animation_2_Run();
            FlipSprite();
        }
        else
        {
            animation_Handler.Animation_1_Idle();
        }

        // Instead use the AddForce method. 
        // Limit the maximum velocity by checking velocity maginute.
        /*   if (enemyRigidbody.velocity.magnitude < movementSpeed)
          {
              enemyRigidbody.AddForce(lookDirection * 50 * Time.deltaTime);
          } */
    }

    /*     private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Pushback");
                PushBack();
            }
        } */

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Projectile") && gameObject.activeSelf)
        {
            flashEffect.Flash();
        }
    }


    /*  private void PushBack()
     {
         Vector2 pushbackDirection = (transform.position - player.transform.position).normalized;
         enemyRigidbody.AddForce(pushbackDirection * 2, ForceMode2D.Impulse);
     } */

    public void DecreaseHealth(float _projectileDamage)
    {
        animation_Handler.Animation_3_Hit();
        //Debug.Log("damage : " + _projectileDamage);
        currentHealth -= _projectileDamage;
        if (currentHealth <= 0)
        {
            isDying = true;
            myRigidbody.velocity = new Vector2(0.0f, 0.0f);
            animation_Handler.Animation_4_Death();
            GameManager.instance.enemyKilled = 1;
            GameManager.instance.score = score;
            myCollider.enabled = false;
            // If the waitFlashRoutine is not null, then it is currently running.
            if (waitFlashRoutine != null)
            {
                // In this case, we should stop it first.
                // Multiple FlashRoutines the same time would cause bugs.
                StopCoroutine(waitFlashRoutine);
            }
            waitFlashRoutine = StartCoroutine(WaitFlash());
        }
    }

    public void ReactivateEnemy()
    {
        currentHealth = maxHealth;
        if (!myCollider.enabled)
        {
            myCollider.enabled = true;
        }
        gameObject.SetActive(true);
    }

    void FlipSprite()
    {
        if (Mathf.Sign(myRigidbody.velocity.x) < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    public void DeactivateEnemy()
    {
        flashEffect.SetDefault_Material();
        if (waitFlashRoutine != null)
        {
            StopCoroutine(waitFlashRoutine);
        }
        gameObject.SetActive(false);
    }

    private IEnumerator WaitFlash()
    {
        yield return new WaitUntil(() => flashEffect.flashEnded);
        waitFlashRoutine = null;
    }
}
