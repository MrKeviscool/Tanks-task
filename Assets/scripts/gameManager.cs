using UnityEngine;
using TMPro;
using UnityEngine.AI;

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

    public TextMeshProUGUI msgTxt;
    public TextMeshProUGUI timerTxt;

    [SerializeField] string startText = "Lets Get Ready To RUMBBLEEEEE";

    public highScores hs;

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
        timerTxt.gameObject.SetActive(false);
        msgTxt.text = startText;
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

        timerTxt.text = string.Format("{0:D2}:{1:D2}", seconds / 60, seconds % 60);

        if (isPLayerDed())
        {
            Debug.Log("loose");
            msgTxt.text = "YOU LOOSE LMAO";
            isGameOver = true;
        }
        else if (oneTankLeft())
        {
            Debug.Log("win");
            msgTxt.text = "les go (ur not even that good bro)";
            isGameOver = true;
            hs.addScore(Mathf.RoundToInt(gameTime));
            hs.saveScoresToFile();
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
                return false;
        }
        return true;
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
            timerTxt.gameObject.SetActive(true);
            msgTxt.text = "";
        }
    }
}
