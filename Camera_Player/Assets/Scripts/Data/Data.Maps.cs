using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Map
{
    public int id;
    public int height;
    public int width;
    public int start;
}

[Serializable]
public class MapData : ILoader<int, Map>
{
    public List<Map> maps= new List<Map>();


    public Dictionary<int, Map> MakeDict()
    {
        Dictionary<int, Map> dict = new Dictionary<int, Map>();
        foreach (Map map in maps)
            dict.Add(map.id, map);

        return dict;
    }
}