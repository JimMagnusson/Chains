using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // Start is called before the first frame update

    //private CompanionStats companionStats;

    public string unitName;
    public int unitLevel;

    public int damage;

    public int maxHP;
    public int currentHP;
    private bool dead = false;
    public int level;

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
