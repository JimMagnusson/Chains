using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionStats : MonoBehaviour
{
    private static CompanionStats instance;

    [SerializeField] private GameObject werewolfCompanion;
    [SerializeField] private GameObject zombieCompanion;
    [SerializeField] private GameObject leshenCompanion;
    private GameObject companionPrefab;

    public int level;
    public int maxHP;
    public int currentHP;
    public int damage;

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
        //Remove later when companion system in place
        companionPrefab = zombieCompanion;
    }

    public GameObject GetPrefab()
    {
        return companionPrefab;
    }

    public void SetPrefab(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.ZOMBIE:
                {
                    companionPrefab = zombieCompanion;
                    break;
                }
            case EnemyType.WEREWOLF:
                {
                    companionPrefab = werewolfCompanion;
                    break;
                }
            case EnemyType.LESHEN:
                {
                    companionPrefab = leshenCompanion;
                    break;
                }

        }
    }
}
