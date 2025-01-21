using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] TextMeshProUGUI dialogBodyText;
    [SerializeField] TextMeshProUGUI dialogNameText;

    List<DialogData> savedDialogDataList = new List<DialogData>();
    bool dialogRunning = false;
    bool dialogProgressedThisFrame = false;
    int dialogProgressionCount = 0;


    public static DialogManager instance;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        HideDialog();
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogRunning) RunDialog();
    }

    void HideDialog()
    {
        dialogBox.SetActive(false);
    }

    #region Utility
    bool ProgressDialogButtonPressed()
    {
        return (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E));
    }

    public bool IsDialogRunning()
    {
        return dialogRunning;
    }
    #endregion

    #region Dialog Core
    void NextDialog()
    {
        if(dialogProgressionCount >= savedDialogDataList.Count)
        {
            EndDialog();
        }
        else
        {
            // Set the first body test
            dialogBodyText.text = savedDialogDataList[dialogProgressionCount].dialogText;

            if (savedDialogDataList[dialogProgressionCount].dialogAudio  != null)
            {
                AudioManager.instance.PlayVoiceLine(savedDialogDataList[dialogProgressionCount].dialogAudio);
            }

            // Increment the Dialog Data List index
            dialogProgressionCount++;
        }
    }
    void EndDialog()
    {
        dialogRunning = false;
        dialogBox.SetActive(false);
        EventsManager.instance.onDialogEnded.Invoke();
    }

    public void TriggerDialog(string npcName, List<DialogData> dialogDatas)
    {
        if(dialogDatas == null)
        {
            Debug.LogError("Attempted to send a null Dialog Data List to DialogManager.TriggerDialog");
        }

        // Turn the dialog box parent GameObject on
        dialogBox.SetActive(true);
        // Set the NPC name
        dialogNameText.text = npcName;
        // Restart the counter for the list infex of information to pull from
        dialogProgressionCount = 0;

        savedDialogDataList = dialogDatas;
        NextDialog();

        // Set the dialog logic to run
        dialogRunning = true;
        EventsManager.instance.onDialogStarted.Invoke();
    }

    void RunDialog()
    {
        if(ProgressDialogButtonPressed() && !dialogProgressedThisFrame)
        {
            dialogProgressedThisFrame = true;
            NextDialog();
        }
        else
        {
            dialogProgressedThisFrame = false;
        }
    }
    #endregion
}

[Serializable]
public class DialogData
{
    public string dialogText = "";
    public AudioClip dialogAudio;
}