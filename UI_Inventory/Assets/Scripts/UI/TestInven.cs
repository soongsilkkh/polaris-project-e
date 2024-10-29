using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class TestInven : MonoBehaviour
{
    bool isInvenOpen = false;

    UI_Inven playerUI = null;

    void Init()
    {
        playerUI=Managers.UI.GeneratePlayerUI<UI_Inven>("UI_Inven");
        

        isInvenOpen = false;

        Managers.Input.KeyAction -=OnKeyBoardOpenCloseInven;
        Managers.Input.KeyAction +=OnKeyBoardOpenCloseInven;
    }

    void Start()
    {
        Init();
    }


    void OnKeyBoardOpenCloseInven()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isInvenOpen)
            {

                StartCoroutine("COHidePlayerUIAnimation", 0.5f);


                //Managers.UI.HidePlayerUI();

                isInvenOpen = false;
            }
            else
            {
                Managers.UI.ShowPlayerUI();

                StartCoroutine("COShowPlayerUIAnimation", 0.5f);

                isInvenOpen =true;
                
            }
        }
    }

    IEnumerator COShowPlayerUIAnimation(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        playerUI.gridPanel.transform.DOMoveX(840f, 1.0f)
            .SetEase(Ease.OutBounce);
    }

    IEnumerator COHidePlayerUIAnimation(float seconds)
    {

        playerUI.gridPanel.transform.DOMoveX(1000f, 1.0f)
            .SetEase(Ease.InOutBack);

        yield return new WaitForSeconds(seconds);

        Managers.UI.HidePlayerUI();
    }

}
