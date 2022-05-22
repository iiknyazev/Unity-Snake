using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Head : MonoBehaviour
{
    // ѕредел точности: 0.01
    public static float Delay { get; set; }
    // _delay должен быть меньше или равен Delay

    // _tick равна половине значени€ Time.deltaTime в FixedUpdate()
    private float _tick = 0.01f;
    private float _timer;
    private float _checkTime;
    public static float TimerForBonusFood { get; set; }
   
    public static int DirX { get; set; }
    public static int DirY { get; set; }
    private float _x;
    private float _y;

    public static int LenghtOfSnake { get; set; }
    public static int CntSteps { get; set; }
    public static float SizeOfTile { get; set; }
    public static int Score { get; set; }
    public static int CntSimpleFood { get; set; }

    [SerializeField] private KeyCode _leftKeyCode;
    [SerializeField] private KeyCode _rightKeyCode;
    [SerializeField] private KeyCode _downKeyCode;
    [SerializeField] private KeyCode _forwardKeyCode;

    [SerializeField] private Transform _leftUpBound;
    [SerializeField] private Transform _rightDownBound;

    [SerializeField] private GameObject _newTile;
    [SerializeField] private GameObject _controller;

    private GameObject _tile;
    private float _distansController;
    private Vector3 _lastPositionNextTile;

    public static bool CanDoAction { get; set; }
    public static string ActiveScene { get; set; }

    void Start()
    {
        if (PlayerPrefs.HasKey("newDelay"))
            Delay = PlayerPrefs.GetFloat("newDelay");
        else
            Delay = 0.05f;

        Debug.Log("Delay = " + Delay);

        DirX = 1;
        DirY = 0;

        _x = transform.position.x;
        _y = transform.position.y;

        _timer = Delay;
        TimerForBonusFood = 0;

        SizeOfTile = GetComponent<Transform>().localScale.x;
         _distansController = (SizeOfTile * 2 / 8) + ((SizeOfTile * 2 / 9) - (SizeOfTile * 2 / 8)) / 2;
        
        Score = 0;
        LenghtOfSnake = 3;
        CntSteps = 0;
        CntSimpleFood = 0;

        if (PlayerPrefs.GetInt("gameMode") == 1 && SceneManager.GetActiveScene().name != "Level 1")
            Score += PlayerPrefs.GetInt("intermediateScore");

        ActiveScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("activeScene", ActiveScene);
        PlayerPrefs.Save();

        CanDoAction = true;
    }

    void Update()
    {
        CheckKeyboard();

        //Debug.Log(_canDoAction);
    }

    void FixedUpdate()
    {
        // Time.deltaTime = 0.02 (т.е. это перерыв между вызовами FixedUpdate)
        // ¬ычита€ из Time.deltaTime значение _tick, мы получаем возможность ставить любой Delay до его предела точности
        _timer -= Time.deltaTime - _tick;
        //Debug.Log(_timer.ToString("F2"));

        // chechTime сохран€ет врем€ отсека€ все дальше второго знака после зап€той
        _checkTime = float.Parse(_timer.ToString("F2"));

        if (_checkTime == 0)
        {
            _lastPositionNextTile = GetComponent<Transform>().position;
            StepSnakeHead();
            StepTilesOfTail();
            CanDoAction = true;
            _timer = Delay;
            CntSteps++;
        }
    }

    void StepSnakeHead()
    {
        var newPos = new Vector2(_x + DirX * SizeOfTile, _y + DirY * SizeOfTile);

        int flagNewDirection = CheckNewDirection(newPos);

        switch (flagNewDirection)
        {
            case 0:
                transform.position = newPos;
                break;
            case 1:
                transform.position = new Vector2(_leftUpBound.transform.position.x, newPos.y);
                break;
            case 2:
                transform.position = new Vector2(_rightDownBound.transform.position.x, newPos.y);
                break;
            case 3:
                transform.position = new Vector2(newPos.x, _leftUpBound.transform.position.y);
                break;
            case 4:
                transform.position = new Vector2(newPos.x, _rightDownBound.transform.position.y);
                break;
            default: return;
        }

        SetNewControllersPosition();

        _x = transform.position.x;
        _y = transform.position.y;
    }

    void SetNewControllersPosition()
    {
        if (DirX == 1)
            _controller.transform.position = new Vector2(transform.position.x + _distansController, transform.position.y);
        else if (DirX == -1)
            _controller.transform.position = new Vector2(transform.position.x - _distansController, transform.position.y);
        else if (DirY == 1)
            _controller.transform.position = new Vector2(transform.position.x, transform.position.y + _distansController);
        else if (DirY == -1)
            _controller.transform.position = new Vector2(transform.position.x, transform.position.y - _distansController);
    }

    int CheckNewDirection(Vector2 pos)
    {
        if (pos.x - SizeOfTile / 2 >= _rightDownBound.transform.position.x)
            return 1;
        else if (pos.x + SizeOfTile / 2 <= _leftUpBound.transform.position.x)
            return 2;
        else if (pos.y + SizeOfTile / 2 <= _rightDownBound.transform.position.y)
            return 3;
        else if (pos.y - SizeOfTile / 2 >= _leftUpBound.transform.position.y)
            return 4;
        else
            return 0;
    }

    void StepTilesOfTail()
    {
        int number = 1;
        _tile = GameObject.Find("Tail (" + number.ToString() + ")(Clone)");
        while (_tile != null)// пока не дошли до последнего тайла
        {
            // запоминаем старую позицию данного тайла во временную переменную
            var temp = _tile.GetComponent<Transform>().position;
            // данный тайл смещаетс€ на предыдущую позицию тайла перед ним
            _tile.transform.position = _lastPositionNextTile;
            // запоминаем старую позицию данного тайла
            _lastPositionNextTile = temp;
            // переходим на следующий тайл
            number++;
            // находим очередной хвостовой тайл
            _tile = GameObject.Find("Tail (" + number.ToString() + ")(Clone)");
        }
    }

    void CheckKeyboard()
    {
        if (Input.GetKeyDown(_forwardKeyCode) && DirY != -1 && CanDoAction)
        {
            DirX = 0;
            DirY = 1;
            CanDoAction = false;
        }
        else if (Input.GetKeyDown(_downKeyCode) && DirY != 1 && CanDoAction)
        {
            DirX = 0;
            DirY = -1;
            CanDoAction = false;
        }
        else if (Input.GetKeyDown(_rightKeyCode) && DirX != -1 && CanDoAction)
        {
            DirX = 1;
            DirY = 0;
            CanDoAction = false;
        }
        else if (Input.GetKeyDown(_leftKeyCode) && DirX != 1 && CanDoAction)
        {
            DirX = -1;
            DirY = 0;
            CanDoAction = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        var eat = col.GetComponent<IFoodDestroyer>();
        if (eat != null)
        {
            Score += 7;
            LenghtOfSnake++;
            CntSimpleFood++;
            Debug.Log("CntSimpleFood = " + CntSimpleFood);

            GenerateNewTile();
            eat.DestroyFood();
        }

        var bonusEat = col.GetComponent<IBonusFoodDestroyer>();
        if (bonusEat != null)
        {
            float lifetimeBonusFood = (Time.realtimeSinceStartup - TimerForBonusFood);
            //Debug.Log("lifetimeBonusFood = " + lifetimeBonusFood);
            float bonusScore = (1 - lifetimeBonusFood / (Delay * BonusFoodScript._delayBonusFood)) * 100;
            //Debug.Log("bonusScore = " + bonusScore);
            Score += (int)bonusScore;
            bonusEat.DestroyBonusFood();
        }

        var portal = col.GetComponent<IPortal>();
        if (portal != null)
        {
            portal.Destroy();
        }
    }

    void GenerateNewTile()
    {
        _newTile.name = "Tail (" + LenghtOfSnake.ToString() + ")";
        //Instantiate(_newTile, GetComponent<Transform>().position, Quaternion.identity);
        Instantiate(_newTile, GameObject.Find("Tail (1)(Clone)").transform.position, Quaternion.identity);
    }
}