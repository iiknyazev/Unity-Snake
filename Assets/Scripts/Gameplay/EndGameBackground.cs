using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameBackground : MonoBehaviour
{
    [SerializeField] private GameObject _btnRight;
    [SerializeField] private GameObject _btnLeft;
    [SerializeField] private GameObject _btnUp;
    [SerializeField] private GameObject _btnDown;
    [SerializeField] private GameObject _btnPause;

    [SerializeField] private GameObject _gameOver;
    [SerializeField] private Text _newRecord;

    void Update()
    {
        if (!EndGame.EndGameFlag)
        {
            Destroy(_btnRight);
            Destroy(_btnLeft);
            Destroy(_btnUp);
            Destroy(_btnDown);
            Destroy(_btnPause);
            _gameOver.transform.position = new Vector2(0, 0);
        }
        if (EndGame.PositionInListOfRecords != -1)
            _newRecord.text = "Game Over\nNew Score: " + Head.Score;
        else 
            _newRecord.text = "Game Over\nScore: " + Head.Score;
    }
}
