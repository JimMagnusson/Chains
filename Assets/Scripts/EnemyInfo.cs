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
    private List<Vector3> defeatedEnemiesPositions;
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
        defeatedEnemiesPositions = new List<Vector3>();
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

    public List<Vector3> GetDefeatedEnemiesPositions()
    {
        return defeatedEnemiesPositions;
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

    public void AddDefeatedEnemyToDefeatedEnemyPositions()
    {
        defeatedEnemiesPositions.Add(enemyInBattlePos);
        enemyDefeated = false;
    }

}
