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
    [SerializeField] private GameObject enemyPrefab;

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
        GameObject playerGO = Instantiate(playerPrefab, playerSpawn);
        playerUnit = playerGO.GetComponent<Unit>();
        GameObject enemyGO = Instantiate(enemyPrefab, enemySpawn);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = "You are confronted by a " + enemyUnit.unitName;

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

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
            yield return new WaitForSeconds(2);
            EndBattle();
        } else
        {
            state = BattleState.ENEMYTURN;
            yield return new WaitForSeconds(2);
            StartCoroutine(EnemyTurn());
        }
    }

    private IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(1f);

        playerUnit.TakeDamage(playerUnit.damage);
        bool playerIsDead = playerUnit.IsDead();
        playerHUD.SetHP(playerUnit.currentHP);
        yield return new WaitForSeconds(1f);

        if (playerIsDead)
        {
            state = BattleState.LOST;
            EndBattle();
        } else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }

    }

    private void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You have won the battle!";
        } else if( state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated";
        }
    }
}
