using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Entrance : UI_Popup
{
    enum GameObjects
    {
        BackGroundImage,
        TitleText,
        PlayText,
        OptionText,
        CreditText
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        base.Bind<GameObject>(typeof(GameObjects));

        GameObject playText=Get<GameObject>((int)GameObjects.PlayText);
        UI_Base.AddUIEvent(playText, OnTextClicked, Define.UIEvent.Click);

        GameObject optionText = Get<GameObject>((int)GameObjects.OptionText);
        UI_Base.AddUIEvent(optionText, OnTextClicked, Define.UIEvent.Click);

        GameObject creditText = Get<GameObject>((int)GameObjects.CreditText);
        UI_Base.AddUIEvent(creditText, OnTextClicked, Define.UIEvent.Click);

    }

    void OnTextClicked(PointerEventData data)
    {

        switch (data.pointerClick.gameObject.name)
        {
            case "PlayText":
                {
                    Managers.Scene.LoadScene(Define.Scene.Game);
                }
                return;//
            case "OptionText":
                {
                    Managers.UI.GeneratePopupUI<UI_Option>("UI_Option");
                }
                break;
            case "CreditText":
                {
                    Managers.UI.GeneratePopupUI<UI_Credit>("UI_Credit");
                }
                break;
        }

        return;
    }
}
