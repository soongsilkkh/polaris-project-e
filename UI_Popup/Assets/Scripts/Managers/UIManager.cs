using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    int _order = 100;

    //UI_Scene _sceneUI = null;
    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    //UI_Player _playerUI=null;

    //�Լ��� �ƴ� ������Ƽ
    public GameObject UI_root
    {
        get
        {
            GameObject ui_root = GameObject.Find("@UI_Root");//�Ҽӽ�ų �θ� ã�ų� ����ų�
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

        //���� ĵ���� ������Ʈ�� ������� ��������.
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        //������ ������ ���� ������ ���������� �ڵ��.

        canvas.overrideSorting = true;
        //ĵ������ ��ø�� ���¿��� �θ� ĵ������ � ���� ������ ������ ���ÿ����� ���� �� �ְ� �ϴ� �ɼ�.

        //sort �����Ѵٴ� �� ������ UI�� �˾��̶�� ��
        if (sort)
            canvas.sortingOrder = _order++;
        else // �˾��� �ƴ� �׳� UI�϶�
        { canvas.sortingOrder = 0; }

    }

    

    public T GeneratePopupUI<T>(string name = null)
   where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;//T�� name���� ���.

        //prefab������ ����Ǿ� �ִ� ��(name)�� ��üȭ��Ű��
        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");


        T popup = Util.GetOrAddComponent<T>(go);


        _popupStack.Push(popup);


        //������Ƽ Ȱ��.
        go.transform.SetParent(UI_root.transform);

        return popup;
    }

    //���� �˾��� �ݴ� �۾��ϴ� �Լ��� ������
    public void PopPopupUI()
    {
        if (_popupStack.Count == 0)
            return;

        UI_Popup popup = _popupStack.Pop();
        //���ÿ��� �����ϰ�
        Managers.Resource.Destroy(popup.gameObject);
        //�����Ѱ� ����. ������, hierarchy����

        //UI_Popup�� ������ ��ũ��Ʈ ������Ʈ�� ���� �ִ� ����(���� ������Ʈ)�� ������Ű�� ����.

        popup = null;//���� ���� ����
    }

    //������� �˾� UI Ȯ�α����ϴ� �Լ�
    public void PopPopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
            return;

        if (_popupStack.Peek() != popup) return;

        PopPopupUI();

    }

    //��� ����� ��
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

        _sceneUI = sceneUI;//���ø��� �׳� ������ ����.

        //������Ƽ Ȱ��.
        go.transform.SetParent(UI_root.transform);

        return sceneUI;
    }

    public void DestroySceneUI()
    {
        if (_sceneUI==null)
            return;

        
        Managers.Resource.Destroy(_sceneUI.gameObject);
        //�����Ѱ� ����. ������, hierarchy����

        _sceneUI = null;//���� ���� ����
    }

    */
    #endregion




    public T MakeSubItem<T>(Transform parent = null, string name = null) where T : UI_Base
    {//T�� ��ũ��Ʈ, name�� ��üȭ�� ������ �̸�

        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/SubItem/{name}");
        //��θ� ���� ��üȭ �ϰ�

        //�θ� ����
        if (parent != null)
            go.transform.SetParent(parent);

        return Util.GetOrAddComponent<T>(go);
        //��üȭ�� ���� ������Ʈ�� T ��ũ��Ʈ �߰��ϰ� �������ų� �׳� �������ų�
        //��ũ��Ʈ ������Ʈ������ ���ų� ���� ������ų�
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
        ���ʿ� �÷��̾�UI�� ��� �����ؾߵ��� �ʳ�..?
        if( _playerUI != null)
        {
            Managers.Resource.Destroy(_playerUI.gameObject);
            _playerUI = null;
        }
        */
    }
}
