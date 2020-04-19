using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    private EnemyInfo enemyInfo;
    [SerializeField] private GameObject enemies;
    // Start is called before the first frame update
    void Start()
    {
        enemyInfo = FindObjectOfType<EnemyInfo>();
        enemyInfo.SetEnemies(enemies);
        if(enemyInfo.enemyDefeated)
        {
            enemyInfo.RemoveDefeatedEnemy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
