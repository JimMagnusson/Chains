using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    private enum Direction { UP, DOWN, LEFT, RIGHT};

    [SerializeField] private GameObject obstacles = null;
    [SerializeField] private GameObject enemiesObj = null;
    private GameObject[] enemies;

    private Tilemap obstaclesTilemap;
    private Vector3Int posInt;
    // Start is called before the first frame update
    void Start()
    {
        obstaclesTilemap = obstacles.GetComponent<Tilemap>();
        enemies = getEnemiesInArray();
    }

    private void printEnemyPositions()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            Debug.Log(enemies[i].transform.position);
        }
    }

    private GameObject[] getEnemiesInArray()
    {
        Enemy[] enemyScripts = enemiesObj.GetComponentsInChildren<Enemy>();
        GameObject[] enemies = new GameObject[enemyScripts.Length];
        for (int i = 0; i < enemyScripts.Length; i++)
        {
            enemies[i] = enemyScripts[i].gameObject;
        }
        return enemies;
    }



    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(Direction.UP);
            if(HasTileCollision() || HasEnemyCollision())
            {
                Move(Direction.DOWN);
            }
        }
        else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(Direction.DOWN);
            if (HasTileCollision() || HasEnemyCollision())
            {
                Move(Direction.UP);
            }
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Direction.RIGHT);
            if (HasTileCollision() || HasEnemyCollision())
            {
                Move(Direction.LEFT);
            }
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Direction.LEFT);
            if (HasTileCollision() || HasEnemyCollision())
            {
                Move(Direction.RIGHT);
            }
        }

        // probably need to check if the enemy array has changed size
    }

    private bool HasTileCollision()
    {
        // -1 on x because of the Tilemap
        posInt = new Vector3Int((int)transform.position.x - 1, (int)transform.position.y, (int)transform.position.z);
        return obstaclesTilemap.HasTile(posInt);
    }

    private bool HasEnemyCollision()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if(transform.position == enemies[i].transform.position)
            {
                return true;
            }
        }
        return false;
    }

    private void Move(Direction dir)
    {
        switch (dir)
        {
            case Direction.UP:
                {
                    transform.position += Vector3.up;
                    // Get current standing tile
                    // If it is in tilemap Obstacles
                    // Move it back
                    break;
                }
            case Direction.DOWN:
                {
                    transform.position += Vector3.down;
                    break;
                }
            case Direction.RIGHT:
                {
                    transform.position += Vector3.right;
                    break;
                }
            case Direction.LEFT:
                {
                    transform.position += Vector3.left;
                    break;
                }

            default:
                {
                    break;
                }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }
}
