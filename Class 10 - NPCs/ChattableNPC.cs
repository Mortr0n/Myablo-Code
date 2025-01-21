using UnityEngine;
using System.Collections.Generic;

public class ChattableNPC : MonoBehaviour
{
    [SerializeField] protected string npcName = "";
    [SerializeField] protected List<DialogData> dialogDatas = new List<DialogData>();
    bool clicked = false;
    bool buttonReleased = true;

    private void Update()
    {
        if (clicked) RunClicked();
    }

    void RunClicked()
    {
        if (Input.GetMouseButtonDown(0) && buttonReleased) {
            clicked = false;
        } else buttonReleased = true;
        if (Vector3.Distance(PlayerController.instance.transform.position, transform.position) < 1.5f)
        {
            StartConversation();
        }
    }

    protected virtual void StartConversation()
    {
        clicked = false;
        DialogManager.instance.TriggerDialog(npcName, dialogDatas);
        PlayerController.instance.Movement().MoveToLocation(PlayerController.instance.transform.position);
    }
    private void OnMouseDown()
    {
        PlayerController.instance.Movement().MoveToLocation(transform.position);
        clicked = true;
        buttonReleased = false;
    }
}
