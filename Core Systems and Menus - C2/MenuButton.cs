using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    #region Custom Scene Switch Functions
    public void LoadMenuScene()
    {
        SceneChanger.instance.LoadMenuScene();
    }
    public void LoadHowToPlayScene()
    {
        SceneChanger.instance.LoadHowToPlayScene();
    }
    public void LoadCreditsScene()
    {
        SceneChanger.instance.LoadCreditsScene();
    }
    public void LoadGameScene()
    {
        SceneChanger.instance.LoadGameScene();
    }
    
    #endregion
}
