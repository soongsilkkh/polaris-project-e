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
        //UI_Inven_Item 오브젝트 산하에 있는 이름들
        ItemIcon,
        ItemNameText,
    }

    void Start()
    {
        Init();
    }

    //테스트
    int i = 0;
    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        

        GameObject textObj = Get<GameObject>((int)GameObjects.ItemNameText);


        TMP_Text textComponent = textObj.GetComponent<TMP_Text>();
        textComponent.text = _name;
        
        GameObject itemImg = Get<GameObject>((int)GameObjects.ItemIcon);
        //타겟 오브젝트를 대상으로 이벤트 추가
        UI_Base.AddUIEvent(itemImg, (PointerEventData data) => { Debug.Log($"d{_name} {i++}"); }, Define.UIEvent.Click);
    }
    
}
