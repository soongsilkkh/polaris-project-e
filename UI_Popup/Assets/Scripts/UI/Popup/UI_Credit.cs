using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Credit : UI_Popup
{
    enum GameObjects
    {
        //subitem - UI_BackText
        BackGroundImage,
        TitleText,
        CreditText,

    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        Transform backTextTransform = gameObject.transform.Find("UI_BackText");

        if (backTextTransform != null)
            Managers.Resource.Destroy(backTextTransform.gameObject);

        GameObject backText
            = Managers.UI.MakeSubItem<UI_BackText>(gameObject.transform, "UI_BackText").gameObject;
    }
}