using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] GameObject projectilePrefab;

    //DetectEnemies_InRadius detectEnemies_InRadius;
    Vector2 moveInput;
    Rigidbody2D myRigidbody;

    bool isAlive = true;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        //detectEnemies_InRadius = GetComponent<DetectEnemies_InRadius>()
        InvokeRepeating("Attack", 0.3f, 1.0f);
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
        Run();
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * movementSpeed, moveInput.y * movementSpeed);
        myRigidbody.velocity = playerVelocity;
    }


    void Attack()
    {
        LaunchProjectile();
    }

    void LaunchProjectile()
    {
        GameObject tempProjectile;
        foreach (var enemy in DetectEnemies_InRadius.GetEnemyList_InRadius())
        {
            if (enemy != null)
            {
                //tempProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

                tempProjectile = GameObjectInstantiation();
                tempProjectile.GetComponent<ProjectileBehaviour>().Fire(enemy);
            }
        }
    }

    GameObject GameObjectInstantiation()
    {
        GameObject projectile = ObjectPool.SharedInstance.GetPooledObject("Projectile");
        if (projectile != null)
        {
            projectile.transform.position = transform.position;
            projectile.transform.rotation = Quaternion.identity;
            projectile.SetActive(true);
            return projectile;
        }
        return null;
    }
}
