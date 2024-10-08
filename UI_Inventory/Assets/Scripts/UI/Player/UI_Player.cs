using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Player : UI_Base
{
    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, false);
    }

    public virtual void DestroyPlayerUI()
    {
        Managers.UI.DestroyPlayerUI();
        //자기 자신 닫는 행동
    }


}
