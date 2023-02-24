using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FireWand_Weapon : Weapon
{
    [SerializeField] int numProjectiles = 3;
    public float circleAngle = 180f;

    List<GameObject> enemyList;
    GameObject closestEnemy;
    Vector3 position;

    [SerializeField] bool canShootAlways = false;

    public override void Fire()
    {
        GameObject tempProjectile;
        if (!canShootAlways)
        {
            enemyList = DetectEnemies_InRadius.GetEnemyList_InRadius();

            closestEnemy = (enemyList != null && enemyList.Count > 0) ? DetectEnemies_InRadius.GetEnemyList_InRadius().
                                   OrderBy(x => Vector2.Distance(x.transform.position, transform.position)).
                                   First() : null;
            if (closestEnemy != null)
            {
                position = closestEnemy.transform.position;
            }
        }

        if (closestEnemy != null || canShootAlways)
        {
            // Calculate the starting angle based on the circle angle
            // "angle" is the leftmost, counter-clockwise projectile, "angleIncrement" is added to it every iteration.
            float angle = Vector2.SignedAngle(transform.up,
                                             position - player.transform.position);
            float angleIncrement;
            if (numProjectiles <= 9)
            {
                angleIncrement = circleAngle / (numProjectiles - 1);
            }
            else
            {
                angleIncrement = 22.5f;
            }
            angle -= circleAngle / 2;

            for (int i = 0; i < numProjectiles; ++i)
            {
                Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
                tempProjectile = GameObjectInstantiation(rotation);
                tempProjectile.GetComponent<ProjectileBehaviour>().StartDelay();

                // Increment the angle for the next bullet
                angle += angleIncrement;
            }
            closestEnemy = null;
        }
    }


    GameObject GameObjectInstantiation(Quaternion rt)
    {
        GameObject projectile = ObjectPool.SharedInstance.GetPooledObject("Fire Spell Projectile");
        if (projectile != null)
        {
            projectile.transform.position = player.transform.position;
            projectile.transform.rotation = rt;

            projectile.SetActive(true);
            return projectile;
        }
        else
            Debug.Log("projectile is null");
        return null;
    }

    public override void IncreaseProjectile()
    {
        if (numProjectiles < 16)
        {
            numProjectiles++;
            if (numProjectiles > 9)
            {
                circleAngle += 22.5f;
            }
            Debug.Log(this.gameObject.name);
        }
        if (numProjectiles == 16)
        {
            canShootAlways = true;
        }
    }
}
