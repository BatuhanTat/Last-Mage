using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Weapon : MonoBehaviour
{
    [Header("Projectile stats")]
    [SerializeField] float projectileDamage = 1.0f;
    [SerializeField] float firingRate = 1.0f;

    [Space(2)]
    [SerializeField] GameObject projectilePrefab;

    
    [HideInInspector] public GameObject player;

    private void Start()
    {
        InvokeRepeating("Fire", 0.3f, firingRate);
        player = GameObject.FindWithTag("Player");
        Set_ProjectileDamage();
    }

    public void Set_ProjectileDamage()
    {
        GameObject projectile = projectilePrefab;
        projectile.GetComponent<ProjectileBehaviour>().damage = projectileDamage;
    }

    public abstract void Fire();

    public abstract void IncreaseProjectile();
    

}
