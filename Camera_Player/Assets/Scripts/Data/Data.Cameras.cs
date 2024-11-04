using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class CameraInfo
{
    public int id;
    public float stoodUp;
    public float zoomInSpeed;
    public float zoomInY;
    public float zoomInZ;
    public float zoomOutSpeed;
    public float cameraMove;
    public float nearEdgeCameraSpeed;
    public float bridgeCameraSpeed;
}


[Serializable]
public class CameraData : ILoader<int, CameraInfo>
{
    public List<CameraInfo> cameras = new List<CameraInfo>();
    public Dictionary<int, CameraInfo> MakeDict()
    {
        Dictionary<int, CameraInfo> dict = new Dictionary<int, CameraInfo>();

        foreach (CameraInfo camera in cameras)
            dict.Add(camera.id, camera);

        return dict;
    }
}
