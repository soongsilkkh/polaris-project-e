using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup : UI_Base
{ 
    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, true);
    }


    public virtual void PopPopupUI()
    {
        Managers.UI.PopPopupUI(this);
    }

    public virtual void PopAllPopupUI()
    {
        Managers.UI.PopAllPopupUI();
    }
}
