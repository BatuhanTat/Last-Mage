using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class InGameUI_Handler : MonoBehaviour
{
    [Header("Pause Panel")]
    [SerializeField] GameObject pausePanel;
    [SerializeField] List<GameObject> weaponSlots;
    [SerializeField] List<GameObject> statSlots;

    [Space(2)]
    [SerializeField] GameObject chestPanel;

    [Space(2)]
    [SerializeField] ChestHandler chestHandlerScript;
    [SerializeField] UpgradeHandler upgradeHandlerScript;

    [Space(2)]
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI enemyKilledText;



    bool inChestPanel = false;



    public void ChestPanel()
    {
        TimeChange();
        if (!inChestPanel)
        {
            chestHandlerScript.SetChestUI();
        }
        chestPanel.SetActive(!chestPanel.activeSelf);
        inChestPanel = !inChestPanel;
    }

    public void GameOverPanel()
    {
        TimeChange();
        GameManager.instance.Set_IsGameActive(false);
        scoreText.SetText("Score : " + GameManager.instance.score);
        enemyKilledText.SetText("Enemy Killed : " + GameManager.instance.enemyKilled);
        gameOverPanel.SetActive(true);
        pausePanel.SetActive(true);
    }

    public void LoadMainMenu()
    {
        //BC_MusicManager.instance.MainMenuMusic();
        MusicManager.instance.MainMenuMusic();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void SetStarterWeapon()
    {
        upgradeHandlerScript.TakeWeaponUpgrade(chestHandlerScript.GetStarter_WeaponPrefab(),
                                              chestHandlerScript.GetStarter_WeaponIngame());
        HandleWeaponUpgrade(chestHandlerScript.GetStarter_WeaponIngame());
    }

    public void TakeUpgrade()
    {
        if (chestHandlerScript.SelectedUpgrade_IsWeapon)
        {
            TakeWeaponUI();
            HandleWeaponUpgrade(chestHandlerScript.Get_SelectedUpgrade());
        }
        else
        {
            TakeStatUpgradeUI();
            HandleStatUpgrade(chestHandlerScript.Get_SelectedUpgrade());
        }

        chestHandlerScript.RemoveFromUpradeList(upgradeHandlerScript.RemoveUpgrade());
        ChestPanel();
    }

    void TakeWeaponUI()
    {
        upgradeHandlerScript.TakeWeaponUpgrade(chestHandlerScript.Get_WeaponPrefab(),
                                                                        chestHandlerScript.Get_SelectedUpgrade());
    }

    void TakeStatUpgradeUI()
    {
        upgradeHandlerScript.TakeStatUpgrade(chestHandlerScript.Get_SelectedUpgrade());
    }

    public void PausePanel()
    {
        if (!inChestPanel)
        {
            TimeChange();
            pausePanel.SetActive(!pausePanel.activeSelf);
        }
    }

    /*     public void AddUpgradetoSlots(Ingame_Upgrade up)
        {
            if (up.IsWeapon)
            {
                HandleWeaponUpgrade(up);
            }
            else
            {
                HandleStatUpgrade(up);
            }
        } */

    int weaponIndex = 0;
    void HandleWeaponUpgrade(Ingame_Upgrade up)
    {
        if (!weaponSlots.Any(obj => obj.GetComponentInChildren<TextMeshProUGUI>().text == up.Name))
        {
            if (weaponIndex < weaponSlots.Count)
            {
                /*    weaponSlots[statIndex].SetActive(true);
                   weaponSlots[statIndex].GetComponentInChildren<TextMeshProUGUI>().text = up.Name;
                   weaponSlots[weaponIndex].GetComponentInChildren<Image>().sprite = up.Image;
                   weaponSlots[weaponIndex].GetComponentInChildren<Slider>().value = 1;
                   weaponIndex++; */
                AddToSlot(up, weaponSlots, weaponIndex);
            }
        }
        else
        {
            UpdateSliderValue(up, weaponSlots);
        }
    }

    int statIndex = 0;
    void HandleStatUpgrade(Ingame_Upgrade up)
    {
        if (!statSlots.Any(obj => obj.GetComponentInChildren<TextMeshProUGUI>().text == up.Name))
        {
            Debug.Log("Osurdum");
            // If the selected upgrade is "Health Regen" upgrade it will not be added to the slot.
            if (statIndex < statSlots.Count && up.Name != "Regen Health")
            {
                AddToSlot(up, statSlots, statIndex);
            }
        }
        else
        {
            UpdateSliderValue(up, statSlots);
        }
    }

    void AddToSlot(Ingame_Upgrade up, List<GameObject> slotList, int slotIndex)
    {
        slotList[slotIndex].SetActive(true);
        slotList[statIndex].GetComponentInChildren<TextMeshProUGUI>().text = up.Name;
        slotList[statIndex].GetComponentInChildren<Image>().sprite = up.Image;
        slotList[statIndex].GetComponentInChildren<Slider>().value = 1;
        slotList[statIndex].GetComponentInChildren<Slider>().maxValue = up.MaxUpgradeCount;
        slotIndex++;
    }

    void UpdateSliderValue(Ingame_Upgrade up, List<GameObject> slotList)
    {
        foreach (GameObject obj in slotList)
        {
            if (obj.GetComponentInChildren<TextMeshProUGUI>().text == up.Name)
            {
                obj.GetComponentInChildren<Slider>().value = upgradeHandlerScript.GetUpgradeCount(up.Name);
            }
        }
    }



    void TimeChange()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

}
