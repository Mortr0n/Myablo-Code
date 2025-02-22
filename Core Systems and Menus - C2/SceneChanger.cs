using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger instance;

    [SerializeField] string menuSceneName;
    [SerializeField] string howToPlaySceneName;
    [SerializeField] string creditsSceneName;
    [SerializeField] string gameSceneName;

    [Space(10)]
    [SerializeField] string level1SceneName;
    [SerializeField] string level2SceneName;
    [SerializeField] string level3SceneName;
    [SerializeField] string hubSceneName;
    [SerializeField] string bossSceneName;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(menuSceneName);
    }
    public void LoadHowToPlayScene()
    {
        // had issues with scene changer not storing the saved HowToPlay on game start so this is helpful to see that.
        //Debug.Log($"Loading Scene: {howToPlaySceneName}");
        SceneManager.LoadScene(howToPlaySceneName);
    }
    public void LoadCreditsScene()
    {
        SceneManager.LoadScene(creditsSceneName);
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene(gameSceneName);
    }
    //-----------------------------------
    public void LoadLevel1Scene()
    {
        SceneManager.LoadScene(level1SceneName);
    }
    public void LoadLevel2Scene()
    {
        SceneManager.LoadScene(level2SceneName);
    }
    public void LoadLevel3Scene()
    {
        SceneManager.LoadScene(level3SceneName);
    }
    public void LoadHubScene()
    {
        SceneManager.LoadScene(hubSceneName);
    }
    public void LoadBossScene()
    {
        SceneManager.LoadScene(bossSceneName);
    }
}
