using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillTreeButton : MonoBehaviour
{
    [SerializeField] Image buttonImage;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI levelText;

    ClassSkill attachedSkill;

    public void UpdateButton(ClassSkill skill)
    {
        
        attachedSkill = skill;
        if (attachedSkill == null)
        {
            Debug.Log("attached skill is null");
        }
        if (skill != null)
        {
            buttonImage.sprite = skill.GetIconSprite();
            nameText.text = skill.GetName();
            levelText.text = skill.GetSkillLevel().ToString();
        }
    }
        
     
    public void PurchaseUpgrade()
    {
        attachedSkill.LevelUp();
        EventsManager.instance.onSkillPointSpent.Invoke();
    }

    public void EquipAbility()
    {
        if (attachedSkill.GetSkillLevel() > 0 && attachedSkill.GetComponent<EquippableAbility>() != null)
            PlayerController.instance.SetAbility2(attachedSkill.GetComponent<EquippableAbility>());
    }
}
