using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private static PlayerStats instance;

    public int level;
    public int maxHP;
    public int currentHP;
    public int damage;
    public Vector3 positionBeforeBattle = new Vector3(0, 0);

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
        maxHP += 10;
        currentHP = maxHP;
        damage += 1;
        xpThreshold = 50 * (int)Mathf.Pow((level + 1), 2) - 50 * (level + 1);
        xp = 0;
    }

}
