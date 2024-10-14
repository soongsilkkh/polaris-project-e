using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    struct MapInfo
    {
        float Height;
        float Width;
        Vector3 StartPosition;
        Vector3 EndPosition;
    }

    [SerializeField]
    Define.CameraMode _cameraMode=Define.CameraMode.VerticalQuaterView;

    [SerializeField]
    float CameraDeltaX = 0;
    [SerializeField]
    float CameraDeltaY = 3f;
    [SerializeField]
    float CameraDeltaZ = 6f;

    [SerializeField]
    GameObject _player = null;

    private void LateUpdate()
    { 
        switch (_cameraMode)
        {
            case Define.CameraMode.VerticalQuaterView:
                DoVerticalQuaterView();
                break;
            case Define.CameraMode.VerticalHumanView:
                break;
            default:
                break;
        }
    }

    public void SetCameraDelta(float x,float y, float z)
    {
        CameraDeltaX = x;
        CameraDeltaY = y;
        CameraDeltaZ = z;
    }
    public void SetCameraMode(Define.CameraMode mode)
    {
        _cameraMode = mode;
    }
    
    public void GetMapInfo()
    {

    }

    private void DoVerticalQuaterView()
    {
        transform.position = _player.transform.position + Vector3.up * CameraDeltaY
          + Vector3.back * CameraDeltaZ
            + Vector3.right * CameraDeltaX;
        Vector3 temp = _player.transform.position + Vector3.up * 2.0f;

        transform.LookAt(temp);
    }

    private void DoVerticalHumanView()
    {

    }
}
