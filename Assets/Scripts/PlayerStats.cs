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
    public Vector3 positionBeforeBattle = new Vector3(0,0);
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

}
