using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniWand_Weapon : Weapon
{
    [SerializeField] int numProjectiles = 1;

    float angle = 270;

    public override void Fire()
    {
        GameObject tempProjectile;


        for (int i = 0; i < numProjectiles; ++i)
        {
            tempProjectile = GameObjectInstantiation();
            if (numProjectiles > 1)
            {
                if (numProjectiles > 2)
                {
                    angle += 90;
                }
                else
                {
                    angle += 180;
                }
            }
        }
    }

    GameObject GameObjectInstantiation()
    {
        GameObject projectile = ObjectPool.SharedInstance.GetPooledObject("Mini Wand Projectile");
        if (projectile != null)
        {
            projectile.transform.position = player.transform.position;
            /*projectile.transform.rotation = Quaternion.Euler(0f, 0f, player.transform.localScale.x * angle
                                                       * (player.GetComponent<SpriteRenderer>().flipX ? -1 : 1)     ); */
            projectile.transform.rotation = Quaternion.Euler(0f, 0f, player.transform.localScale.z + angle *
                                                    (player.GetComponent<SpriteRenderer>().flipX ? -1 : 1));


            projectile.SetActive(true);
            return projectile;
        }
        else
            Debug.Log("projectile is null");
        return null;
    }

    public override void IncreaseProjectile()
    {
        // numProjectiles = numProjectiles < 2 ? numProjectiles++ : numProjectiles;
        numProjectiles += numProjectiles < 4 ? 1 : 0;
        //Debug.Log(this.gameObject.name);
    }
}
