using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FoodPositionChanger : MonoBehaviour, IFoodDestroyer
{
    [SerializeField] private Transform _leftUpBound;
    [SerializeField] private Transform _rightDownBound;

    private GameObject _checkTile;
    private int _activeMode;
    private float _sizeOfTile; // —тоит помнить, что _sizeOfTile должен быть меньше 1
    static public int _flagForBonusEaten { get; set; }

    void Start()
    {
        GeneratePosition();

        _sizeOfTile = Head.SizeOfTile;
        _activeMode = PlayerPrefs.GetInt("gameMode");
        _flagForBonusEaten = 0;
    }

    private void Update()
    {
        if (_activeMode == 1 && Head.CntSimpleFood >= 12)
            transform.position = new Vector2(10, 0);
    }

    public void DestroyFood()
    {
        _flagForBonusEaten++;
        GeneratePosition();
    }

    private const int tryCounts = 1000;

    protected void GeneratePosition()
    {
        for (int i = 0; i < tryCounts; i++)
        {
            // √енерируем очередную позицию так, чтобы она была по лини€м сетки тайлов, а не только в узлах координатной сетки ёнити
            var newPos = new Vector3(Random.Range((int)_leftUpBound.position.x,
                (int)_rightDownBound.position.x) + (_sizeOfTile * Random.Range(0, (int) (1/ _sizeOfTile))),
                Random.Range((int)_leftUpBound.position.y,
                (int)_rightDownBound.position.y) + (_sizeOfTile * Random.Range(0, (int) (1 / _sizeOfTile))));

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

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.gameObject.name != "Head")
    //        GeneratePosition();
    //}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Head")
            GeneratePosition();
    }
}

public interface IFoodDestroyer
{
    void DestroyFood();
}