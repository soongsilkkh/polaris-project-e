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
            KeyAction.Invoke();//�Լ�ü�̴� �� ����!


        if (MouseAction != null)
        {
            //���� Ŭ���� 0��
            if (Input.GetMouseButton(0))//�ٿ�
            {
                MouseAction.Invoke(Define.MouseEvent.Press);
                _mousePressed = true;
            }
            else//���콺�� ������ �������°� �ƴϹǷ� else
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
