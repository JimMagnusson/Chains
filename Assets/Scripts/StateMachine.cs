using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneState { OVERWORLD, BATTLE }
public class StateMachine : MonoBehaviour
{
    private static StateMachine instance;

    private SceneState sceneState = SceneState.OVERWORLD;

    public SceneState GetSceneState()
    {
        return sceneState;
    }

    public void SetSceneState(SceneState state)
    {
        sceneState = state;
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

}
