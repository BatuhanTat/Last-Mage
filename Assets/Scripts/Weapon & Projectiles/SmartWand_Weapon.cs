using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SmartWand_Weapon : Weapon
{
    [SerializeField] int numProjectiles = 2;


    List<GameObject> TargetEnemies = new List<GameObject>();
    // DetectEnemies_InRadius detectEnemies_Script;

    public override void Fire()
    {
        GameObject tempProjectile;

        TargetEnemies = DetectEnemies_InRadius.GetEnemyList_InRadius().
                                    OrderBy(x => Vector2.Distance(x.transform.position, transform.position)).
                                    ToList();
        // Debug.Log("Smart wand weapon " + TargetEnemies);

        for (int i = 0; i < TargetEnemies.Count; ++i)
        {
            if (TargetEnemies[i] != null && i < numProjectiles)
            {
                tempProjectile = GameObjectInstantiation();
                tempProjectile.GetComponent<ProjectileBehaviour>().HomingToTarget(TargetEnemies[i]);
            }
        }
        TargetEnemies.Clear();
    }

    GameObject GameObjectInstantiation()
    {
        GameObject projectile = ObjectPool.SharedInstance.GetPooledObject("Smart Wand Projectile");
        if (projectile != null)
        {
            projectile.transform.position = player.transform.position;
            projectile.transform.rotation = Quaternion.identity;
            projectile.SetActive(true);
            return projectile;
        }
        else
            Debug.Log("projectile is null");
        return null;

    }

    public override void IncreaseProjectile()
    {
        Debug.Log("Osurdum");
        numProjectiles++;
        //Debug.Log(this.gameObject.name);
    }
}
