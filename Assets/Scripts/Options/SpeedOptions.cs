using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedOptions : MonoBehaviour
{
    [SerializeField] private Slider _speedSlider;
    [SerializeField] private Text _speedValue;

    [SerializeField] private GameObject _btnLess;
    [SerializeField] private GameObject _btnMore;

    private float _delay;
    private float _stepOfDelay;
    private float _maxDelay;
    private float _minDelay;

    void Start()
    {
        _stepOfDelay = 0.025f;
        _maxDelay = 0.2f;
        _minDelay = 0.05f;
    }

    void FixedUpdate()
    {
        //Debug.Log("Delay = " + _delay);
        if (PlayerPrefs.HasKey("newDelay"))
        {
            _speedValue.text = PlayerPrefs.GetFloat("newDelay").ToString("F2");
            _delay = PlayerPrefs.GetFloat("newDelay");
        }
        else
        {
            _delay = _stepOfDelay;
            _speedValue.text = _delay.ToString("F2");
        }
        _speedSlider.value = _maxDelay - _delay + _minDelay;
    }

    public void OnClickLess()
    {
        if (_delay < _maxDelay)
        {
            PlayerPrefs.SetFloat("newDelay", _delay + _stepOfDelay);
            PlayerPrefs.Save();
        }
    }

    public void OnClickMore()
    {
        if (_delay > _minDelay + 0.001f)
        {
            PlayerPrefs.SetFloat("newDelay", _delay - _stepOfDelay);
            PlayerPrefs.Save();
        }
    }
}
