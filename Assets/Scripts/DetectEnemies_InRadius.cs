using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemies_InRadius : MonoBehaviour
{
    public static List<GameObject> enemyList_InRadius = new List<GameObject>();
    public static List<GameObject> GetEnemyList_InRadius()
    {
        return enemyList_InRadius;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            enemyList_InRadius.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            enemyList_InRadius.Remove(other.gameObject);
        }
    }

}
