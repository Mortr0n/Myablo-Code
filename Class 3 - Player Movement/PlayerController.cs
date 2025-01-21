using System;
using UnityEngine;

public class PlayerController : Clickable
{
    [SerializeField] ClassSkillManager skillManager;

    [SerializeField] EquippableAbility ability1;
    [SerializeField] EquippableAbility ability2;
        
    
    bool alive = true;
    int factionID = 1;
    bool inDialog = false;

    public static PlayerController instance;
    private void Awake()
    {
        if (instance == null) { instance = this; }
        else Destroy(gameObject);
    }

    void Start()
    {
        // finds main controller gets the object it's attached to and then adds the controller.  Very Cool!  Instead of adding to it in the inspector so you could change it out
        CameraController cameraController = Camera.main.gameObject.AddComponent<CameraController>(); // in class they spelled both out and did not set it to a variable.  I felt like I wanted to try the var way
        //TODO: Set follow target needs reenabled if I try to use the camera controller again.
        //cameraController.SetFollowTarget(gameObject); // run the CameraControllers SetFollowTarget
        //Camera.main.gameObject.GetComponent<CameraController>().SetFollowTarget(gameObject); // run the CameraControllers SetFollowTarget function original class way.
        EventsManager.instance.onDialogStarted.AddListener(StartDialogMode);
        EventsManager.instance.onDialogEnded.AddListener(EndDialogMode);
    }

    private void OnDestroy()
    {
        EventsManager.instance.onDialogStarted.RemoveListener(StartDialogMode);
        EventsManager.instance.onDialogEnded.RemoveListener(EndDialogMode);
    }

    void Update()
    {
        if (inDialog) return;
        if (!alive) return;

        if (Input.GetMouseButtonDown(0) && ability1 != null) UseAbility1();
        if (Input.GetMouseButtonDown(1) && ability2 != null) UseAbility2();
    }


    #region Ability Stuff
    void UseAbility1()
    {
        ability1.RunAbilityClicked(this);
    }

    void UseAbility2()
    {
        ability2.RunAbilityClicked(this);
    }
    public void SetAbility2(EquippableAbility newAbility)
    {
        ability2 = newAbility;
        EventsManager.instance.onNewAbility2Equipped.Invoke(ability2);
    }
    public EquippableAbility GetAbility2()
    {
        return ability2;
    }
    #endregion

    #region Utility
    public PlayerMovement Movement()
    {
        return GetComponent<PlayerMovement>();
    }
    public PlayerAnimator GetAnimator()
    {
        return GetComponent<PlayerAnimator>();
    }
    public PlayerCharacterSheet GetCharacterSheet()
    {
        return GetComponent<PlayerCharacterSheet>();
    }
    public PlayerCombat Combat()
    {
        return GetComponent<PlayerCombat>();
    }

    public ClassSkillManager SkillManager()
    {
        return skillManager;
    }
    public int GetFactionID()
    {
        //Debug.Log("Player GetFactionID");
        return factionID;
    }
    #endregion
    
    public void TriggerDeath()
    {
        alive = false;
        GetAnimator().TriggerDeath();
    }

    public void TriggerRevive()
    {
        alive = true;
        GetAnimator().TriggerRevive();
    }

    #region Dialog Mode Listeners
    public void StartDialogMode()
    {
        inDialog = true;
    }

    public void EndDialogMode()
    {
        inDialog = false;
    }
    #endregion

}

