using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();

        if (component == null)
            component = go.AddComponent<T>();

        return component;
    }

    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);

        if (transform == null)
            return null;

        return transform.gameObject;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false)
        where T : UnityEngine.Object
    {
        if (go == null) return null;

        if (recursive == false)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (transform == null) continue;

                if (String.IsNullOrEmpty(name) || transform.name == name)
                {
                    T child = transform.GetComponent<T>();

                    if (child != null) return child;
                }

            }
        }
        else
        {
            foreach (T child in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || child.name == name)
                    return child;
            }
        }
        return null;
    }

}


public class TriggerPass
{
    private float _prevX;
    private float _nowX;
    private float _standard;

    private bool _isBegined;
    private bool _isEnded;

    public TriggerPass(float standard)
    {
        _standard = standard>0?standard:-standard;
        _prevX = 0;
        _nowX = 0;
        _isBegined = false;
        _isEnded = false;
    }

    public void Begin(float otherPosX) { _prevX = otherPosX; _isBegined = true; }
    public void End(float otherPosX) { _nowX = otherPosX; _isEnded = true; }

    public bool IsPass()
    {
        if (_isBegined && _isEnded)
        {
            bool result = false;

            _isBegined=false;
            _isEnded=false;

            if(result=(_prevX - _nowX >= _standard) || (_prevX - _nowX <= -_standard))
                Debug.Log($"_prevX {_prevX} nowX {_nowX} standard {_standard} isThrough");

            return result;
        }
        else
            return false;
    }
}