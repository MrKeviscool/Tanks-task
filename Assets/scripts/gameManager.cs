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
        for(int i = 0; i < tanks.Length; i++)
        {
            tanks[i].SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
        if (Input.GetKeyUp(KeyCode.Return)) { 
        
        }
    }
    void gameStatePlaying()
    {

    }

    void gameStateGameover()
    {

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
}
