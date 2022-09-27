using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float movementSpeed;


    Rigidbody2D enemyRigidbody;
    GameObject player;

    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        MoveTowardsPlayer();
    }
    
    void MoveTowardsPlayer()
    {
        Vector2 lookDirection = (player.transform.position - transform.position).normalized;
        // Multiplying "lookDirection" with "movementSpeed" to find the enemy velocity;
        enemyRigidbody.velocity = lookDirection * movementSpeed;
    }
}
