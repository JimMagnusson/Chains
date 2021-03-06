﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattle : MonoBehaviour
{
    private PlayerStats playerStats;
    private Unit unit;
    // Start is called before the first frame update
    private void Awake()
    {
        StatsUpdate();
    }

    public void StatsUpdate()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        unit = GetComponent<Unit>();
        unit.unitLevel = playerStats.level;
        unit.damage = playerStats.damage;
        unit.maxHP = playerStats.maxHP;
        unit.currentHP = playerStats.currentHP;
    }
}
