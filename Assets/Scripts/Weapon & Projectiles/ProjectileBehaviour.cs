using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [Header("Projectile stats")]
    [SerializeField] float speed = 15.0f;
    [SerializeField] float lifeTime = 5.0f;
    [SerializeField] bool is_smartProjectile;

    [HideInInspector]
    public float damage;

    GameObject target;
    Vector3 moveDirection;


    // Update is called once per frame
    void Update()
    {
        if (is_smartProjectile)
            FollowEnemy();
        else
            Move();
    }

    void FollowEnemy()
    {
        if (target != null)
        {
            if (is_smartProjectile && target.activeSelf == true)
            {
                moveDirection = (target.transform.position - transform.position).normalized;
                transform.position += moveDirection * speed * Time.deltaTime;
                transform.up = target.transform.position - transform.position;
            }
            else if (target.activeSelf == false)
            {
                //Debug.Log("Zortladim");
                transform.position += moveDirection * speed * Time.deltaTime;
            }
        }
    }

    void Move()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    public void HomingToTarget(GameObject newTarget)
    {
        target = newTarget;
        // moveDirection = (target.transform.position - transform.position).normalized;
        StartDelay();
    }

    /*   void OnCollisionEnter2D(Collision2D other)
      {
          if (target != null)
          {
              // checking if the collided object has is our target by comparing tags, 
              // in our case target tag is "Enemy0,1,2,3,4...".
              //if (other.gameObject.CompareTag(target.tag))
              //{
              //Destroy(other.gameObject, 0.5f);
              //other.gameObject.SetActive(false);
              other.gameObject.GetComponent<Enemy>().DecreaseHealth(projectileDamage);
              //Destroy(gameObject);
              gameObject.SetActive(false);
              //}
          }
      } */


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().DecreaseHealth(damage);
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Boundary"))
        {
            gameObject.SetActive(false);
        }

    }

    public void StartDelay()
    {
        StartCoroutine(SetDeactiveDelay());
    }
    // Lifespan of the projectile
    IEnumerator SetDeactiveDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }
}
