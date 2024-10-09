using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    int _order = 100;

    //UI_Scene _sceneUI = null;
    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    //UI_Player _playerUI=null;

    //함수가 아닌 프로퍼티
    public GameObject UI_root
    {
        get
        {
            GameObject ui_root = GameObject.Find("@UI_Root");//소속시킬 부모를 찾거나 만들거나
            if (ui_root == null)
            {
                ui_root = new GameObject { name = "@UI_Root" };
            }
            return ui_root;
        }
    }

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);

        //뽑은 캔버스 컴포넌트의 내용들을 설정하자.
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        //전에는 툴에서 직접 일일이 세팅했지만 코드로.

        canvas.overrideSorting = true;
        //캔버스가 중첩된 상태에서 부모 캔버스가 어떤 값을 가지던 본인의 소팅오더를 가질 수 있게 하는 옵션.

        //sort 적용한다는 건 세팅할 UI가 팝업이라는 뜻
        if (sort)
            canvas.sortingOrder = _order++;
        else // 팝업이 아닌 그냥 UI일때
        { canvas.sortingOrder = 0; }

    }

    

    public T GeneratePopupUI<T>(string name = null)
   where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;//T를 name으로 사용.

        //prefab폴더에 저장되어 있는 애(name)를 객체화시키고
        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");


        T popup = Util.GetOrAddComponent<T>(go);


        _popupStack.Push(popup);


        //프로퍼티 활용.
        go.transform.SetParent(UI_root.transform);

        return popup;
    }

    //이제 팝업을 닫는 작업하는 함수도 만들자
    public void PopPopupUI()
    {
        if (_popupStack.Count == 0)
            return;

        UI_Popup popup = _popupStack.Pop();
        //스택에서 추출하고
        Managers.Resource.Destroy(popup.gameObject);
        //추출한거 삭제. 씬에서, hierarchy에서

        //UI_Popup과 연관된 스크립트 컴포넌트를 갖고 있는 주인(게임 오브젝트)를 삭제시키는 동작.

        popup = null;//접근 방지 보장
    }

    //지우려는 팝업 UI 확인까지하는 함수
    public void PopPopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
            return;

        if (_popupStack.Peek() != popup) return;

        PopPopupUI();

    }

    //모두 지우는 거
    public void PopAllPopupUI()
    {
        while (_popupStack.Count > 0) PopPopupUI();
    }

    #region later
    /*
    public T GenerateSceneUI<T>(string name = null)
where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");

        T sceneUI = Util.GetOrAddComponent<T>(go);

        _sceneUI = sceneUI;//스택말고 그냥 변수에 저장.

        //프로퍼티 활용.
        go.transform.SetParent(UI_root.transform);

        return sceneUI;
    }

    public void DestroySceneUI()
    {
        if (_sceneUI==null)
            return;

        
        Managers.Resource.Destroy(_sceneUI.gameObject);
        //추출한거 삭제. 씬에서, hierarchy에서

        _sceneUI = null;//접근 방지 보장
    }

    */
    #endregion




    public T MakeSubItem<T>(Transform parent = null, string name = null) where T : UI_Base
    {//T는 스크립트, name은 객체화할 프리팹 이름

        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/SubItem/{name}");
        //경로를 통해 객체화 하고

        //부모 연결
        if (parent != null)
            go.transform.SetParent(parent);

        return Util.GetOrAddComponent<T>(go);
        //객체화된 게임 오브젝트의 T 스크립트 추가하고 가져오거나 그냥 가져오거나
        //스크립트 컴포넌트를갖고 오거나 만들어서 갖고오거나
    }

    public void Clear()
    {
        _popupStack.Clear();
        
        /*
        if (_sceneUI != null)
        {
            Managers.Resource.Destroy(_sceneUI.gameObject);
            _sceneUI = null;
        }
        */
        
        /*
        애초에 플레이어UI는 계속 유지해야되지 않나..?
        if( _playerUI != null)
        {
            Managers.Resource.Destroy(_playerUI.gameObject);
            _playerUI = null;
        }
        */
    }
}
