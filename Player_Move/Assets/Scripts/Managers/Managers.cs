using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    //클래스 자료형 변수는 같은 종류의 스크립트 컴포넌트를 갖고 있을 수 있다.
    static Managers s_instance=null;
    public static Managers Instance { get { Init(); return s_instance; } }


    InputManager _input=new InputManager();
    public static InputManager Input {  get { return Instance._input; } }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");

            if (go == null)
            {
                go = new GameObject() { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance=go.GetComponent<Managers>();
        }
    }
}
