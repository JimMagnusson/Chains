using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerStats playerStats;
    private EnemyInfo enemyInfo;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerStats = FindObjectOfType<PlayerStats>();
        enemyInfo = FindObjectOfType<EnemyInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(EnemyInFront())
            {
                playerStats.positionBeforeBattle = transform.position;
                StateMachine stateMachine = FindObjectOfType<StateMachine>();
                stateMachine.loadScene(SceneState.BATTLE);
                stateMachine.SetSceneState(SceneState.BATTLE);
            }
        }
    }

    private bool EnemyInFront()
    {
        // Checks if there is an enemy in front of the player. If there is, it also sets the enemy as the prefab to load into the battle. Also saves reference to gameobject in enemyInfo.
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
                enemyInfo.SetPrefab(enemies[i].GetComponent<Enemy>().GetEnemyType());
                enemyInfo.SetEnemyInBattlePos(enemies[i].transform.position);
                return true;
            }
        }
        return false;
    }
}
