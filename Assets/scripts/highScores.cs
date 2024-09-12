using UnityEngine;
using System.IO;
public class highScores : MonoBehaviour
{
    public int[] scores = new int[10];
    string currentDirectory;
    public string scoreFileName = "highscore.penis";
    // Start is called before the first frame update
    void Start()
    {
        currentDirectory = Application.dataPath;
        loadScoresFromFile();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadScoresFromFile()
    {
        bool fileExists = File.Exists(currentDirectory + "\\" + scoreFileName);
        if (fileExists == true)
        {
            Debug.Log("Found high score file " + scoreFileName);
        }
        else
        {
            Debug.Log("The file " + scoreFileName +" does not exist. No scores will be loaded.", this);
            return;
        }
        scores = new int[scores.Length];
        StreamReader fileReader = new StreamReader(currentDirectory +"\\" + scoreFileName);
        int scoreCount = 0;
        while (fileReader.Peek() != 0 && scoreCount < scores.Length)
        {
            string fileLine = fileReader.ReadLine();
            int readScore = -1;
            bool didParse = int.TryParse(fileLine, out readScore);
            if (didParse)
            {
                scores[scoreCount] = readScore;
            }
            else
            {
                Debug.Log("Invalid line in scores file at " + scoreCount +", using default value.", this);
                scores[scoreCount] = 0;
            }
            scoreCount++;
        }
        fileReader.Close();
        Debug.Log("High scores read from " + scoreFileName);
    }

    public void saveScoresToFile()
    {
        StreamWriter fileWriter = new StreamWriter(currentDirectory + "\\" + scoreFileName);
        for (int i = 0; i < scores.Length; i++)
            fileWriter.WriteLine(scores[i]);
        fileWriter.Close();
        Debug.Log("scores written to " + scoreFileName);
    }
    public void addScore(int newScore)
    {
        int desiredIndex = -1;
        for(int i = 0; i < scores.Length; i++)
        {
            if (scores[i] > newScore || scores[i] == 0)
            {
                desiredIndex = i;
                break;
            }
        }
        if(desiredIndex < 0)
        {
            Debug.Log(newScore + " not good enough for list");
            return;
        }
        for (int i = scores.Length -1; i > desiredIndex; i--)
            scores[i] = scores[i - 1];
        scores[desiredIndex] = newScore;
        Debug.Log(newScore + " got on the list");
    }
}
