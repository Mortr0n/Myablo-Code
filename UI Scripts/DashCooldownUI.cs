using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class DashCooldownUI : MonoBehaviour
{
    public Image dashCooldownOverlay;
    //public float dashCooldownTime = 1f;

    public void StartDashCooldown(float dashTime)
    {
        StartCoroutine(DashCooldown(dashTime));
    }

    private IEnumerator DashCooldown(float dashTime)
    {

        dashCooldownOverlay.fillAmount = 1f;
        float timeLeft = dashTime + .25f; //NOTE: This seemed to clear out too early visually so I added a little extra time to the fill amount

        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            dashCooldownOverlay.fillAmount = timeLeft / dashTime;
            yield return null;
        }
        dashCooldownOverlay.fillAmount = 0f;
    }
}
