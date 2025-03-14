using UnityEngine;

public class MentalFortitudePassiveAbility : ClassSkill
{
    public override void LevelUp()
    {
        //if (PlayerCharacterSheet.instance.SkillPointSpendSuccessful())
        if (PlayerCharacterSheet.instance.TryToSpendSkillPoint(this));
        {
            int skillLevel = PlayerCharacterSheet.instance.GetSkillLevel(this);
            float manaRegenMod = 1 + .5f * skillLevel;
            PlayerController.instance.Combat().SetManaRegenMod(manaRegenMod);
        }
    }
   
}
