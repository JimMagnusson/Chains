using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class BattleSystem : MonoBehaviour
{
    private EnemyInfo enemyInfo;
    [SerializeField] private GameObject playerPrefab;
    private GameObject playerGO;
    [SerializeField] private GameObject enemyPrefab;
    private GameObject enemyGO;

    [SerializeField] private Transform playerSpawn;
    [SerializeField] private Transform enemySpawn;

    [SerializeField] private Text dialogueText;

    [SerializeField] private BattleHUD playerHUD;
    [SerializeField] private BattleHUD enemyHUD;

    [SerializeField] private BattleState state;

    private Unit playerUnit;
    private Unit enemyUnit;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        enemyInfo = FindObjectOfType<EnemyInfo>();
        enemyPrefab = enemyInfo.GetPrefab();
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        playerGO = Instantiate(playerPrefab, playerSpawn);
        playerUnit = playerGO.GetComponent<Unit>();
        enemyGO = Instantiate(enemyPrefab, enemySpawn);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = "You are confronted by a " + enemyUnit.unitName;

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(1.5f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    private void PlayerTurn()
    {
        dialogueText.text = "Choose an action";
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerAttack());
    }


    private IEnumerator PlayerAttack()
    {
        enemyUnit.TakeDamage(playerUnit.damage);
        bool enemyIsDead = enemyUnit.IsDead();
        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "The enemy was attacked";
        if(enemyIsDead)
        {
            state = BattleState.WON;
            yield return new WaitForSeconds(1);
            StartCoroutine(EndBattle());
        } else
        {
            state = BattleState.ENEMYTURN;
            yield return new WaitForSeconds(1);
            StartCoroutine(EnemyTurn());
        }
    }

    private IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(0.5f);

        playerUnit.TakeDamage(enemyUnit.damage);
        bool playerIsDead = playerUnit.IsDead();
        playerHUD.SetHP(playerUnit.currentHP);
        yield return new WaitForSeconds(0.5f);

        if (playerIsDead)
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        } else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }

    }

    private IEnumerator EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You have won the battle!";
            enemyInfo.enemyDefeated = true;
        } else if( state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated";
        }
        FindObjectOfType<PlayerStats>().currentHP = playerGO.GetComponent<Unit>().currentHP;
        yield return new WaitForSeconds(2f);
        FindObjectOfType<StateMachine>().loadScene(SceneState.OVERWORLD);
    }
}
