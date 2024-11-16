using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Scene
    //어떤 Scene들이 필요한지 생각해보자
    {
        Unknown,
        Login,//로그인 화면
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

    public enum PlayerMode
    {
        Reality, //현실
        Virtuality, //현실+가상
        Puzzle, //미니게임

    }

    public enum CameraMode
    {
        VerticalQuaterView,
        VerticalHumanView,
        HorizontalHallView,

    }

    public enum MouseEvent
    {
        Press,
        Click,

    }
}
