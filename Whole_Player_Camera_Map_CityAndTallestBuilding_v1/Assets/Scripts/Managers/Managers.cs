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

    //이동 관련해서는 Fixed Update가 낫다더라,,,
    //대부분의 키가 이동관련이니 적용해봄
    //사이드 collision시 파다닥 거리는 이슈 해결됨.
    //동시에 눌리는 부분에 취약하닌 Space(점프)는 Update에서
    private void FixedUpdate()
    {
        _input.OnMoveUpdate();
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
