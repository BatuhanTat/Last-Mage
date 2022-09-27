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
        if (other.tag == "Enemy0" || 
            other.tag == "Enemy1" || 
            other.tag == "Enemy2" || 
            other.tag == "Enemy3" || 
            other.tag == "Enemy4")
        {
            enemyList_InRadius.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy0" || 
            other.tag == "Enemy1" || 
            other.tag == "Enemy2" || 
            other.tag == "Enemy3" || 
            other.tag == "Enemy4")
        {
            enemyList_InRadius.Remove(other.gameObject);
        }
    }

}
