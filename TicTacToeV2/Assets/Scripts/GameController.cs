using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class GameController : MonoBehaviour
{

    public int whoTurn; // 0=x & 1=o
    public int turnCount; //counts the number of turns
    public GameObject[] turnIcons;
    public Sprite[] playIcons;
    public Button[] tictactoeSpaces;
    public int[] markedSpaces; //ID which space is marked by player
    public Text winnerText;
    public GameObject[] winningLine; // Holds lines for the winning move
    public GameObject winnerPanel;
    public int xPlayersScore;
    public int oPlayersScore;
    public Text xPlayersScoreText;
    public Text oPlayersScoreText;

    // Start is called before the first frame update
    void Start()
    {
        GameSetup();
    }

    void GameSetup()
    {
        whoTurn = 0;
        turnCount = 0;
        turnIcons[0].SetActive(true);
        turnIcons[1].SetActive(false);
        for (int i = 0; i < tictactoeSpaces.Length; i++)
        {
            tictactoeSpaces[i].interactable = true;
            tictactoeSpaces[i].GetComponent<Image>().sprite = null;
        }
        for(int i = 0; i < markedSpaces.Length; i++)
        {
            markedSpaces[i] = -100;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TicTacToeButton(int WhichNumber)
    {
        tictactoeSpaces[WhichNumber].image.sprite = playIcons[whoTurn];
        tictactoeSpaces[WhichNumber].interactable = false;

        markedSpaces[WhichNumber] = whoTurn+1;
        turnCount++;
        if(turnCount > 4)
        {
            WinnerCheck();
        }

        if (whoTurn == 0)
        {
            whoTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
        else
        {
            whoTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }
    }

    void WinnerCheck()
    {
        int s1 = markedSpaces[0] + markedSpaces[1] + markedSpaces[2];
        int s2 = markedSpaces[3] + markedSpaces[4] + markedSpaces[5];
        int s3 = markedSpaces[6] + markedSpaces[7] + markedSpaces[8];
        int s4 = markedSpaces[0] + markedSpaces[3] + markedSpaces[6];
        int s5 = markedSpaces[1] + markedSpaces[4] + markedSpaces[7];
        int s6 = markedSpaces[2] + markedSpaces[5] + markedSpaces[8];
        int s7 = markedSpaces[0] + markedSpaces[4] + markedSpaces[8];
        int s8 = markedSpaces[0] + markedSpaces[4] + markedSpaces[6];
        var solutions = new int[] { s1, s2, s3, s4, s5, s6, s7, s8 };
        for(int i = 0; i < solutions.Length; i++)
        {
            if(solutions[i] == 3 *(whoTurn+1))
            {
                WinnerDisplay(i);
                return;
            }
        }
    }

    void WinnerDisplay(int indexIn)
    {
        winnerPanel.gameObject.SetActive(true);
        if(whoTurn == 0)
        {
            xPlayersScore++;
            xPlayersScoreText.text = xPlayersScore.ToString();
            winnerText.text = "Player X Wins!";
        }
        else if(whoTurn == 1)
        {
            oPlayersScore++;
            oPlayersScoreText.text = oPlayersScore.ToString();
            winnerText.text = "Player O Wins!";
        }
        winningLine[indexIn].SetActive(true);
    }
}
