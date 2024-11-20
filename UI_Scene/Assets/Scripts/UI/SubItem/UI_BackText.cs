using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_BackText : UI_Base
{
    enum GameObjects
    {
        BackText,

    }

    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));

        GameObject backText=Get<GameObject>((int)GameObjects.BackText);

        UI_Base.AddUIEvent(backText,OnTextClicked,Define.UIEvent.Click);
    }

    void OnTextClicked(PointerEventData data)
    {
        Managers.UI.PopPopupUI();
    }
}
