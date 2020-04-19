using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { ZOMBIE,WEREWOLF,LESHEN}
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyType enemyType;
    // Start is called before the first frame update

    public EnemyType GetEnemyType()
    {
        return enemyType;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            Destroy(gameObject);
        }
    }
}
