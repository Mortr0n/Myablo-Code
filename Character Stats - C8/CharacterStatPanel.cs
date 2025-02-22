using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class CharacterStatPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI strengthText;
    [SerializeField] TextMeshProUGUI dexterityText;
    [SerializeField] TextMeshProUGUI vitalityText;
    [SerializeField] TextMeshProUGUI energyText;

    [SerializeField] List<GameObject> spendButtons = new List<GameObject>();

    #region Listener Subscription
    private void Start()
    {
        EventsManager.instance.onStatPointSpent.AddListener(UpdateCharacterSheetPanel);
        EventsManager.instance.onPlayerLeveledUp.AddListener(UpdateCharacterSheetPanel);
        //HideCharacterStatPanel();
        
    }
    private void OnDestroy()
    {
        EventsManager.instance.onStatPointSpent.RemoveListener(UpdateCharacterSheetPanel);
        EventsManager.instance.onPlayerLeveledUp.RemoveListener(UpdateCharacterSheetPanel);
    }
    #endregion

    private void OnEnable()
    {
        if (PlayerCharacterSheet.instance != null)
        {
            UpdateCharacterSheetPanel();
        }
    }

    void UpdateCharacterSheetPanel()
    {
        
        if (PlayerCharacterSheet.instance.GetStatPointsToSpend() == 0) HideSpendButtons();
        else ShowSpendButtons();

        UpdateStatDisplayText();
    }
    void UpdateStatDisplayText()
    {
        int displayInt = 15;
        // Update Strength Display
        displayInt = (int)PlayerCharacterSheet.instance.GetStrength();
        strengthText.text = displayInt.ToString();
        // Update Dexterity Display
        displayInt = (int)PlayerCharacterSheet.instance.GetDexterity();
        dexterityText.text = displayInt.ToString();
        // Update Vitality Display
        displayInt = (int)PlayerCharacterSheet.instance.GetVitality();
        vitalityText.text = displayInt.ToString();
        // Update Energy Display
        displayInt = (int)PlayerCharacterSheet.instance.GetEnergy();
        energyText.text = displayInt.ToString();
    }
    #region Spend Button Display
    void HideSpendButtons()
    {
        foreach(GameObject b in spendButtons)
        {
            b.SetActive(false);
        }
    }
    void ShowSpendButtons()
    {
        foreach (GameObject b in spendButtons)
        {
            b.SetActive(true);
        }
    }
    #endregion

    #region Spend Functions
    public void BuyStrength()
    {
        PlayerCharacterSheet.instance.BuyStrengthPoint();
    }
    public void BuyDexterity()
    {
        PlayerCharacterSheet.instance.BuyDexterityPoint();
    }
    public void BuyVitality()
    {
        PlayerCharacterSheet.instance.BuyVitalityPoint();
    }
    public void BuyEnergy()
    {
        PlayerCharacterSheet.instance.BuyEnergyPoint();
    }
    #endregion

    public void HideCharacterStatPanel()
    {
        UIManager.instance.HideCharacterStatsPanel();
    }
}
