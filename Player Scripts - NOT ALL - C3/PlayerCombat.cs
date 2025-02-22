using UnityEngine;

public class PlayerCombat : CombatReceiver
{
    protected float currentMana = 35;
    [SerializeField] protected float maxMana = 35;

    // Regen Vars
    protected float healthRegenBase = 0.5f;
    protected float healthRegenMod = 1f;
    protected float manaRegenBase = .5f;
    protected float healthMod = .1f;
    protected float manaRegenMod = 1f;
    protected float regenUpdateTickTimer = 0;
    protected float regenUpdateTickTime = 2f;

    protected void Update()
    {
        if (alive) RunRegen();
    }

    protected override void Start()
    {
        base.Start();
        factionID = GetComponent<PlayerController>().GetFactionID();

        EventsManager.instance.onPlayerLeveledUp.AddListener(LevelUp);
        EventsManager.instance.onStatPointSpent.AddListener(StatsChangedAdjustment);
    }

    private void OnDestroy()
    {
        EventsManager.instance.onPlayerLeveledUp.RemoveListener(LevelUp);
        EventsManager.instance.onStatPointSpent.RemoveListener(StatsChangedAdjustment);
    }

    public override void TakeDamage(float amount)
    {
        //Debug.Log("Take Damage");
        base.TakeDamage(amount);
        EventsManager.instance.onHealthChanged.Invoke(currentHP / maxHP);
    }

    public override void Die()
    {
        base.Die();
        GetComponent<PlayerController>().TriggerDeath();
        EventsManager.instance.onPlayerDied.Invoke();
    }

    #region ManaManagement
    public float GetMana()
    {
        return currentMana;
    }
    
    public void SpendMana(float amount)
    {
        currentMana -= amount;
        EventsManager.instance.onManaChanged.Invoke(currentMana / maxMana);
    }

    #endregion

    #region Level Up Events

    void LevelUp()
    {
        currentHP = maxHP;
        currentMana = maxMana;

        EventsManager.instance.onManaChanged.Invoke(currentMana / maxMana);
        EventsManager.instance.onHealthChanged.Invoke(currentHP / maxHP);
    }

    void StatsChangedAdjustment()
    {
        UpdateBaseRegen();

        float oldMaxHP = maxHP;
        float oldMaxMana = maxMana;

        maxHP = PlayerCharacterSheet.instance.GetMaxHP();
        maxMana = PlayerCharacterSheet.instance.GetMaxMana();

        currentHP += maxHP - oldMaxHP;
        currentMana += maxMana - oldMaxMana;

        EventsManager.instance.onManaChanged.Invoke(currentMana / maxMana);
        EventsManager.instance.onHealthChanged.Invoke(currentHP / maxHP);
    }

    #endregion

    #region Regen

    protected void RunRegen()
    {
        currentHP += (healthRegenBase * healthRegenMod * Time.deltaTime);
        if (currentHP > maxHP)
            currentHP = maxHP;

        currentMana += (manaRegenBase * manaRegenMod * Time.deltaTime);
        if (currentMana > maxMana)
            currentMana = maxMana;


        regenUpdateTickTimer += Time.deltaTime;
        if (regenUpdateTickTimer >= regenUpdateTickTime)
        {
            regenUpdateTickTimer -= regenUpdateTickTime;
            EventsManager.instance.onHealthChanged.Invoke(currentHP / maxHP);
            EventsManager.instance.onManaChanged.Invoke(currentMana / maxMana);
        }
    }

    public void SetHPRegenMod(float newMod)
    {
        healthRegenMod = newMod;
    }

    public void SetManaRegenMod(float newMod)
    {
        manaRegenMod = newMod;
    }

    protected void UpdateBaseRegen()
    {
        healthRegenBase = .5f + (.01f * PlayerCharacterSheet.instance.GetVitality());
        manaRegenBase = .5f + (.01f * PlayerCharacterSheet.instance.GetEnergy());
    }

    #endregion

    #region Health Management

    public void Heal(float amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;
        EventsManager.instance.onHealthChanged.Invoke(currentHP / maxHP);
    }

    public void SetHealMod(float newMod)
    {

        healthMod = newMod;
    }

    public float GetHealAmount()
    {
        return healthMod * GetMaxHealth();
    }

    #endregion
}
