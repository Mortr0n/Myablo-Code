using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] GameObject statLevelUpButton;
    [SerializeField] GameObject skillLevelUpButton;


    private void Start()
    {
        EventsManager.instance.onPlayerLeveledUp.AddListener(ShowStatLevelUpButton);

        EventsManager.instance.onPlayerLeveledUp.AddListener(ShowSkillLevelUpButton);

        HideStatLevelUpButton();
        HideSkillLevelUpButton();

    }
    private void OnDestroy()
    {
        EventsManager.instance.onPlayerLeveledUp.RemoveListener(ShowStatLevelUpButton);
        EventsManager.instance.onPlayerLeveledUp.RemoveListener(ShowSkillLevelUpButton);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) ToggleCharacterStatPanel();
        if (Input.GetKeyDown(KeyCode.T)) ToggleSkillTree();
    }

    public void ToggleCharacterStatPanel()
    {
        UIManager.instance.ToggleCharacterStatsPanel();
        HideStatLevelUpButton();
    }

    public void ToggleSkillTree()
    {
        UIManager.instance.ToggleSkillTree();
        HideSkillLevelUpButton();
    }

    #region StatLevelUpButton
    public void HideStatLevelUpButton()
    {
        statLevelUpButton.SetActive(false);
    }
    public void ShowStatLevelUpButton()
    {
        statLevelUpButton.SetActive(true);
    }
    #endregion


    
    #region Skill Level Up Button
    public void HideSkillLevelUpButton()
    {
        skillLevelUpButton.SetActive(false);
    }
    public void ShowSkillLevelUpButton()
    {
        skillLevelUpButton.SetActive(true);
    }
    #endregion
}
