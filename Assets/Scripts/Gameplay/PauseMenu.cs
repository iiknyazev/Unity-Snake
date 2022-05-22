using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _btnRight;
    [SerializeField] private GameObject _btnLeft;
    [SerializeField] private GameObject _btnUp;
    [SerializeField] private GameObject _btnDown;
    [SerializeField] private GameObject _btnPause;
    [SerializeField] private GameObject _pauseMenu;

    private Vector2 _posBtnRight;
    private Vector2 _posBtnLeft;
    private Vector2 _posBtnUp;
    private Vector2 _posBtnDown;

    public static bool GameIsPause = false;

    void Start()
    {
        _posBtnRight = _btnRight.transform.position;
        _posBtnLeft = _btnLeft.transform.position;
        _posBtnUp = _btnUp.transform.position;
        _posBtnDown = _btnDown.transform.position;
    }

    public void OnClickPause()
    {
        GameIsPause = !GameIsPause;

        if (GameIsPause)
        {
            Time.timeScale = 0f;
            _btnRight.transform.position = new Vector2(-2876, 0);
            _btnLeft.transform.position = new Vector2(-2876, 0);
            _btnUp.transform.position = new Vector2(-2876, 0);
            _btnDown.transform.position = new Vector2(-2876, 0);
            _pauseMenu.transform.position = new Vector2(0, 0);
            _btnPause.GetComponentInChildren<Text>().text = "Play";
        }
        else
        {
            Time.timeScale = 1f;
            _btnRight.transform.position = _posBtnRight;
            _btnLeft.transform.position = _posBtnLeft;
            _btnUp.transform.position = _posBtnUp;
            _btnDown.transform.position = _posBtnDown;
            _pauseMenu.transform.position = new Vector2(-2876, 0);
            _btnPause.GetComponentInChildren<Text>().text = "Pause";
        }
    }
}
