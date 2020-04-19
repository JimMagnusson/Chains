using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum Direction { UP, DOWN, LEFT, RIGHT };
public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private GameObject obstacles = null;
    [SerializeField] private GameObject enemiesObj = null;
    private GameObject[] enemies;

    private Tilemap obstaclesTilemap;
    private Vector3Int posInt;
    private Direction currentDirection = Direction.DOWN;
    private PlayerSprite playerSprite;
    // Start is called before the first frame update
    void Start()
    {
        obstaclesTilemap = obstacles.GetComponent<Tilemap>();
        enemies = getEnemiesInArray();
        playerSprite = GetComponent<PlayerSprite>();
    }

    private void PrintEnemyPositions()
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

    public Direction getCurrentDirection()
    {
        return currentDirection;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(Direction.UP);
            currentDirection = Direction.UP;
            if (HasTileCollision() || HasEnemyCollision())
            {
                Move(Direction.DOWN);
            }
        }
        else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(Direction.DOWN);
            currentDirection = Direction.DOWN;
            if (HasTileCollision() || HasEnemyCollision())
            {
                Move(Direction.UP);
            }
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Direction.RIGHT);
            currentDirection = Direction.RIGHT;

            if (HasTileCollision() || HasEnemyCollision())
            {
                Move(Direction.LEFT);
            }
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Direction.LEFT);
            currentDirection = Direction.LEFT;
            if (HasTileCollision() || HasEnemyCollision())
            {
                Move(Direction.RIGHT);
            }
        }

        enemies = getEnemiesInArray();
        playerSprite.ChangeSprite(currentDirection);
    }

    private bool HasTileCollision()
    {
        // -1 on x because of the Tilemap
        posInt = new Vector3Int((int)transform.position.x - 1, (int)transform.position.y, (int)transform.position.z);
        return obstaclesTilemap.HasTile(posInt);
    }

    public GameObject[] getEnemies()
    {
        return enemies;
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
