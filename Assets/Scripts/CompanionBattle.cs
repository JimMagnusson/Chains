using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionBattle : MonoBehaviour
{
    private CompanionStats companionStats;
    private Unit unit;
    // Start is called before the first frame update
    private void Awake()
    {
        StatsUpdate();
    }

    public void StatsUpdate()
    {
        companionStats = FindObjectOfType<CompanionStats>();
        unit = GetComponent<Unit>();
        unit.unitLevel = companionStats.GetLevel();
        unit.damage = companionStats.GetDamage();
        unit.maxHP = companionStats.GetMaxHP();
        unit.currentHP = companionStats.GetCurrentHP();
    }
}
