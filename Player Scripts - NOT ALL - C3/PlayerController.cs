using System;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : Clickable
{
    [SerializeField] ClassSkillManager skillManager;

    [SerializeField] EquippableAbility ability1;
    [SerializeField] EquippableAbility ability2;

    [SerializeField] GameObject magicShield;
        
    
    bool alive = true;
    int factionID = 1;
    bool inDialog = false;
    Vector3 dashDirection = Vector3.zero;


    public static PlayerController instance;

    public void SetShieldActive()
    {
        magicShield.SetActive(true);
    }

    public void SetShieldInactive()
    {
        magicShield.SetActive(false);
    }

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

        // cam relative move direction
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        // Flatten to ignore vertical movement
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        if (Input.GetKeyDown(KeyCode.W))
        {
            dashDirection = camForward;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            dashDirection = -camForward;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            dashDirection = camRight;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            dashDirection = -camRight;
        }
        if (Input.GetMouseButtonDown(0) && ability1 != null) UseAbility1();
        if (Input.GetMouseButtonDown(1) && ability2 != null) UseAbility2();
        if (Input.GetKeyDown(KeyCode.Space) && dashDirection != Vector3.zero)
        {
            Dash(dashDirection);
        }
    }

    protected void Dash(Vector3 direction)
    {
        //myPlayer = player;
        //Debug.Log($"myPlayer: {this} Direction: {direction}");
        Vector3 dashDirection = direction;

        // Prevent dashing in place
        if (dashDirection == Vector3.zero) return;

         this.Movement().PerformDash(dashDirection);
        
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

