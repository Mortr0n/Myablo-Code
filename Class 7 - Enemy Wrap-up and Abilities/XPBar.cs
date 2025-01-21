using UnityEngine;

public class XPBar : MonoBehaviour
{
    [SerializeField] GameObject xpFill;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventsManager.instance.onExperienceUpdated.AddListener(UpdateXPBar);
        EventsManager.instance.onExperienceUpdated.Invoke(0);
    }

    // Update is called once per frame
    void OnDestroy()
    {
        EventsManager.instance.onExperienceUpdated.RemoveListener(UpdateXPBar);
    }

    void UpdateXPBar(float newXPPercent)
    {
        if (newXPPercent > 1) newXPPercent = 1;
        if (newXPPercent < 0) newXPPercent = 0;

        xpFill.transform.localScale = new Vector3(newXPPercent, 1, 1);
    }
}
