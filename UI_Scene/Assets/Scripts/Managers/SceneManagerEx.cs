using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

    public void LoadScene(Define.Scene type)
    {
        Managers.Clear();

        string name = GetSceneName(type);

        SceneManager.LoadScene(name);
    }


    string GetSceneName(Define.Scene type)
    {
        string name=Enum.GetName(typeof(Define.Scene), type);

        return name;
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }
}
