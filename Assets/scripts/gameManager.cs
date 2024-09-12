using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public GameObject[] tanks;
    float gameTime = 0;
    public float GameTime { get { return gameTime; } }
    public enum GameState
    {
        start,
        playing,
        gameover,
    };

    GameState gameState;
    public GameState state { get { return gameState; } }

    private void Awake()
    {
        gameState = GameState.start;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < tanks.Length; i++)
        {
            tanks[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            Application.Quit();
        switch (gameState)
        {
            case GameState.start:
                gameStateStart();
                break;
            case GameState.playing:
                gameStatePlaying();
                break;
            case GameState.gameover:
                gameStateGameover();
                break;
        }
    }

    void gameStateStart()
    {
        onNewGame();
    }
    void gameStatePlaying()
    {
        bool isGameOver = false;
        gameTime += Time.deltaTime;
        int seconds = Mathf.RoundToInt(gameTime);

        if (isPLayerDed())
        {
            Debug.Log("loose");
            isGameOver = true;
        }
        else if (oneTankLeft())
        {
            Debug.Log("win");
            isGameOver = true;
        }
        if (isGameOver)
        {
            gameState = GameState.gameover;
        }
    }

    void gameStateGameover()
    {
        onNewGame();
    }

    bool oneTankLeft()
    {
        int numTanksLeft = 0;
        for(int i = 0; i < tanks.Length; i++)
        {
            if (tanks[i].activeSelf)
                numTanksLeft++;
        }
        return numTanksLeft <= 1;
    }

    bool isPLayerDed()
    {
        for(int i = 0; i < tanks.Length; i++)
        {
            if (tanks[i].activeSelf && tanks[i].tag == "Player")
                return true;
        }
        return false;
    }

    void onNewGame()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            gameTime = 0;
            gameState = GameState.playing;
            for (int i = 0; i < tanks.Length; i++)
            {
                tanks[i].SetActive(true);
            }
        }
    }
}
