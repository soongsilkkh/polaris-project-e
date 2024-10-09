using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    { return Resources.Load<T>(path); }



    public GameObject Instantiate(string path, Transform parent = null)
    //원래 Instantiate함수의 인자를 따름.
    {
        GameObject prefab = this.Load<GameObject>($"Prefabs/{path}");
        if (prefab == null)
        {//없다면
            Debug.Log("fail to load prefab");
            return null;
        }

        GameObject go = Object.Instantiate(prefab, parent);

        int index = go.name.IndexOf("(Clone)");
        if (index > 0)
            go.name = go.name.Substring(0, index);

        return go;

    }

    public void Destroy(GameObject go)
    {
        if (go == null) return;
        Object.Destroy(go);
    }

    public void Clear()
    {

    }
}
