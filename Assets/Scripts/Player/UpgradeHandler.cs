using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UpgradeHandler : MonoBehaviour
{
    [Header("WeaponsList")]
    [SerializeField] List<GameObject> weaponsList;

    [SerializeField] GameObject weaponHolder;


    PlayerController playerController;
    GameManager.SaveDataClass permenantUpgrades;

    // This list will contain all the taken upgrades including weapons. 
    // This will be used on InGameUI_Handler script to show the current weapons and upgrades taken 
    // in the Pause Panel
    [SerializeField] List<UpgradeClass> upgradesList;
    InGameUI_Handler inGameUI_HandlerScript;

    public class UpgradeClass
    {
        public Ingame_Upgrade upgrade;
        public int current_UpgradeCount;

        public UpgradeClass(Ingame_Upgrade up, int count)
        {
            upgrade = up;
            current_UpgradeCount = count;
        }
        public void ChangeCount(int value)
        {
            current_UpgradeCount = value;
        }
    }

    public List<UpgradeClass> GetUpgradeClassList()
    {
        return upgradesList;
    }

    private void Start()
    {
        playerController = this.transform.parent.gameObject.GetComponent<PlayerController>();
        upgradesList = new List<UpgradeClass>();
        GameObject.Find("Canvas").GetComponent<InGameUI_Handler>().SetStarterWeapon(); 
        CalculateStats();
    }

    public void TakeWeaponUpgrade(GameObject _weapon, Ingame_Upgrade upgrade)
    {
        // Add the new weapon to the weaponsList if it does not already added.
        if (!weaponsList.Any(go => go.name.StartsWith(_weapon.name)))
        {
            //Debug.Log("Weapon name" + _weapon.name);
            GameObject weapon = Instantiate(_weapon, weaponHolder.transform);
            weaponsList.Add(weapon);
            upgradesList.Add(new UpgradeClass(upgrade, 0));
        }
        // If new weapon already exists in the weaponsList instead of adding it again increase it's stats.
        else
        {
            foreach (Transform child in weaponHolder.transform)
            {
                MonoBehaviour[] scripts = child.gameObject.GetComponents<MonoBehaviour>();
                foreach (MonoBehaviour script in scripts)
                {
                    if (script is Weapon && script.gameObject.name.StartsWith(_weapon.name))
                    {
                        (script as Weapon).IncreaseProjectile();
                    }
                }
            }
        }
        FindAndIncrease_UpgradeCount(upgrade);
        upgradeToRemove = CheckUpgradeCount();
        //PrintUpgradeList(upgradesList);
    }


    public void TakeStatUpgrade(Ingame_Upgrade upgrade)
    {
        string _name = upgrade.name;
        if (_name.Contains("Speed"))
        {
            IncreaseSpeed(1);
        }
        else if (_name.Contains("Attack"))
        {
            IncreaseAttackRate(1);
        }
        else if (_name.Contains("Health"))
        {
            IncreaseCurrentHealth();
        }

        if (!upgradesList.Any(x => x.upgrade.Name == upgrade.Name))
        {
            upgradesList.Add(new UpgradeClass(upgrade, 0));
        }
        FindAndIncrease_UpgradeCount(upgrade);
        upgradeToRemove = CheckUpgradeCount();
        //PrintUpgradeList(upgradesList);
    }

    void FindAndIncrease_UpgradeCount(Ingame_Upgrade up)
    {
        for (int i = 0; i < upgradesList.Count; ++i)
        {
            if (upgradesList[i].upgrade.Name == up.Name)
            {
                upgradesList[i].ChangeCount(upgradesList[i].current_UpgradeCount + 1);
            }
        }
    }

    Ingame_Upgrade upgradeToRemove;
    Ingame_Upgrade CheckUpgradeCount()
    {
        foreach (UpgradeClass s in upgradesList)
        {
            if (s.current_UpgradeCount == s.upgrade.MaxUpgradeCount)
            {
                return s.upgrade;
            }
        }
        return null;
    }

    public Ingame_Upgrade RemoveUpgrade()
    {
        return upgradeToRemove;
    }

    void CalculateStats()
    {
        permenantUpgrades = GameManager.instance.GetPermenant_Upgrades();
        IncreaseSpeed(permenantUpgrades.speed);
        IncreaseMaxHealth(permenantUpgrades.health);
        IncreaseAttackRate(permenantUpgrades.attackRate);
    }

    void IncreaseSpeed(int multiplier)
    {
        // Each point incrases SPEED by %5 
        // Debug.Log("Speed Before: " + playerController.MovementSpeed);
        playerController.MovementSpeed += ((playerController.MovementSpeed * 5) / 100) * multiplier;
        // Debug.Log("Speed After: " + playerController.MovementSpeed);
    }

    void IncreaseMaxHealth(int multiplier)
    {
        // Each point incrases HEALTH by %5 
        // Debug.Log("Health Before: " + playerController.MaxHealth);
        playerController.MaxHealth += ((playerController.MaxHealth * 10) / 100) * multiplier;
        // Debug.Log("Health AFTER: " + playerController.MaxHealth);
    }

    void IncreaseCurrentHealth()
    {
        // Debug.Log("Health Before: " + playerController.CurrentHealth);
        playerController.CurrentHealth += ((playerController.MaxHealth * 10) / 100);
        // Debug.Log("Health AFTER: " + playerController.CurrentHealth);
    }

    void IncreaseAttackRate(int multiplier)
    {
        // Each point incrases ATTACK_RATE by %5 
        // Debug.Log("Attack Rate Before: " + playerController.AttackRate);
        playerController.AttackRate -= ((playerController.AttackRate * 5) / 100) * multiplier;
        // Debug.Log("Attack Rate After: " + playerController.AttackRate);
    }


    void PrintUpgradeList(List<UpgradeClass> list)
    {
        //Debug.Log("List Count: " + list.Count);
        foreach (UpgradeClass st in list)
        {
            Debug.Log("Name: " + st.upgrade.Name);
            Debug.Log("CurrentCount: " + st.current_UpgradeCount);
            Debug.Log("MaxCount: " + st.upgrade.MaxUpgradeCount);
        }
    }


    public int GetUpgradeCount(string name)
    {
        foreach (UpgradeClass upclass in upgradesList)
        {
            if (upclass.upgrade.Name == name)
            {
                return upclass.current_UpgradeCount;
            }
        }
        throw new System.Exception("Upgrade count of did not managed to returned.");
    }

    public List<UpgradeClass> GetUpgradesList()
    {
        return upgradesList;
    }
}
