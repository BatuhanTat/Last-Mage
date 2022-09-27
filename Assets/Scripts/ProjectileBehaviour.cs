using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{

    [SerializeField] float speed = 15.0f;
    //[SerializeField] float projectileDamage = 15.0f;
    [SerializeField] float lifeTime = 5.0f;

    GameObject target;
    bool homing;
    Vector3 moveDirection;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsEnemy();
    }

    void MoveTowardsEnemy()
    {
        if (homing && target.activeSelf == true)
        {
            moveDirection = (target.transform.position - transform.position).normalized;
            transform.position += moveDirection * speed * Time.deltaTime;
            transform.up = target.transform.position - transform.position;
        }
        else if (target.activeSelf == false)
        {
            transform.position += moveDirection * speed * Time.deltaTime;
        }
    }

    public void Fire(GameObject newTarget)
    {
        target = newTarget;
        homing = true;
        //Destroy(gameObject, lifeTime);
        StartCoroutine(SetDeactiveDelay());
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (target != null)
        {
            // checking if the collided object has is our target by comparing tags, 
            // in our case target tag is "Enemy0,1,2,3,4...".
            if (other.gameObject.CompareTag(target.tag))
            {
                //Destroy(other.gameObject, 0.5f);
                other.gameObject.SetActive(false);
                //Destroy(gameObject);
                gameObject.SetActive(false);
            }
        }
    }

    IEnumerator SetDeactiveDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }
}
