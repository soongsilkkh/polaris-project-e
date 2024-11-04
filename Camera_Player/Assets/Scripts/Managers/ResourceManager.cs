using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf('/');//마지막 / 인덱스 반환

            if (index >= 0)
                name = name.Substring(index + 1);//이름 가공

            //가공된 이름으로 오리지널 가져오기
            GameObject go = Managers.Pool.GetOriginal(name);
            if (go != null)//있다면 바로 반환
                return go as T;
        }

        //없다면 예정대로 로드하여 반환
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    //원래 Instantiate함수의 인자를 따름.
    {
        GameObject original = this.Load<GameObject>($"Prefabs/{path}");
        if (original == null)
        {//없다면
            Debug.Log("fail to load prefab");
            return null;
        }

        //Load함수에서 이미 풀링된 것을 가져왔다면
        //이미 객체화(instantiate(pool클래스의 create함수를 통해서)
        //돼 있는데 또 객체화?
        //이미 Load함수에서 확인했지만 또 확인
        //create함수에서는 Object.Instantiate<GameObject>(Original);이고
        //여기서는 Object.Instantiate(original,parent);이다.

        //우선 뽑은(어떻게 뽑힌지는 상관 X)
        //original이 poolable컴포넌트를 갖고 있는지 확인
        if (original.GetComponent<Poolable>() != null)
            //갖고 있으면 풀에서 뽑은거니까
            return Managers.Pool.Pop(original, parent).gameObject;
        //바로 꺼내서 반환

        //풀링하는 대상이 아니라면 원래대로 (카메라나 잘 안쓰는 UI,,,)
        GameObject go = Object.Instantiate(original, parent);

        go.name = original.name;

        return go;

        //원형이 앞에 Object. 를 붙임. 무한재귀 방지.
        //return Object.Instantiate(prefab, parent);
    }

    public void Destroy(GameObject go)
    {
        if (go == null) return;

        //풀링 대상인지 확인 == poolable 컴포넌트를 갖고 있는지 확인
        Poolable poolable = go.GetComponent<Poolable>();

        if (poolable != null)//갖고 있다면
        {
            Managers.Pool.Push(poolable);
            return;
            //바로 다시 풀에 푸쉬하고 반환
        }

        Object.Destroy(go);
    }

    public void Clear()
    {

    }
}
