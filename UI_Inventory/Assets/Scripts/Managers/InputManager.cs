using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    public Action KeyAction = null;
    public Action<Define.MouseEvent> MouseAction = null;

    bool _mousePressed = false;


    public void OnUpdate()
    {

        if (EventSystem.current.IsPointerOverGameObject())
            return;


        if (Input.anyKey != false && KeyAction != null)
            KeyAction.Invoke();//함수체이닝 다 실행!


        if (MouseAction != null)
        {
            //왼쪽 클릭이 0번
            if (Input.GetMouseButton(0))//꾸욱
            {
                MouseAction.Invoke(Define.MouseEvent.Press);
                _mousePressed = true;
            }
            else//마우스가 떼지면 눌린상태가 아니므로 else
            {
                if (_mousePressed)
                    MouseAction.Invoke(Define.MouseEvent.Click);
                _mousePressed = false;
            }

        }

    }


    public void Clear()
    {
        KeyAction = null;
        MouseAction = null;
    }
}
