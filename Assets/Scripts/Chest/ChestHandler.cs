using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestHandler : MonoBehaviour
{
    /*     [Header("WeaponsList")]
        [SerializeField] List<GameObject> weaponsList; */


    [Header("In-game Upgrades List")]
    [Tooltip("All Upgrades")]
    [SerializeField] List<Ingame_Upgrade> upgradesList;
    [Space(2)]

    [SerializeField] Image upgradeImage;
    [SerializeField] TextMeshProUGUI upgradeName;
    [SerializeField] TextMeshProUGUI upgradeDescription;

    [SerializeField] List<GameObject> weaponPrefabs;

    [Space(2)]
    [Header("Defaults")]
    [SerializeField] Ingame_Upgrade defaultUpgrade;
    [SerializeField] Ingame_Upgrade starterWeapon;
    [SerializeField] GameObject starterWeaponPrefab;


    // Probability of the upgrades.
    List<float> weightsList = new List<float>();

    Ingame_Upgrade selectedUpgrade;
    int selectedUpgradeIndex;

    private void Start()
    {
        NormalizeWeights();
    }

    Ingame_Upgrade SelectUpgrade()
    {
        float randomValue = Random.Range(0f, 1f);
        float accumulatedProbability = 0;
        for (int i = 0; i < upgradesList.Count; i++)
        {
            accumulatedProbability += weightsList[i];
            if (randomValue < accumulatedProbability)
            {
                //Debug.Log("Selected item is: " + upgradesList[i].Name);
                selectedUpgradeIndex = i;
                return upgradesList[i];
            }
        }

        return defaultUpgrade;
        //throw new System.Exception("SelectUpgrade did not manage to return a item.");
    }

    // This function will remove an Ingame_Upgrade from the current upgradesList if the Ingame_Upgrade's 
    // current_UpgradeCount is equal to the max_UpgradeCount.
    public void RemoveFromUpradeList(Ingame_Upgrade upgrade)
    {
        // Check if there is an upgrade that satisfies the condition explained in the upper comment.
        /*  foreach (Ingame_Upgrade up in upgradesList)
         {
             if (up.CurrentUpgradeCount == up.MaxUpgradeCount)
             {
                 upgradesList.Remove(up);
             }
         } */
        if (upgrade != null)
        {
            for (int i = 0; i < upgradesList.Count; i++)
            {
                Ingame_Upgrade up = upgradesList[i];
                if (up.Name == upgrade.Name)
                {
                    upgradesList.RemoveAt(i);
                }
            }
        }
    }

    void NormalizeWeights()
    {
        // get the weights from "upgradesList" to "weightsList" list
        for (int i = 0; i < upgradesList.Count; i++)
        {
            //Debug.Log("normalizeweights: " + upgradesList[i].Probability);
            weightsList.Add(upgradesList[i].Probability);
        }

        float sum = 0.0f;
        foreach (float weight in weightsList)
        {
            sum += weight;
        }

        // Normalize the weights by dividing each weight by the sum of all the weights
        for (int i = 0; i < weightsList.Count; i++)
        {
            weightsList[i] /= sum;
        }

        // Check that the weights are normalized and sum up to 1.0
        sum = 0.0f;
        foreach (float weight in weightsList)
        {
            sum += weight;
            // Debug.Log("weight " + weight);
        }
        // Debug.Log("Sum of weights: " + sum);
    }

    public void SetChestUI()
    {
        selectedUpgrade = SelectUpgrade();

        upgradeImage.sprite = selectedUpgrade.Image;
        upgradeName.SetText(selectedUpgrade.Name);
        upgradeDescription.SetText(selectedUpgrade.Description);
    }

    public GameObject Get_WeaponPrefab()
    {
        if (selectedUpgrade != null && selectedUpgradeIndex < weaponPrefabs.Count)
        {
            return weaponPrefabs[selectedUpgradeIndex];
        }
        else
            return null;
    }

    public Ingame_Upgrade Get_SelectedUpgrade()
    {
        return selectedUpgrade;
    }

    public bool SelectedUpgrade_IsWeapon
    {
        get { return selectedUpgrade.IsWeapon; }
    }

    public Ingame_Upgrade GetStarter_WeaponIngame()
    {
        return starterWeapon;
    }
    public GameObject GetStarter_WeaponPrefab()
    {
        return starterWeaponPrefab;
    }
}
