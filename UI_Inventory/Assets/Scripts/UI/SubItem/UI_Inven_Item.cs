using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Inven_Item : UI_Base
{
    string _name;

    public void SetInfo(string name)
    {
        _name = name;
    }


    enum GameObjects
    {
        //UI_Inven_Item ������Ʈ ���Ͽ� �ִ� �̸���
        ItemIcon,
        ItemNameText,
    }

    void Start()
    {
        Init();
    }

    //�׽�Ʈ
    int i = 0;
    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        

        GameObject textObj = Get<GameObject>((int)GameObjects.ItemNameText);


        TMP_Text textComponent = textObj.GetComponent<TMP_Text>();
        textComponent.text = _name;
        
        GameObject itemImg = Get<GameObject>((int)GameObjects.ItemIcon);
        //Ÿ�� ������Ʈ�� ������� �̺�Ʈ �߰�
        UI_Base.AddUIEvent(itemImg, (PointerEventData data) => { Debug.Log($"d{_name} {i++}"); }, Define.UIEvent.Click);
    }
    
}
