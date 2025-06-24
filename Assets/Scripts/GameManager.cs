using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Instance")]
    public static GameManager instance;

    [Header("Game State")]
    public GameState gameState;

    [Header("Lists")]
    public List<GameObject> lootTable;
    public List<GameObject> enemyList;
    public List<GameObject> circuitList;

    [Header("Location")]
    public Transform dropLocation;

    [Header("UI")]
    public GameObject gameOverScreen;

    private int previousEnemyCount = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
            // TODO: later start in main menu
            gameState = GameState.InGame;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (enemyList.Count == 0 && previousEnemyCount > 0)
        {
            GenerateLoot(dropLocation.position);
        }
        previousEnemyCount = enemyList.Count;
        if (Input.GetKeyDown(KeyCode.N))
        {
            SceneManager.LoadScene("SampleScene");
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("WaveDing");
        }
    }
    public void SetGameState(GameState newGameState)
    {
        gameState = newGameState;
        switch (newGameState)
        {
            //case GameState.MainMenu:
            //    break;
            case GameState.InGame:
                break;
            case GameState.Inventory:
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().direction = Vector2.zero;
                break;
        }
    }
    public void GenerateLoot(Vector3 dropPosition)
    {
        circuitList.Add(Instantiate(lootTable[Random.Range(0, lootTable.Count)], dropPosition, Quaternion.identity));
    }
    public void GameOver()
    {
        gameState = GameState.Dead;
        gameOverScreen.SetActive(true);
    }
}
public enum GameState
{
    //MainMenu,
    InGame,
    Inventory,
    Dead
}
