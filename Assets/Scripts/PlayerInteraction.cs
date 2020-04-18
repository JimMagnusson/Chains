using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(EnemyInFront())
            {
                StateMachine stateMachine = FindObjectOfType<StateMachine>();
                stateMachine.loadScene(SceneState.BATTLE);
                stateMachine.SetSceneState(SceneState.BATTLE);
            }
        }
    }

    private bool EnemyInFront()
    {
        // Checks if there is an enemy in front of the player. If there is, it also sets the enemy as the prefab to load into the battle.
        int xOffset = 0;
        int yOffset = 0;
        // CHeck if there is an enemy on the tile according to player direction
        GameObject[] enemies = playerMovement.getEnemies();
        switch (playerMovement.getCurrentDirection())
        {
            case Direction.UP:
                {
                    yOffset = 1;
                    break;
                }
            case Direction.DOWN:
                {
                    yOffset = -1;
                    break;
                }
            case Direction.LEFT:
                {
                    xOffset = -1;
                    break;
                }
            case Direction.RIGHT:
                {
                    xOffset = 1;
                    break;
                }
        }
        for (int i = 0; i < enemies.Length; i++)
        {
            Vector3 posInFront = new Vector3(transform.position.x + xOffset, transform.position.y + yOffset, transform.position.z);
            if (posInFront == enemies[i].transform.position)
            {
                FindObjectOfType<EnemyInfo>().SetPrefab(enemies[i].GetComponent<Enemy>().GetEnemyType());
                return true;
            }
        }
        return false;
    }
}
