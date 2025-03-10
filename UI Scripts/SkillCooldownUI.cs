using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillCooldownUI : MonoBehaviour
{
    public Image skillCooldownOverlay;
    //public float dashCooldownTime = 1f;

    public void StartDashCooldown(float dashTime)
    {
        StartCoroutine(SkillCooldown(dashTime));
    }

    public IEnumerator SkillCooldown(float skillTime)
    {

        skillCooldownOverlay.fillAmount = 1f;
        float timeLeft = skillTime + .25f; //NOTE: This seemed to clear out too early visually so I added a little extra time to the fill amount

        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            skillCooldownOverlay.fillAmount = timeLeft / skillTime;
            yield return null;
        }
        skillCooldownOverlay.fillAmount = 0f;
    }
}
