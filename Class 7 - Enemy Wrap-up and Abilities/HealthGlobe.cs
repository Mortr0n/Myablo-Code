using UnityEngine;

public class HealthGlobe : MonoBehaviour
{
    [SerializeField] GameObject healthFill;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventsManager.instance.onHealthChanged.AddListener(UpdateHealthBar);
    }

    private void OnDestroy()
    {
        EventsManager.instance.onHealthChanged.RemoveListener(UpdateHealthBar);
    }

    void UpdateHealthBar(float newHPPercent)
    {
        if (newHPPercent > 1) newHPPercent = 1;
        if (newHPPercent < 0) newHPPercent = 0;

        healthFill.transform.localScale = new Vector3(1, newHPPercent, 1);
    }
}
