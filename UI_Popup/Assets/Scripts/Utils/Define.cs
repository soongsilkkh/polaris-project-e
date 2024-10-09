using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Sound
    {
        BGM,
        Effect,
        MaxCount,//�ǳ����� MaxCount������ ��Ÿ���� ȿ��
    }


    public enum Scene
    //� Scene���� �ʿ����� �����غ���
    {
        Unknown,
        Entrance,//
        Lobby,//�κ� ȭ��
        Game,//�ΰ��� ��
        Option,//�ɼ� 
        Credit,//ũ���� ȭ��
        //��� 
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
