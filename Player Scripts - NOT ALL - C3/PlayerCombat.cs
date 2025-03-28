using UnityEngine;

public class PlayerCombat : CombatReceiver
{
    protected float currentMana = 35;
    [SerializeField] private bool isInvincible = false;
    [SerializeField] protected float maxMana = 35;
    [SerializeField] protected float shieldSize = 10f;
    [SerializeField] protected float currentShield = 0;

    // Regen Vars
    protected float healthRegenBase = 0.5f;
    protected float healthRegenMod = 1f;
    protected float manaRegenBase = .5f;
    protected float healthMod = .1f;
    protected float manaRegenMod = 1f;
    protected float regenUpdateTickTimer = 0;
    protected float regenUpdateTickTime = 2f;


    public float GetCurrentShield() { return currentShield; }
    public void SetCurrentShield(float newShield) { currentShield = newShield; }
    public void SetShieldSize(float newSize) { shieldSize = newSize; }
    public float GetShieldSize() { return shieldSize; }

    protected void Update()
    {
        if (alive) RunRegen();
        if (currentShield <= 0) PlayerController.instance.SetShieldInactive();
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
        if (isInvincible) return;
        //Debug.Log($"Taking Damage {amount}");
        if (!alive) { return; }
        //TODO: Could change shield logic to only block a percentage of damage, that can be increased with level. But I'm staying simple for now
        if (currentShield > 0)
        {
            if (currentShield >= amount)
            {
                currentShield -= amount;
                return;
            }
            else
            {
                amount -= currentShield;
                currentShield = 0;
            }
        }
        //Debug.Log($"Player HP: {currentHP} Damage: {amount} ");
        currentHP -= amount;
        if (currentHP <= 0) Die();
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
