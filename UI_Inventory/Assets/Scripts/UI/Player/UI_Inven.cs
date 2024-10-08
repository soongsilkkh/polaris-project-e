using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inven : UI_Player
{
    int _subItemCount = 4;

    enum GameObjects
    {
        GridPanel,
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);


        foreach (Transform child in gridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);//삭제
        

        for (int i = 0; i < _subItemCount; i++)
        {
            
            GameObject item = Managers.UI.MakeSubItem<UI_Inven_Item>(gridPanel.transform, "UI_Inven_Item").gameObject;

            UI_Inven_Item invenItem = Util.GetOrAddComponent<UI_Inven_Item>(item);
           
            invenItem.SetInfo($"{i} Item");

            //부모에서 산하 오브젝트를 타겟으로 이벤트 추가
            //UI_Base.AddUIEvent(item, (PointerEventData data) => { Debug.Log("ff"); }, Define.UIEvent.Click);
        }
    }
}
