using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    private static EnemyInfo instance;

    [SerializeField] private GameObject werewolf;
    [SerializeField] private GameObject zombie;
    [SerializeField] private GameObject leshen;
    private GameObject enemyPrefab;
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
}
