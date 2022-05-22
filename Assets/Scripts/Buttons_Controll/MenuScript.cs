using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject _btnStart;
    [SerializeField] private GameObject _btnGameMode;
    [SerializeField] private GameObject _btnOptions;
    [SerializeField] private GameObject _btnAboutUs;
    [SerializeField] private GameObject _btnRecords;
    [SerializeField] private GameObject _btnQuit;

    private float _posBtnStart;
    private float _posBtnGameMode;
    private float _posBtnOptions;
    private float _posBtnAboutUs;
    private float _posBtnRecords;
    private float _posBtnQuit;

    void Start()
    {
        _posBtnStart = _btnStart.transform.position.y;
        _posBtnGameMode = _btnGameMode.transform.position.y;
        _posBtnOptions = _btnOptions.transform.position.y;
        _posBtnAboutUs = _btnAboutUs.transform.position.y;
        _posBtnRecords = _btnRecords.transform.position.y;
        _posBtnQuit = _btnQuit.transform.position.y;
    }

    void Update()
    {
        if (_posBtnStart != _btnStart.transform.position.y)
        {
            if (PlayerPrefs.HasKey("gameMode"))
            {
                if (PlayerPrefs.GetInt("gameMode") == 1)
                    SceneManager.LoadScene("Level 1");
                else if (PlayerPrefs.GetInt("gameMode") == 0)
                {
                    if (PlayerPrefs.HasKey("activeScene"))
                        SceneManager.LoadScene(PlayerPrefs.GetString("activeScene"));
                    else
                        SceneManager.LoadScene("Level 1");
                }
            }
            else
            {
                SceneManager.LoadScene("Level 1");
                PlayerPrefs.SetInt("gameMode", 0);
                PlayerPrefs.Save();
            }
        }
        else if (_posBtnGameMode != _btnGameMode.transform.position.y)
            SceneManager.LoadScene("GameMode");
        else if (_posBtnOptions != _btnOptions.transform.position.y)
            SceneManager.LoadScene("Options");
        else if (_posBtnAboutUs != _btnAboutUs.transform.position.y)
            SceneManager.LoadScene("AboutUs");
        else if (_posBtnRecords != _btnRecords.transform.position.y)
            SceneManager.LoadScene("Records");
        else if (_posBtnQuit != _btnQuit.transform.position.y)
            Application.Quit();
    }
}