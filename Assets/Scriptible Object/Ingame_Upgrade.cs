using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ingame Upgrades")]
public class Ingame_Upgrade : ScriptableObject
{
    [SerializeField] string upgradeName;
    [Multiline, TextArea]
    [SerializeField] string description;
    [SerializeField] float probability;
    [SerializeField] Sprite image;
    [SerializeField] bool isWeapon;
    [SerializeField] int max_UpgradeCount = 8;
    //[SerializeField] int current_UpgradeCount = 0;

    public string Name
    {
        get { return upgradeName; }
    }

    public string Description
    {
        get { return description; }
    }
    public float Probability
    {
        get { return probability; }
    }

    public Sprite Image
    {
        get { return image; }
    }

    public bool IsWeapon
    {
        get { return isWeapon; }
    }

  /*   public int CurrentUpgradeCount
    {
        get { return current_UpgradeCount; }
        set
        {
            if (value <= max_UpgradeCount)
            {
                current_UpgradeCount = value;
            }
        }
    } */

    public int MaxUpgradeCount
    {
        get { return max_UpgradeCount; }
    }
}
