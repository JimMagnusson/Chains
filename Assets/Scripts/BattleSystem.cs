using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum BattleState { START, PLAYERTURN, COMPANIONTURN, ENEMYTURN, WON, LOST }
public class BattleSystem : MonoBehaviour
{
    private EnemyInfo enemyInfo;
    private CompanionStats companionStats;

    [SerializeField] private GameObject playerPrefab;
    private GameObject playerGO;
    [SerializeField] private GameObject enemyPrefab;
    private GameObject enemyGO;
    [SerializeField] private GameObject companionPrefab;
    private GameObject companionGO;


    [SerializeField] private Transform playerSpawn;
    [SerializeField] private Transform enemySpawn;
    [SerializeField] private Transform companionSpawn;

    [SerializeField] private Text dialogueText;

    [SerializeField] private BattleHUD playerHUD;
    [SerializeField] private BattleHUD companionHUD;
    [SerializeField] private BattleHUD enemyHUD;

    [SerializeField] private BattleState state;

    private Unit playerUnit;
    private Unit enemyUnit;
    private Unit companionUnit;

    private bool companionIsDead = false;
    private bool playerIsDead = false;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        enemyInfo = FindObjectOfType<EnemyInfo>();
        enemyPrefab = enemyInfo.GetPrefab();
        companionStats = FindObjectOfType<CompanionStats>();
        companionPrefab = companionStats.GetPrefab();
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        playerGO = Instantiate(playerPrefab, playerSpawn);
        playerUnit = playerGO.GetComponent<Unit>();
        enemyGO = Instantiate(enemyPrefab, enemySpawn);
        enemyUnit = enemyGO.GetComponent<Unit>();
        companionGO = Instantiate(companionPrefab, companionSpawn);
        companionUnit = companionGO.GetComponent<Unit>();

        dialogueText.text = "You are confronted by a " + enemyUnit.unitName;

        playerHUD.SetHUD(playerUnit);
        companionHUD.SetHUD(companionUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(1.5f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    private void PlayerTurn()
    {
        dialogueText.text = "Choose player action";
    }

    private void CompanionTurn()
    {
        dialogueText.text = "Choose companion action";
    }

    public void OnAttackButton()
    {
        if (state == BattleState.PLAYERTURN)
        {
            StartCoroutine(PlayerAttack());
        }
        else if(state == BattleState.COMPANIONTURN)
        {
            StartCoroutine(CompanionAttack());
        }
        return;
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
            if(companionIsDead)
            {
                state = BattleState.ENEMYTURN;
                yield return new WaitForSeconds(1);
                StartCoroutine(EnemyTurn());
            }
            else
            {
                state = BattleState.COMPANIONTURN;
                yield return new WaitForSeconds(1);
                CompanionTurn();
            }
        }
    }

    private IEnumerator CompanionAttack()
    {
        enemyUnit.TakeDamage(companionUnit.damage);
        bool enemyIsDead = enemyUnit.IsDead();
        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "The enemy was attacked";
        if (enemyIsDead)
        {
            state = BattleState.WON;
            yield return new WaitForSeconds(1);
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.ENEMYTURN;
            yield return new WaitForSeconds(1);
            StartCoroutine(EnemyTurn());
        }
    }

    public void OnCaptureButton()
    {
        if (state == BattleState.PLAYERTURN)
        {
            StartCoroutine(Capture());
        }
        return;
    }

    private IEnumerator Capture()
    {
        int rndInt = UnityEngine.Random.Range(0, enemyUnit.maxHP);
        if(rndInt > enemyUnit.currentHP)
        {
            dialogueText.text = "You captured the enemy!";
            state = BattleState.WON;
            yield return new WaitForSeconds(2);
            companionStats.SetPrefabAndStartingStats(enemyUnit.unitType);
            StartCoroutine(EndBattle());
        }
        else
        {
            dialogueText.text = "Capture falied! Weaken the enemy to get better chance.";
            if (companionIsDead)
            {
                state = BattleState.ENEMYTURN;
                yield return new WaitForSeconds(2);
                StartCoroutine(EnemyTurn());
            }
            else
            {
                state = BattleState.COMPANIONTURN;
                yield return new WaitForSeconds(2);
                CompanionTurn();
            }
        }
    }

    private IEnumerator EnemyTurn()
    {
        dialogueText.text = "The " + enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(0.5f);
        if(companionIsDead)
        {
            playerUnit.TakeDamage(enemyUnit.damage);
            playerHUD.SetHP(playerUnit.currentHP);
            playerIsDead = playerUnit.IsDead();
            if (playerIsDead)
            {
                dialogueText.text = "You are dead.";
                yield return new WaitForSeconds(1f);
                state = BattleState.LOST;
                StartCoroutine(EndBattle());
            }
        }
        else
        {
            companionUnit.TakeDamage(enemyUnit.damage);
            companionHUD.SetHP(companionUnit.currentHP);
            companionIsDead = companionUnit.IsDead();
        }
        
        yield return new WaitForSeconds(0.5f);
        state = BattleState.PLAYERTURN;
        PlayerTurn();

    }

    public IEnumerator EndBattle()
    {
        if( state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated";
            yield return new WaitForSeconds(2f);
            ResetStatsAndInfoObjects();
            FindObjectOfType<Loader>().LoadLoseScreen();
        }
        dialogueText.text = "You have won the battle!";
        enemyInfo.enemyDefeated = true;

        PlayerStats playerStats = FindObjectOfType<PlayerStats>();
        playerStats.currentHP = playerGO.GetComponent<Unit>().currentHP;
        playerStats.IncreaseXP(enemyUnit.xpReward);
        if(playerStats.isTimeToLevelUp())
        {
            playerStats.LevelUp();
            dialogueText.text = "Player has leveled up!";
        }
        CompanionStats companionStats = FindObjectOfType<CompanionStats>();
        companionStats.IncreaseXP(enemyUnit.xpReward);
        if (companionStats.isTimeToLevelUp())
        {
            companionStats.LevelUp();
            dialogueText.text = "Companion has leveled up!";
        }

        yield return new WaitForSeconds(2f);
        FindObjectOfType<Loader>().LoadScene(SceneState.OVERWORLD);
    }

    private void ResetStatsAndInfoObjects()
    {
        Destroy(FindObjectOfType<EnemyInfo>());
        Destroy(FindObjectOfType<PlayerStats>());
        Destroy(FindObjectOfType<CompanionStats>());
    }
}
