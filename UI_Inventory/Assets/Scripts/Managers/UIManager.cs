using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    int _order = 100;

    //UI_Scene _sceneUI = null;
    //Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    UI_Player _playerUI=null;

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

    public GameObject UI_root_dontdestroy
    {
        get
        {
            GameObject ui_root_dontdestroy = GameObject.Find("@UI_Root_DontDestroy");
            if(ui_root_dontdestroy == null)
            {
                ui_root_dontdestroy = new GameObject { name = "@UI_Root_DontDestroy" };
                
            }

            Object.DontDestroyOnLoad(ui_root_dontdestroy);

            return ui_root_dontdestroy;
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

    
    public T GeneratePlayerUI<T>(string name = null)
   where T : UI_Player
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;//T를 name으로 사용.

        //prefab폴더에 저장되어 있는 애(name)를 객체화시키고
        GameObject go = Managers.Resource.Instantiate($"UI/Player/{name}");


        T playerUI = Util.GetOrAddComponent<T>(go);


        _playerUI=playerUI;


        //프로퍼티 활용.
        go.transform.SetParent(UI_root_dontdestroy.transform);

        playerUI.gameObject.SetActive(false);

        return playerUI;
    }

    public void DestroyPlayerUI()
    {
        if (_playerUI == null)
            return;

        Managers.Resource.Destroy( _playerUI.gameObject);

        _playerUI = null;
    }

    public void ShowPlayerUI()
    {
        _playerUI.gameObject.SetActive(true);
    }

    public void HidePlayerUI()
    {
        _playerUI.gameObject.SetActive(false);
    }

    public T MakeSubItem<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/SubItem/{name}");
        //경로를 통해 객체화 하고

        //부모 연결
        if (parent != null)
            go.transform.SetParent(parent);

        return Util.GetOrAddComponent<T>(go);
        //스크립트 컴포넌트를갖고 오거나 만들어서 갖고오거나
    }

    public void Clear()
    {
        //_popupStack.Clear();
        /*
        if (_sceneUI != null)
        {
            Managers.Resource.Destroy(_sceneUI.gameObject);
            _sceneUI = null;
        }
        */
        
        if( _playerUI != null)
        {
            Managers.Resource.Destroy(_playerUI.gameObject);
            _playerUI = null;
        }
    }
}
