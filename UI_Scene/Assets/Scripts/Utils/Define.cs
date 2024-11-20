using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Sound
    {
        BGM,
        Effect,
        MaxCount,//맨끝에는 MaxCount갯수를 나타내는 효과
    }


    public enum Scene
    //어떤 Scene들이 필요한지 생각해보자
    {
        Unknown,
        Entrance,//
        Lobby,//로비 화면
        Game,//인게임 씬
        Option,//옵션 
        Credit,//크레딧 화면
        //등등 
    }

    public enum UIEvent
    {
        Click,
        Drag,

    }
    public enum MouseEvent
    {
        Press,
        Click
    }
    public enum CameraMode
    {
        QuaterView,
    }

}
