using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene : BaseScene
{
    //LoginScene Start함수 없어도
    //BaseScene Start함수 실행되며 Init실행됨

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))//특정키 Q 누르면
        {
            //이미 구현돼있는 제공된 SceneManager를 활용
            //SceneManager.LoadScene("Game");
            //다음으로 가고 싶은 씬 이름 인자로

            //새롭게 만든 SceneManagerEx이용
            Managers.Scene.LoadScene(Define.Scene.Game);

        }
    }

    protected override void Init()
    {
        base.Init();

        //씬 타입 적절하게 배치
        SceneType = Define.Scene.Login;


        /*
         오브젝트 풀링 테스트 
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < 8; i++)
        {
            list.Add(Managers.Resource.Instantiate("unitychan"));
        }

        for(int i = 0;i <4; i++)
        {
            Managers.Resource.Destroy(list[i]);
        }
        */

        Debug.Log("ur now entering Login scene");
    }

    public override void Clear()
    {
        Debug.Log("Login Scene Clear");
    }

}
