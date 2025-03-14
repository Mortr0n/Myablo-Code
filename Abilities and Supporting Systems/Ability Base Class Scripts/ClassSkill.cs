using UnityEngine;

public class ClassSkill : MonoBehaviour
{
    //protected int skillLevel = 0; no more!

    //[SerializeField] protected int skillLevel = 0;
    [SerializeField] protected string name = "";
    [SerializeField] protected string description = "";
    [SerializeField] protected Sprite iconSprite;

    public virtual void LevelUp()
    {

    }

    public int GetSkillLevel() 
    {
        return PlayerCharacterSheet.instance.GetSkillLevel(this);
    }
    public string GetName() { return name; }
    public string GetDescription() { return description; }
    public Sprite GetIconSprite() { return iconSprite; }


}
