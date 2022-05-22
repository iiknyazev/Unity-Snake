using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnBackScript : MonoBehaviour
{
    [SerializeField] private GameObject _btnBack;

    private float _posBtnBack;

    void Start()
    {
        _posBtnBack = _btnBack.transform.position.y;
    }

    void Update()
    {
        if (_posBtnBack != _btnBack.transform.position.y)
            SceneManager.LoadScene("Menu");
    }
}
