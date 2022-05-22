using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSlider : MonoBehaviour
{
    [SerializeField] private Slider _scoreSlider;

    private int _gameMode;

    void Start()
    {
        if (PlayerPrefs.HasKey("gameMode"))
            _gameMode = PlayerPrefs.GetInt("gameMode");
        else
            _gameMode = 0;
        if (_gameMode == 1)
            _scoreSlider.transform.position = new Vector2(1, 4.8f);
        else
            _scoreSlider.transform.position = new Vector2(5, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameMode == 1)
            _scoreSlider.value = Head.CntSimpleFood;
    }
}
