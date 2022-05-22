using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private GameObject _btnLevel1;
    [SerializeField] private GameObject _btnLevel2;
    [SerializeField] private GameObject _btnLevel3;
    [SerializeField] private GameObject _btnLevel4;
    [SerializeField] private GameObject _btnLevel5;
    [SerializeField] private GameObject _btnLevel6;

    private float _posBtnLevel1;
    private float _posBtnLevel2;
    private float _posBtnLevel3;
    private float _posBtnLevel4;
    private float _posBtnLevel5;
    private float _posBtnLevel6;

    void Start()
    {
        _posBtnLevel1 = _btnLevel1.transform.position.y;
        _posBtnLevel2 = _btnLevel2.transform.position.y;
        _posBtnLevel3 = _btnLevel3.transform.position.y;
        _posBtnLevel4 = _btnLevel4.transform.position.y;
        _posBtnLevel5 = _btnLevel5.transform.position.y;
        _posBtnLevel6 = _btnLevel6.transform.position.y;
    }

    void Update()
    {
        if (_posBtnLevel1 != _btnLevel1.transform.position.y)
            SceneManager.LoadScene("Level 1");
        else if (_posBtnLevel2 != _btnLevel2.transform.position.y)
            SceneManager.LoadScene("Level 2");
        else if (_posBtnLevel3 != _btnLevel3.transform.position.y)
            SceneManager.LoadScene("Level 3");
        else if (_posBtnLevel4 != _btnLevel4.transform.position.y)
            SceneManager.LoadScene("Level 4");
        else if (_posBtnLevel5 != _btnLevel5.transform.position.y)
            SceneManager.LoadScene("Level 5");
        else if (_posBtnLevel6 != _btnLevel6.transform.position.y)
            SceneManager.LoadScene("Level 6");
    }
}
