using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum TriggerMode
    {
        MapStartTrigger,
        MapEndTrigger,


        NPCTrigger,
        ObjectTrigger,

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

    }

    public enum MouseEvent
    {
        Press,
        Click,

    }
}
