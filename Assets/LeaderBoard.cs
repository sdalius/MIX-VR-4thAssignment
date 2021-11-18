using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderBoard : MonoBehaviour
{
    // Start is called before the first frame update
    private TextMeshProUGUI  playersLeaderBoard;
    private TextMeshProUGUI  playersLeaderBoardNumbers;
    private List<(string, int)> generatedScores = new List<(string, int)>();

    void Start()
    {
        playersLeaderBoard = GameObject.FindGameObjectWithTag("PlayersScore").GetComponent<TextMeshProUGUI>();
        playersLeaderBoardNumbers = GameObject.FindGameObjectWithTag("ScoreNumbers").GetComponent<TextMeshProUGUI>();
        fillRandomPlayers();
    }

    private void fillRandomPlayers()
    {
        string[] playerNames = {"ElectricEel", "KrakenBite", "CaesarJ", "Pharos", "Endocryne", "Cothurnal", "RingRaid", "Trilemma", "PigPaddle", "Belting"};

        for(int i = 0; i < 10; i++)
        {
            generatedScores.Add((playerNames[Random.Range(1,10)], Random.Range(1,20)));
        }
        generatedScores.Sort(new ScoreComparer());
        generatedScores.Reverse();

        for (int i = 0 ; i < 10; i++)
        {
            playersLeaderBoard.text += i+1 + " " + generatedScores[i].Item1 + "\n";
            playersLeaderBoardNumbers.text += generatedScores[i].Item2 + "\n";
        }
    }

    private void insertPlayersScore(string playerName, int score)
    {
        if (playerName == "" || score < 0)
        {
            return;
        }

        generatedScores.Add((playerName,score));
        
        generatedScores.Sort(new ScoreComparer());
        generatedScores.Reverse();

        clearLeaderboards();

        for (int i = 0 ; i < 10; i++)
        {
            playersLeaderBoard.text += i+1 + " " + generatedScores[i].Item1 + "\n";
            playersLeaderBoardNumbers.text += generatedScores[i].Item2 + "\n";
        }
    }

    private void clearLeaderboards()
    {
        playersLeaderBoard.text = string.Empty;
        playersLeaderBoardNumbers.text = string.Empty;
    }
    
    class ScoreComparer : IComparer<(string, int)> 
    {
        public int Compare((string, int) player1, (string, int) player2) 
        {
            return player1.Item2.CompareTo(player2.Item2);
        }
    }
}
