using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour, IEndGame
{
    public static bool EndGameFlag { get; set; }
    public static int[] Records = new int[8];
    public static int PrevRecordScore { get; set; }
    public static int PositionInListOfRecords;

    void Start()
    {
        PositionInListOfRecords = -2;
        EndGameFlag = true;

        for (int i = 0; i < Records.Length; ++i)
            if (PlayerPrefs.HasKey("rec" + i.ToString()))
                Records[i] = PlayerPrefs.GetInt("rec" + i.ToString());

        SortListOfRecords();
    }

    void SortListOfRecords()
    {
        for(int i = 0; i < Records.Length - 1; ++i)
            for(int j = i + 1; j < Records.Length; ++j)
                if (Records[i] < Records[j])
                {
                    var tmp = Records[i];
                    Records[i] = Records[j];
                    Records[j] = tmp;
                }
    }

    public void EndGameFoo()
    {
        Debug.Log("Конец игры!");
        EndGameFlag = false;
        PositionInListOfRecords = CheckNewScore();
        if (PositionInListOfRecords != -1)
        {
            SetNewRecord(PositionInListOfRecords);
            Debug.Log("Save new record");
            SaveNewListOfRecords();
        }
    }

    int CheckNewScore()
    {
        for (int i = 0; i < Records.Length; ++i)
        {
            if (Head.Score > Records[i])
                return i;
            else if (Head.Score == Records[i])
                return -1;
        }
        return -1;
    }

    void SetNewRecord(int pos)
    {
        PlayerPrefs.SetInt("NewScore", Head.Score);
        PlayerPrefs.Save();
        for (int i = Records.Length - 1; i > pos; --i)
            Records[i] = Records[i - 1];
        Records[pos] = PlayerPrefs.GetInt("NewScore");
    }

    void SaveNewListOfRecords()
    {
        for (int i = 0; i < Records.Length; ++i)
            if (Records[i] != 0)
            {
                PlayerPrefs.SetInt("rec" + i.ToString(), Records[i]);
                PlayerPrefs.Save();
            }
        for (int i = 0; i < Records.Length; ++i)
            if (Records[i] != 0)
                Records[i] = PlayerPrefs.GetInt("rec" + i.ToString());
    }
}

interface IEndGame
{
    void EndGameFoo();
}
