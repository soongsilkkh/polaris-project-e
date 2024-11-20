using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PannelPuzzlePic : UI_Scene
{

    public override void Init()
    {
        base.Init();

        transform.GetComponent<Canvas>().sortingOrder = 1;
    }
    private void Start()
    {
        Init();
    }

    
}
