using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key,Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<int,Map> MapDict { get;private set; }=new Dictionary<int, Map> ();
    public Dictionary<int,PlayerMove> PlayerMoveDict { get;private set; }=new Dictionary<int, PlayerMove> ();
    public Dictionary<int, CameraInfo> CameraDict { get;private set; }=new Dictionary<int, CameraInfo> ();

    public void Init()
    {
        MapDict = LoadJson<MapData, int, Map>("MapData").MakeDict();
        PlayerMoveDict=LoadJson<PlayerMoveData,int,PlayerMove>("PlayerMoveData").MakeDict ();
        CameraDict = LoadJson<CameraData, int, CameraInfo>("CameraData").MakeDict ();

    }

    LoadType LoadJson<LoadType,Key,Value>(string path) where LoadType : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        LoadType data = JsonUtility.FromJson<LoadType>(textAsset.text);

        return data;
    }

}
