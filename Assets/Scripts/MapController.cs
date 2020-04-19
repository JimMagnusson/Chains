using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    private EnemyInfo enemyInfo;
    [SerializeField] private GameObject enemiesObj;
    // Start is called before the first frame update
    void Awake()
    {
        enemyInfo = FindObjectOfType<EnemyInfo>();
        enemyInfo.SetEnemies(enemiesObj);
        if(enemyInfo.enemyDefeated)
        {
            enemyInfo.AddDefeatedEnemyToDefeatedEnemyPositions();
        }
    }
}
