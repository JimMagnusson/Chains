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
        companionStats = FindObjectOfType<CompanionStats>();
        unit = GetComponent<Unit>();
        unit.unitLevel = companionStats.level;
        unit.damage = companionStats.damage;
        unit.maxHP = companionStats.maxHP;
        unit.currentHP = companionStats.currentHP;
    }
}
