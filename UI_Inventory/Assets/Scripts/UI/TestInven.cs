using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInven : MonoBehaviour
{
    bool isInvenOpen = false;
    void Init()
    {
        Managers.UI.GeneratePlayerUI<UI_Inven>("UI_Inven");
        isInvenOpen = true;

        Managers.Input.KeyAction -=OnKeyBoardOpenCloseInven;
        Managers.Input.KeyAction +=OnKeyBoardOpenCloseInven;
    }

    void Start()
    {
        Init();
    }


    void OnKeyBoardOpenCloseInven()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isInvenOpen)
            {
                Managers.UI.HidePlayerUI();
                isInvenOpen = false;
            }
            else
            {
                Managers.UI.ShowPlayerUI();
                isInvenOpen=true;
            }
        }
    }

}
