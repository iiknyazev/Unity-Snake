using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeSelection : MonoBehaviour
{
    //[SerializeField] private GameObject _btnClassicMode;
    //[SerializeField] private GameObject _btnCompanyMode;

    //private float _posBtnClassicMode;
    //private float _posBtnCompanyMode;


    //void Start()
    //{
    //    _posBtnClassicMode = _btnClassicMode.transform.position.y;
    //    _posBtnCompanyMode = _btnCompanyMode.transform.position.y;
    //}

    //void Update()
    //{
    //    if (_posBtnClassicMode != _btnClassicMode.transform.position.y)
    //    {
    //        SceneManager.LoadScene("ClassicMode");
    //        PlayerPrefs.SetInt("gameMode", 0);
    //        PlayerPrefs.Save();
    //        Debug.Log("gameMode = " + PlayerPrefs.GetInt("gameMode"));
    //    }
    //    else if (_posBtnCompanyMode != _btnCompanyMode.transform.position.y)
    //    {
    //        //SceneManager.LoadScene("CompanyMode");
    //        PlayerPrefs.SetInt("gameMode", 1);
    //        PlayerPrefs.Save();
    //        Debug.Log("gameMode = " + PlayerPrefs.GetInt("gameMode"));
    //        SceneManager.LoadScene("Level 1");
    //    }
    //}

    public void OnClickClassicMode()
    {
        SceneManager.LoadScene("ClassicMode");
        PlayerPrefs.SetInt("gameMode", 0);
        PlayerPrefs.Save();
        Debug.Log("gameMode = " + PlayerPrefs.GetInt("gameMode"));
    }

    public void OnClickCompanyMode()
    {
        PlayerPrefs.SetInt("gameMode", 1);
        PlayerPrefs.Save();
        Debug.Log("gameMode = " + PlayerPrefs.GetInt("gameMode"));
        SceneManager.LoadScene("Level 1");
    }
}