using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
    //상속받아서 사용하는 클래스이기에
{
    //현재 어떤 씬(화면)인지 나타낼
    //상태 변수 선언 & 초기화
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;
    //_sceneType 변수에 대한
    //접근이 많을 것이기에 프로퍼티로


    void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        //EventSystem이라는 오브젝트가 있는지 확인
        Object obj=GameObject.FindObjectOfType(typeof(EventSystem));
        //인자로 들어간 타입이 하나라도 있는지 확인하는 함수
        //찾는 건 GameObject 타입이지만
        //함수 반환 형식이 상위 클래스인 Object타입

        if (obj == null)//찾는 타입이 하나도 없다면~
        {
            /*
            //리소스 매니저를 통해서 객체화
            GameObject go = Managers.Resource.Instantiate("UI/EventSystem");
            go.name = "@EventSystem";//이름 변경
            Debug.Log("EventSystem builded");
            */
        }


    }

    //씬이 끝났을때 호출되는 정리 함수 
    public abstract void Clear();
    //지금 현재 이클래스에서 
    //정의를 할 필요 없기에
}
