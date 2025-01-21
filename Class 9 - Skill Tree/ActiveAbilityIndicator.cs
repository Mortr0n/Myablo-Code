using UnityEngine;
using UnityEngine.UI;

public class ActiveAbilityIndicator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventsManager.instance.onNewAbility2Equipped.AddListener(UpdateImage);
        UpdateImage(PlayerController.instance.GetAbility2());
    }
    private void OnDestroy()
    {
        EventsManager.instance.onNewAbility2Equipped.RemoveListener(UpdateImage);
    }
    // Update is called once per frame
    void UpdateImage(ClassSkill newAbility)
    {
        GetComponent<Image>().sprite = newAbility.GetIconSprite();
    }
}
