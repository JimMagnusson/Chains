using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneState { OVERWORLD, BATTLE }
public class StateMachine : MonoBehaviour
{
    private static StateMachine instance;

    public int overworldBuildIndex = 0;
    public int battleScreenBuildIndex = 1;
    private SceneState sceneState = SceneState.OVERWORLD;

    public SceneState GetSceneState()
    {
        return sceneState;
    }

    public void SetSceneState(SceneState state)
    {
        sceneState = state;
    }

    public void loadScene(SceneState scene)
    {
        switch(scene)
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

    private void Awake()
    {
        //Singleton
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        Destroy(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
