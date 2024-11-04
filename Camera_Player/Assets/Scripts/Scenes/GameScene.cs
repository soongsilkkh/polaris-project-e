using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
    //BaseScene 클래스 상속받아서 사용
{

    protected override void Init() 
    {
        base.Init();

        //BaseScene클래스에 있는 SceneType변수 알맞게 초기화
        SceneType = Define.Scene.Game;

        Debug.Log("ur now entering Game scene");

        //플레이어 컨트롤러의 초기화 부분에
        //있던 GameScene관련 초기화 코드들
        //Managers.UI.ShowSceneUI<UI_Inven>("UI_Inven");

        /*
         오브젝트 풀링 테스트
        Managers.Resource.Instantiate("unitychan");
        Managers.Resource.Instantiate("unitychan");
        */


        /*
        데이터 매니저 예제 코드
        
        Dictionary<int,Stat> dict=Managers.Data.StatDict;
        */

    }

    public override void Clear()
    {

    }

    //상위 클래스인 BaseScene클래스에서
    //Start(Awake)함수가 Init함수(가상함수)를 호출하므로
    //GameScene의 Start함수가 없어도 
    //GameScene의 Init함수(가상함수)가 잘 호출된다.
    void Start()
    {
        Init();
    }

}
