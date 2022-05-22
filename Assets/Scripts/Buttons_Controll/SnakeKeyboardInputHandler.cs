using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeKeyboardInputHandler : MonoBehaviour
{
    void Update()
    {
        if (KeyboardButtonScript._holdBtnRight)
            OnClickRightBtn();
        else if (KeyboardButtonScript._holdBtnLeft)
            OnClickLeftBtn();
        else if (KeyboardButtonScript._holdBtnUp)
            OnClickUpBtn();
        else if (KeyboardButtonScript._holdBtnDown)
            OnClickDownBtn();
    }

    public void OnClickDownBtn()
    {
        if (Head.DirY != 1 && Head.CanDoAction)
        { 
            Head.DirX = 0;
            Head.DirY = -1;
            Head.CanDoAction = false;
        }
    }

    public void OnClickUpBtn()
    {
        if (Head.DirY != -1 && Head.CanDoAction)
        { 
            Head.DirX = 0;
            Head.DirY = 1;
            Head.CanDoAction = false;
        }
    }

    public void OnClickLeftBtn()
    {
        if (Head.DirX != 1 && Head.CanDoAction)
        {
            Head.DirX = -1;
            Head.DirY = 0;
            Head.CanDoAction = false;
        }
    }

    public void OnClickRightBtn()
    {
        if (Head.DirX != -1 && Head.CanDoAction)
        {
            Head.DirX = 1;
            Head.DirY = 0;
            Head.CanDoAction = false;
        } 
    }
}
