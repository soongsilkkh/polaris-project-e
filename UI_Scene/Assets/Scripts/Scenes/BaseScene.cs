using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;

    private void Start()
    {
        Init();
    }

    protected virtual void Init()
    { 
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));

        if (obj == null)
        {
            GameObject go = Managers.Resource.Instantiate("UI/EventSystem");
            go.name = "@EventSystem";
            Debug.Log("EventSystem Generated");
        }
    }

    public abstract void Clear();
}
