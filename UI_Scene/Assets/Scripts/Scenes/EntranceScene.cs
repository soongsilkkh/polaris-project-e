using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceScene : BaseScene
{


    protected override void Init()
    {
        base.Init();

        base.SceneType = Define.Scene.Entrance;

        //Generate first PopupUI
        Managers.UI.GeneratePopupUI<UI_Entrance>("UI_Entrance");

        //test
        //Managers.Input.KeyAction -= OnKeyBoardTest;
        //Managers.Input.KeyAction += OnKeyBoardTest;
    }

    void OnKeyBoardTest()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Managers.Scene.LoadScene(Define.Scene.Game);
        }
    }

    public override void Clear()
    {

    }

}
