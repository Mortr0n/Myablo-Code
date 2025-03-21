using System;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : Clickable
{
    [SerializeField] ClassSkillManager skillManager;

    [SerializeField] EquippableAbility ability1;
    [SerializeField] EquippableAbility ability2;

    bool canCastAbility1 = true;
    bool canCastAbility2 = true;
    //[SerializeField] float ability1Cooldown = 2f;
    [SerializeField] float ability2Cooldown = 1f;
    SkillCooldownUI skillCooldownUI;

    [SerializeField] GameObject magicShield;
       
    
    bool alive = true;
    int factionID = 1;
    bool inDialog = false;
    Vector3 dashDirection = Vector3.zero;
    float lastCameraYRotation = 0f; // Store last camera rotation

    public static PlayerController instance;

    public void SetShieldActive()
    {
        magicShield.SetActive(true);
    }

    public void SetShieldInactive()
    {
        magicShield.SetActive(false);
    }

    public bool GetCanCastAbility1()
    {
        return canCastAbility1;
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
        if (inDialog || !alive) return;

        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        // If movement key is pressed, store dash direction & camera rotation
        if (Input.GetKeyDown(KeyCode.W))
        {
            dashDirection = camForward;
            lastCameraYRotation = Camera.main.transform.eulerAngles.y;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            dashDirection = -camForward;
            lastCameraYRotation = Camera.main.transform.eulerAngles.y;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            dashDirection = camRight;
            lastCameraYRotation = Camera.main.transform.eulerAngles.y;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            dashDirection = -camRight;
            lastCameraYRotation = Camera.main.transform.eulerAngles.y;
        }

        if (Input.GetMouseButtonDown(0) && ability1 != null && canCastAbility1) UseAbility1();
        if (Input.GetMouseButtonDown(1) && ability2 != null) UseAbility2();

        // Adjust dash direction if camera has rotated
        if (Input.GetKeyDown(KeyCode.Space) && dashDirection != Vector3.zero)
        {
            Dash(dashDirection);
        }
    }

    protected void Dash(Vector3 direction)
    {
        float currentCameraYRotation = Camera.main.transform.eulerAngles.y;
        float rotationDifference = currentCameraYRotation - lastCameraYRotation;

        // Apply the rotation difference to the dash direction
        Quaternion rotationAdjustment = Quaternion.Euler(0, rotationDifference, 0);
        Vector3 adjustedDashDirection = rotationAdjustment * direction;

        Debug.Log($"Dashing! Adjusted Direction: {adjustedDashDirection}");

        transform.rotation = Quaternion.LookRotation(adjustedDashDirection);

        this.Movement().PerformDash(adjustedDashDirection);
    }

    #region Ability Stuff
    // could not get the wait to work as an ability 1 so there's a melee wait in the equippable ability
    void UseAbility1()
    {
        ability1.RunAbilityClicked(this, 0);
    }

    void UseAbility2()
    {
        if (canCastAbility2) StartCoroutine(Ability2WaitTimer());
    }


    public IEnumerator Ability2WaitTimer()
    {
        ability2.RunAbilityClicked(this, ability2Cooldown);
        canCastAbility2 = false;
        yield return new WaitForSeconds(ability2Cooldown);
        canCastAbility2 = true;
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

