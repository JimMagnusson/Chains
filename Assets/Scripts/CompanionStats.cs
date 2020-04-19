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

    [SerializeField] private int level;
    [SerializeField] private int maxHP;
    [SerializeField] private int currentHP;
    [SerializeField] private int damage;

    [SerializeField] private int xp = 0;
    [SerializeField] private int xpThreshold = 50;

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
        SetPrefabAndStartingStats(EnemyType.ZOMBIE);
    }

    public int GetLevel()
    {
        return level;
    }

    public int GetMaxHP()
    {
        return maxHP;
    }

    public int GetCurrentHP()
    {
        return currentHP;
    }

    public int GetDamage()
    {
        return damage;
    }
    public GameObject GetPrefab()
    {
        return companionPrefab;
    }

    public void SetPrefabAndStartingStats(EnemyType type)
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

        level = companionPrefab.GetComponent<Unit>().unitLevel;
        maxHP = companionPrefab.GetComponent<Unit>().maxHP;
        currentHP = companionPrefab.GetComponent<Unit>().currentHP;
        damage = companionPrefab.GetComponent<Unit>().damage;
        xpThreshold = 50;
    }

    public void IncreaseXP(int amount)
    {
        xp += amount;
    }

    public bool isTimeToLevelUp()
    {
        return xp >= xpThreshold;
    }

    public void LevelUp()
    {
        level += 1;
        maxHP += 10;
        currentHP = maxHP;
        damage += 1;
        xp -= xpThreshold;
        xpThreshold = 50*(int)Mathf.Pow((level), 2) - 50*(level);
        if(isTimeToLevelUp())
        {
            LevelUp();
        }
    }
}
