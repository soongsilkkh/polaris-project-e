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
        Reality, //����
        Virtuality, //����+����
        Puzzle, //�̴ϰ���

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
