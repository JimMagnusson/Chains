using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    private static EnemyInfo instance;

    [SerializeField] private GameObject werewolf;
    [SerializeField] private GameObject zombie;
    [SerializeField] private GameObject leshen;
    [SerializeField] public GameObject[] enemies;
    private GameObject enemyPrefab;
    public Vector3 enemyInBattlePos;
    public bool enemyDefeated;
    private void Awake()
    {
        //Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        Destroy(this.gameObject);
    }
    private void Start()
    {
    }

    public void SetEnemies(GameObject enemiesObj)
    {
        // There is probably a way better way to access the children objects of the object
        Enemy[] enemiesScripts = enemiesObj.GetComponentsInChildren<Enemy>();
        enemies = new GameObject[enemiesScripts.Length];
        for (int i = 0; i < enemiesScripts.Length; i++)
        {
            enemies[i] = enemiesScripts[i].gameObject;
        }
    }

    public void SetEnemyInBattlePos(Vector3 enemyPos)
    {
        enemyInBattlePos = enemyPos;
    }

    public GameObject GetPrefab ()
    {
        return enemyPrefab;
    }

    public void SetPrefab(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.ZOMBIE:
                {
                    enemyPrefab = zombie;
                    break;
                }
            case EnemyType.WEREWOLF:
                {
                    enemyPrefab = werewolf;
                    break;
                }
            case EnemyType.LESHEN:
                {
                    enemyPrefab = leshen;
                    break;
                }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveDefeatedEnemy()
    {
        for(int i = 0; i < enemies.Length; i++)
        {
            if (enemyInBattlePos == enemies[i].transform.position)
            {
                Destroy(enemies[i]);
            }
        }
    }

}
