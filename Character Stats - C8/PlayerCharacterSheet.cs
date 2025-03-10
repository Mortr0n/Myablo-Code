using UnityEngine;

public class PlayerCharacterSheet : MonoBehaviour
{
    int level = 1;
    float experience = 0;

    float strength = 15;
    float dexterity = 15;
    float vitality = 15;
    float energy = 15;

    //float currentHitpoints = 35;
    //float maxHitpoints = 35;
    //float currentMana = 35;
    //float maxMana = 35;

    int statPointsToSpend = 0;
    int skillPointsToSpend = 0;

    public static PlayerCharacterSheet instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject); //TODO:  do I need?
    }

    #region Listeners
    private void Start()
    {
        EventsManager.instance.onExperienceGranted.AddListener(AddExperience);
    }
    private void OnDestroy()
    {
        EventsManager.instance.onExperienceGranted.RemoveListener(AddExperience);
    }
    #endregion

    #region Levels and Experience
    public int GetLevel()
    {
        return level;
    }

    public float GetExperience()
    {
        return experience;
    }

    public void AddExperience(float amount)
    {
        experience += amount;
        if (experience >= GetExperienceToNextLevel())
        {
            LevelUp();
        }

        EventsManager.instance.onExperienceUpdated.Invoke(experience / GetExperienceToNextLevel());
    }
    float GetExperienceToNextLevel()
    {
        return (100f * Mathf.Pow(1.12f, level));
        /* at level 1 100 * 1.12 = 112
         * at level 10 100 * 1.12^10 = 313
         * at level 20 100 * 1.12^20 = 877
         * at level 30 100 * 1.12^30 = 2451
         * at level 40 100 * 1.12^40 = 6853
         * at level 50 100 * 1.12^50 = 19179
         * at level 100 100 * 1.12^100 = 1.3e+08  <== Big number! 
         */
    }
    void LevelUp()
    {
        experience -= GetExperienceToNextLevel();
        level++;
        statPointsToSpend += 5;
        skillPointsToSpend++;
        EventsManager.instance.onPlayerLeveledUp.Invoke();
    }
    #endregion

    #region Stats
    public float GetStrength() { return strength; }
    public float GetDexterity()  { return dexterity; }

    public float GetVitality() { return vitality; }
    public float GetEnergy() { return energy; }
    #endregion

    #region Hitpoints
    public float GetMaxHP()
    {
        return (5 + (5 * vitality));
    }

    public float GetMaxMana()
    {
        return (5 + (4 * energy));
    }
    #endregion

    #region Stat and Skill Getters
    public int GetStatPointsToSpend()
    {
        return statPointsToSpend;
    }
    public int GetSkillPointsToSpend()
    {
        return skillPointsToSpend;
    }
    #endregion

    #region Stat Point Spend
    bool PointSpendSuccessful()
    {
        if(statPointsToSpend <= 0) return false;
        else
        {
            statPointsToSpend--;
            return true;
        }
    }
    public void BuyStrengthPoint()
    {
        if (PointSpendSuccessful()) strength++;
        EventsManager.instance.onStatPointSpent.Invoke();
    }
    public void BuyDexterityPoint()
    {
        if (PointSpendSuccessful()) dexterity++;
        EventsManager.instance.onStatPointSpent.Invoke();
    }
    public void BuyVitalityPoint()
    {
        if (PointSpendSuccessful()) vitality++;
        EventsManager.instance.onStatPointSpent.Invoke();
    }
    public void BuyEnergyPoint()
    {
        if (PointSpendSuccessful()) energy++;
        EventsManager.instance.onStatPointSpent.Invoke();
    }
    #endregion

    #region Skill Point Spend
    public bool SkillPointSpendSuccessful()
    {
        if (skillPointsToSpend <= 0) return false;
        else
        {
            skillPointsToSpend--;
            return true;
        }
    }

    #endregion
}
