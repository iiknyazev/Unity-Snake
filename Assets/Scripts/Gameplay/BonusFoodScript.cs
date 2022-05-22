using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusFoodScript : MonoBehaviour, IBonusFoodDestroyer
{
    private float _sizeOfTile;
    
    private Coroutine _lifetimeCoroutine;
    private Coroutine _timerBonusFoodCoroutine;

    [SerializeField] private Transform _leftUpBound;
    [SerializeField] private Transform _rightDownBound;

    [SerializeField] private Slider _bonusFoodSlider;

    [SerializeField] private Text _timerBonusFood;
    private GameObject _checkTile;

    public static int _delayBonusFood;
    private int _secondsLeftBeforeDisappearing;
    private int _allSecondsForBonusFood;
    private Vector3 _defaultPosition;
    private bool _flagOnBonusFoodExist;

    void Start()
    {
        _allSecondsForBonusFood = 20;
        _delayBonusFood = 75;
        _sizeOfTile = Head.SizeOfTile;
        _defaultPosition = new Vector2(5, 0);
        transform.position = _defaultPosition;
        _flagOnBonusFoodExist = false;
    }

    void Update()
    {
        if (FoodPositionChanger._flagForBonusEaten == 5)
        {
            _secondsLeftBeforeDisappearing = (int)(Head.Delay * _delayBonusFood) * 2;
            _secondsLeftBeforeDisappearing = _allSecondsForBonusFood;
            _lifetimeCoroutine = StartCoroutine(Lifetime());
            _flagOnBonusFoodExist = true;
            RefreshTimerBonusFood();
            FoodPositionChanger._flagForBonusEaten = 0;
        }
        // Исправляем баг: Иногда бонусная еда после съедения не пропадает,
        // а перемещается на новую клетку игрового поля
        else if(transform.position != _defaultPosition && !_flagOnBonusFoodExist)
            transform.position = _defaultPosition;
    }

    private const int tryCounts = 1000;

    private void GeneratePosition()
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

    public void DestroyBonusFood()
    {
        //_BFslider.GetComponent<RectTransform>().position = new Vector2(1100, 642);
        _bonusFoodSlider.transform.position = new Vector2(10, 4.4f);
        StopCoroutine(_lifetimeCoroutine);
        StopCoroutine(_timerBonusFoodCoroutine);
        _timerBonusFood.text = "";
        _flagOnBonusFoodExist = false;
        transform.position = _defaultPosition;
    }

    public IEnumerator Lifetime()
    {
        GeneratePosition();

        Head.TimerForBonusFood = Time.realtimeSinceStartup;

        //Debug.Log(" Head._timerForBonusFood = " + Head.TimerForBonusFood);
        //Debug.Log("Head.Delay = " + Head.Delay);

        yield return new WaitForSeconds(Head.Delay * _delayBonusFood);

        //Debug.Log("momentOfDisappearance = " + (Time.realtimeSinceStartup - Head.TimerForBonusFood).ToString());
        //transform.position = new Vector2(5, 0);
        DestroyBonusFood();
    }

    IEnumerator TimerBonusFood()
    {
        _timerBonusFood.text = _secondsLeftBeforeDisappearing.ToString();
        _bonusFoodSlider.value = _secondsLeftBeforeDisappearing;

        yield return new WaitForSeconds((Head.Delay * _delayBonusFood) / 20);

        _secondsLeftBeforeDisappearing--;
        RefreshTimerBonusFood();
    }

    void RefreshTimerBonusFood()
    {
        //_BFslider.GetComponent<RectTransform>().position = new Vector2(164, 642);
        _bonusFoodSlider.transform.position = new Vector2(1, 4.4f);
        _timerBonusFoodCoroutine = StartCoroutine(TimerBonusFood());
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.name != "Head"  && collision.gameObject.name != "SimpleFood")
    //    {
    //        //Debug.Log(transform.position.x.ToString("F2"));
    //        //Debug.Log(transform.position.y.ToString("F2"));
    //        //Debug.Log(collision.gameObject.name);
    //        GeneratePosition();
    //    }
    //}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Head" && collision.gameObject.name != "SimpleFood")
            GeneratePosition();
    }
}

public interface IBonusFoodDestroyer
{
    void DestroyBonusFood();

    IEnumerator Lifetime();
}