using UnityEngine;
using System.Collections.Generic;

public class SkillTreePanel : MonoBehaviour
{
    ClassSkillTree skillTree;

    [SerializeField] List<SkillTreeButton> skillTreeButtons;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ClassSkillManager skillManager = PlayerController.instance.SkillManager();
        skillTree = PlayerController.instance.SkillManager().GetSkillTree();
        HideSkillTree();
        EventsManager.instance.onSkillPointSpent.AddListener(UpdateSkillTree);
    }
    private void OnDestroy()
    {
        EventsManager.instance.onSkillPointSpent.RemoveListener(UpdateSkillTree);
    }


    // Update is called once per frame
    void OnEnable()
    {
        if (skillTree != null) UpdateSkillTree();
        else
        {
            if (PlayerController.instance != null)
            {
                skillTree = PlayerController.instance.SkillManager().GetSkillTree();
                UpdateSkillTree();
            }
        }
    }

    void UpdateSkillTree()
    {
        Debug.Log("Updating skill tree");
        int i = 0;
        foreach(SkillTreeButton button in skillTreeButtons)
        {
            if (button != null)
            {
                if (i < skillTree.list.Count && skillTree.list[i] != null)
                {
                    button.gameObject.SetActive(true);
                    button.UpdateButton(skillTree.list[i]);
                }
                else
                {
                    button.gameObject.SetActive(false);
                }
            }
            i++;
            if (i >= skillTreeButtons.Count)
            {
                Debug.LogWarning("More skills than button slots in the active Class Skill Manager");
                i--;
            }
        }
    }

    public void HideSkillTree()
    {
        if (UIManager.instance != null) UIManager.instance.HideSkillTree();
    }
}
