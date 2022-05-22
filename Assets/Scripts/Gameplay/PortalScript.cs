using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour, IPortal
{
    [SerializeField] private Transform _leftUpBound;
    [SerializeField] private Transform _rightDownBound;

    private GameObject _checkTile;

    private float _sizeOfTile;
    private int _activeMode;
    private int _activePortal;

    void Start()
    {
        _activePortal = 0;
        _sizeOfTile = Head.SizeOfTile;
        _activeMode = PlayerPrefs.GetInt("gameMode");
        transform.position = new Vector2(6, 0);

        if (SceneManager.GetActiveScene().name == "Level 1")
            PlayerPrefs.SetInt("intermediateScore", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Head.CntSimpleFood >= 12 && _activeMode == 1)
        {
            _activePortal++;
            if (_activePortal == 1)
                GeneratePosition();
            _activePortal++;
        }
    }

    private const int tryCounts = 1000;

    protected void GeneratePosition()
    {
        for (int i = 0; i < tryCounts; i++)
        {
            // Генерируем очередную позицию так, чтобы она была по линиям сетки тайлов, а не только в узлах координатной сетки Юнити
            var newPos = new Vector3(Random.Range((int)_leftUpBound.position.x,
                (int)_rightDownBound.position.x) + (_sizeOfTile * Random.Range(0, (int)(1 / _sizeOfTile))),
                Random.Range((int)_leftUpBound.position.y,
                (int)_rightDownBound.position.y) + (_sizeOfTile * Random.Range(0, (int)(1 / _sizeOfTile))));

            if (!IsValidPosition(newPos))
                continue;

            transform.position = newPos;
            break;
        }
    }

    private bool IsValidPosition(Vector3 pos)
    {
        _checkTile = GameObject.Find("Head");
        if (_checkTile.transform.position == pos)
            return false;

        if (pos.x - _sizeOfTile / 2 >= _rightDownBound.transform.position.x)
            return false;
        else if (pos.x + _sizeOfTile / 2 <= _leftUpBound.transform.position.x)
            return false;
        else if (pos.y + _sizeOfTile / 2 <= _rightDownBound.transform.position.y)
            return false;
        else if (pos.y - _sizeOfTile / 2 >= _leftUpBound.transform.position.y)
            return false;

        return true;
    }

    public void Destroy()
    {
        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            PlayerPrefs.SetInt("intermediateScore", Head.Score);
            PlayerPrefs.Save();
        }
        else
        {
            int intermediateScore = PlayerPrefs.GetInt("intermediateScore");
            PlayerPrefs.SetInt("intermediateScore", intermediateScore + Head.Score);
            PlayerPrefs.Save();
        }

        string numNewScene = SceneManager.GetActiveScene().name;
        numNewScene = numNewScene.Substring(5);
        int InumNewScene = int.Parse(numNewScene);
        InumNewScene++;
        if (InumNewScene > 6)
            InumNewScene = 1;
        SceneManager.LoadScene("Level " + InumNewScene);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name != "Head")
            GeneratePosition();
    }
}

public interface IPortal
{
    void Destroy();
}
