using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject playerHUD;
    [SerializeField] GameObject characterStatsPanel;
    [SerializeField] GameObject notificationPanel;
    [SerializeField] TextMeshProUGUI notificationText;

    [SerializeField] GameObject skillTree;

    public static UIManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        HideCharacterStatsPanel();
    }

    public void HideAll()
    {
        // Hide everything
        HidePlayerHUD();
        HideCharacterStatsPanel();
        HideSkillTree();
    }

    #region PlayerHUD
    public void ShowPlayerHUD()
    {
        playerHUD.SetActive(true);
    }
    public void HidePlayerHUD()
    {
        playerHUD.SetActive(false);
    }

    public void ShowNotificationPanel()
    {
        notificationPanel.SetActive(true);
    }

    public void HideNotificationPanel()
    {
        notificationPanel.SetActive(false);
    }

    public void SetMessage(string message)
    {
        notificationText.text = message;   
    }

    public System.Collections.IEnumerator RunNotificationText(string message, float timer)
    {
        Debug.Log($"Running notification coroutine with message {message}");
        SetMessage(message);
        ShowNotificationPanel();
        yield return new WaitForSeconds(timer);
        HideNotificationPanel();
    }

    #endregion

    #region Character Stat Panel
    public void ShowCharacterStatsPanel()
    {
        characterStatsPanel.SetActive(true);
    }
    public void HideCharacterStatsPanel()
    {
        characterStatsPanel.SetActive(false);
    }
    public void ToggleCharacterStatsPanel()
    {
        if (characterStatsPanel.activeInHierarchy) HideCharacterStatsPanel();
        else ShowCharacterStatsPanel();
    }


    public void ShowSkillTree()
    {
        skillTree.SetActive(true);
    }
    public void HideSkillTree()
    {
          
        skillTree.SetActive(false);
    }
    public void ToggleSkillTree()
    {
        if (skillTree.activeInHierarchy) HideSkillTree();
        else ShowSkillTree();
    }

    #endregion
}
