using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private GameObject _score;

    void Update()
    {
        _score.GetComponent<Text>().text = "Score: " + (Head.Score).ToString();
        //Debug.Log(_head.Score);
    }
}
