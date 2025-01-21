using UnityEngine;

public class ManaGlobe : MonoBehaviour
{
    [SerializeField] GameObject manaFill;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventsManager.instance.onManaChanged.AddListener(UpdateManaBar);
    }

    private void OnDestroy()
    {
        EventsManager.instance.onManaChanged.RemoveListener(UpdateManaBar);
    }

    void UpdateManaBar(float newManaPercent)
    {
        if (newManaPercent > 1) newManaPercent = 1;
        if (newManaPercent < 0) newManaPercent = 0;

        manaFill.transform.localScale = new Vector3(1, newManaPercent, 1);
    }
}
