using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuUI_Handler : MonoBehaviour
{
    [SerializeField] GameObject upgradesPanel;
    [Header("Upgrade texts")]
    [SerializeField] TextMeshProUGUI speedUpgradeText;
    [SerializeField] TextMeshProUGUI healthUpgradeText;
    [SerializeField] TextMeshProUGUI attackRateUpgradeText;

    [Space(2)]
    // Upgrade count texts. It shows how many times an upgrade purchased.
    [Header("Upgrade Counts")]
    [SerializeField] TextMeshProUGUI speedUpgradeCount;
    [SerializeField] TextMeshProUGUI healthUpgradeCount;
    [SerializeField] TextMeshProUGUI attackRateUpgradeCount;
    [SerializeField] TextMeshProUGUI availablePointsCount;

    GameManager.SaveDataClass permenantUpgrades;

    private void Start()
    {
        LoadUpgradeStats();
    }
    public void StartGame()
    {
        GameManager.instance.Set_IsGameActive(true);
        GameManager.instance.ResetEnemyKilled();
        GameManager.instance.ResetScore();
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void UpgradePanel()
    {
        upgradesPanel.SetActive(!upgradesPanel.activeSelf);
    }

    public void Speed_Upgrade()
    {
        if (GameManager.instance.playerUpgrades_Permanent.availablePoints > 0)
        {
            Debug.Log("Speed upgrade");
            GameManager.instance.playerUpgrades_Permanent.speed += 1;
            speedUpgradeText.SetText("Speed            :    +%" + 5 * GameManager.instance.playerUpgrades_Permanent.speed);
            speedUpgradeCount.SetText(GameManager.instance.playerUpgrades_Permanent.speed.ToString());

            GameManager.instance.playerUpgrades_Permanent.availablePoints--;
            availablePointsCount.SetText(GameManager.instance.playerUpgrades_Permanent.availablePoints.ToString());
            GameManager.instance.SaveData();
        }
    }

    public void Health_Upgrade()
    {
        if (GameManager.instance.playerUpgrades_Permanent.availablePoints > 0)
        {
            Debug.Log("Health upgrade");
            GameManager.instance.playerUpgrades_Permanent.health += 1;
            healthUpgradeText.SetText("Health            :    +%" + 10 * GameManager.instance.playerUpgrades_Permanent.health);
            healthUpgradeCount.SetText(GameManager.instance.playerUpgrades_Permanent.health.ToString());

            GameManager.instance.playerUpgrades_Permanent.availablePoints--;
            availablePointsCount.SetText(GameManager.instance.playerUpgrades_Permanent.availablePoints.ToString());
            GameManager.instance.SaveData();
        }
    }

    public void AttackRate_Upgrade()
    {
        if (GameManager.instance.playerUpgrades_Permanent.availablePoints > 0)
        {
            Debug.Log("Attack Rate upgrade");
            GameManager.instance.playerUpgrades_Permanent.attackRate += 1;
            attackRateUpgradeText.SetText("Attack Rate    :    +%" + 5 * GameManager.instance.playerUpgrades_Permanent.attackRate);
            attackRateUpgradeCount.SetText(GameManager.instance.playerUpgrades_Permanent.attackRate.ToString());

            GameManager.instance.playerUpgrades_Permanent.availablePoints--;
            availablePointsCount.SetText(GameManager.instance.playerUpgrades_Permanent.availablePoints.ToString());
            GameManager.instance.SaveData();
        }
    }

    public void ResetUpgrades()
    {
        int availablePoints = 0;
        permenantUpgrades = GameManager.instance.GetPermenant_Upgrades();

        availablePoints = permenantUpgrades.speed +
                          permenantUpgrades.health +
                          permenantUpgrades.attackRate + permenantUpgrades.availablePoints;


        availablePointsCount.SetText(availablePoints.ToString());

        permenantUpgrades.speed = 0;
        permenantUpgrades.health = 0;
        permenantUpgrades.attackRate = 0;
        permenantUpgrades.availablePoints = availablePoints;
        GameManager.instance.playerUpgrades_Permanent = permenantUpgrades;
        GameManager.instance.SaveData();
        LoadUpgradeStats();

    }

    private void LoadUpgradeStats()
    {
        permenantUpgrades = GameManager.instance.GetPermenant_Upgrades();

        speedUpgradeText.SetText("Speed            :    +%" + 5 * permenantUpgrades.speed);
        healthUpgradeText.SetText("Health            :    +%" + 10 * permenantUpgrades.health);
        attackRateUpgradeText.SetText("Attack Rate    :    +%" + 5 * permenantUpgrades.attackRate);

        speedUpgradeCount.SetText(permenantUpgrades.speed.ToString());
        healthUpgradeCount.SetText(permenantUpgrades.health.ToString());
        attackRateUpgradeCount.SetText(permenantUpgrades.attackRate.ToString());
        availablePointsCount.SetText(permenantUpgrades.availablePoints.ToString());
    }


}
