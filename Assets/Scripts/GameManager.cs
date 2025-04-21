using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Instance")]
    public static GameManager instance;

    [Header("Game State")]
    public GameState gameState;

    [Header("Lists")]
    public List<GameObject> enemyList;
    public List<GameObject> circuitList;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            // TODO: later start in main menu
            gameState = GameState.InGame;
        }
        else
        {
            Destroy(gameObject);
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
}

public enum GameState
{
    //MainMenu,
    InGame,
    Inventory
}
