using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KeyboardButtonScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static bool _holdBtnRight { get; set; }
    public static bool _holdBtnLeft { get; set; }
    public static bool _holdBtnUp { get; set; }
    public static bool _holdBtnDown { get; set; }

    void Start()
    {
        _holdBtnRight = false;
        _holdBtnLeft = false;
        _holdBtnUp = false;
        _holdBtnDown = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + 0.09f);
        if (gameObject.tag == "Right")
            _holdBtnRight = true;
        else if (gameObject.tag == "Left")
            _holdBtnLeft = true;
        else if (gameObject.tag == "Up")
            _holdBtnUp = true;
        else if (gameObject.tag == "Down")
            _holdBtnDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - 0.09f);
        if (gameObject.tag == "Right")
            _holdBtnRight = false;
        else if (gameObject.tag == "Left")
            _holdBtnLeft = false;
        else if (gameObject.tag == "Up")
            _holdBtnUp = false;
        else if (gameObject.tag == "Down")
            _holdBtnDown = false;
    }
}