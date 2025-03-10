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

    // This is a recursive coroutine that will display each message in the array for the specified time
    // the start should likely always be a the beginning of the array so 0, but who am I to tell you how to live your life
    // the args often should look like and array of messages to be called in order then the 0 index and then how long you want each message to display
    public System.Collections.IEnumerator RunMultiNotificationText(string[] message, int currIndex, float timePerMessage)
    {
        if (currIndex >= message.Length)
        {
            HideNotificationPanel();
            yield break;
        }
        //Debug.Log($"Running notification coroutine with message {message} and index {currIndex}");
        SetMessage(message[currIndex]);
        ShowNotificationPanel();
        yield return new WaitForSeconds(timePerMessage);
        HideNotificationPanel();
        StartCoroutine(RunMultiNotificationText(message, currIndex + 1, timePerMessage));
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
