using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    //현재 씬에 접근하기 위한 프로퍼티 작성
    public BaseScene CurrentScene
    { get { return GameObject.FindObjectOfType<BaseScene>(); } }
    //전에 봤던 FindObjecOfType함수는 그냥 Object타입으로 반환.
    //이건 제네릭에 넘겨준 타입으로 변환까지하고 반환.
    //지금 현재 씬에서 BaseScene 클래스나 상속받은 하위 클래스
    //의 객체를 반환.


    //원래 함수에서는 문자열을 받았지만
    //현재 우리는 타입으로 어떤 씬인지 구별하고 있으니
    //Define Scene 타입으로 인자를 받자.
    //문자열로 하드코딩이 아닌
    //Define에서 정의된 애들로만 되게끔
    //선택지를 줄여줌.
    public void LoadScene(Define.Scene type)
    //Define에서 정의된 씬 타입들의 이름과
    //실제 씬들의 이름이 같아야겠지?
    {
        //다음 씬을 새로 로드하기전
        //지금 씬에 대한 리소스를 정리해야겠죠?
        //그래서 Clear라는 함수를 정의한거고.

        //먼저 현재 씬에 접근하자.
        //아까 정의한 프로퍼티 사용
        //CurrentScene.Clear();
        //현재 씬의 @Scene 게임오브젝트의
        //현재 씬 관련 스크립트 클래스 객체를 반환.
        //현재 씬 관련 스크립트의 Clear함수 호출.

        //씬이 이동하면 다 클리어해야하므로
        Managers.Clear();

        //아래 만든 함수 활용.
        string name = GetSceneName(type);

        //기존의 제공된 SceneManager를 활용
        SceneManager.LoadScene(name);
    }

    string GetSceneName(Define.Scene type)
    {//c++에서 만들면 귀찮은 기능이지만
     //c#은 리플렉션 시스템이 있어서 어느정도 추출가능

        string name = System.Enum.GetName(typeof(Define.Scene), type);
        //타입의 종류와 어떤 내용인지를 넘겨주면
        //해당되는 문자열을 반환함.

        return name;
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }
}
