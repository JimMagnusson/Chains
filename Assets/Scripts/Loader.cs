using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    private int overworldBuildIndex = 1;
    private int battleScreenBuildIndex = 2;
    private int loseScreenBuildIndex = 3;
    private int winScreenBuildIndex = 4;

    public void LoadGame()
    {
        SceneManager.LoadScene(overworldBuildIndex);
    }

    public void LoadLoseScreen()
    {
        SceneManager.LoadScene(loseScreenBuildIndex);
    }

    public void LoadWinScreen()
    {
        SceneManager.LoadScene(winScreenBuildIndex);
    }

    public void LoadScene(SceneState scene)
    {
        switch (scene)
        {
            case SceneState.OVERWORLD:
                {
                    SceneManager.LoadScene(overworldBuildIndex);
                    break;
                }
            case SceneState.BATTLE:
                {
                    SceneManager.LoadScene(battleScreenBuildIndex);
                    break;
                }
        }
    }
}
