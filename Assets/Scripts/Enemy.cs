using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { ZOMBIE,WEREWOLF,LESHEN,IMP,GOLEM, NONE}
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyType enemyType;
    private EnemyInfo enemyInfo;
    // Start is called before the first frame update

    public EnemyType GetEnemyType()
    {
        return enemyType;
    }
    void Start()
    {
        DestroyIfInListOfDefeatedEnemies();
    }

    private void DestroyIfInListOfDefeatedEnemies()
    {
        enemyInfo = FindObjectOfType<EnemyInfo>();
        List<Vector3> defeatedEnemiesCoordinates = enemyInfo.GetDefeatedEnemiesPositions();
        if (defeatedEnemiesCoordinates != null)
        {
            for (int i = 0; i < defeatedEnemiesCoordinates.Count; i++)
            {
                if (transform.position == defeatedEnemiesCoordinates[i])
                {
                    Destroy(gameObject);
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
    }
}
