using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public Action KeyAction = null;
    public Action MoveKeyAction = null;

    public void OnUpdate()
    {
        if (KeyAction != null)
            KeyAction.Invoke();

    }

    public void OnMoveUpdate()
    {
        if(MoveKeyAction != null) 
            MoveKeyAction.Invoke();
    }
}
