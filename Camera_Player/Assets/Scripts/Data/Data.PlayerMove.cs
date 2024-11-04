using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class PlayerMove
{
    public int id;
    public float speed;
    public float accelspeed;
    public float jump;
    public float acceljump;
}


[Serializable]
public class PlayerMoveData : ILoader<int, PlayerMove>
{
    public List<PlayerMove> playermoves = new List<PlayerMove>();

    public Dictionary<int, PlayerMove> MakeDict()
    {
        Dictionary<int, PlayerMove> dict = new Dictionary<int, PlayerMove>();
        foreach (PlayerMove playermove in playermoves)
            dict.Add(playermove.id, playermove);

        return dict;
    }
}