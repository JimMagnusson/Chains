﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // Start is called before the first frame update

    //private CompanionStats companionStats;

    public string unitName;
    public EnemyType unitType;
    public int unitLevel;

    public int damage;

    public int maxHP;
    public int currentHP;

    public int xpReward;
    private bool dead = false;

    private void Start()
    {
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if(currentHP <= 0)
        {
            dead = true;
        }
    }

    public bool IsDead()
    {
        return dead;
    }
}
