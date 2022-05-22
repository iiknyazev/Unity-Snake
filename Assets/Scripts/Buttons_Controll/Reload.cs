using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour
{
    [SerializeField] private GameObject _btnReload;

    private float _posBtnReload;

    void Start()
    {
        _posBtnReload = _btnReload.transform.position.y;
    }

    void Update()
    {
        if (_posBtnReload != _btnReload.transform.position.y)
        {
            if (PlayerPrefs.GetInt("gameMode") == 0)
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            else if (PlayerPrefs.GetInt("gameMode") == 1)
                SceneManager.LoadScene("Level 1");
        }
    }
}
