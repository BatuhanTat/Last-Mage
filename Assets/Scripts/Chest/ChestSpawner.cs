using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{

    [SerializeField] GameObject chestPrefab;
    [Space(2)]
    [SerializeField] float lowerTimeInterval = 2f;
    [SerializeField] float upperTimeInterval = 4f;
    [SerializeField] Vector2 boundary = new Vector2(18, 8);

    [Space(2)]
    [SerializeField] int maxChestsAllowed = 4;
    [SerializeField] GameObject chestContainer;
    [SerializeField] float chestRadius = 2.0f;



    private void Start()
    {
        InvokeRepeating("InstantiateChest", Random.Range(lowerTimeInterval, upperTimeInterval),
                                            Random.Range(lowerTimeInterval, upperTimeInterval));
    }

    private void Update()
    {
        if (!IsInvoking("InstantiateChest") && CheckChestCount() <= maxChestsAllowed)
        {
            InvokeRepeating("InstantiateChest", Random.Range(lowerTimeInterval, upperTimeInterval),
                                            Random.Range(lowerTimeInterval, upperTimeInterval));
        }
    }

    void InstantiateChest()
    {
        if (CheckChestCount() <= maxChestsAllowed)
        {
            Vector2 pos = GetRandomPosition();
            if (pos != Vector2.zero)
            {
                Instantiate(chestPrefab, pos, Quaternion.identity, chestContainer.transform);
            }
        }
        else
        {
            CancelInvoke("InstantiateChest");
        }
    }


    Vector2 GetRandomPosition()
    {
        Vector2 randomPosition = new Vector2(Random.Range(-boundary.x, boundary.x),
                                                 Random.Range(-boundary.y, boundary.y));

        int combinedlayerMask = LayerMask.GetMask("Player") | LayerMask.GetMask("Upgrade");

        int randomPositionTry_Count = 0;
        while (Physics2D.OverlapCircle(randomPosition, chestRadius, combinedlayerMask))
        {
            if (randomPositionTry_Count <= 20)
            {
                randomPosition = new Vector2(Random.Range(-boundary.x, boundary.x),
                                            Random.Range(-boundary.y, boundary.y));
                randomPositionTry_Count++;
            }
            else { return Vector2.zero; }
        }
        return randomPosition;
    }

    int CheckChestCount()
    {
        int chestCounter = 0;
        GameObject[] chests = GameObject.FindGameObjectsWithTag("Upgrade");
        chestCounter = chests.Length;
        return chestCounter;
    }

}
