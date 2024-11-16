using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.CameraMode _cameraMode=Define.CameraMode.VerticalQuaterView;

    //no data detach
    [SerializeField]
    float CameraDeltaX = 0f;
    [SerializeField]
    float CameraDeltaY = 2.5f;
    [SerializeField]
    float CameraDeltaZ = 7.75f;

    [SerializeField]
    GameObject _player = null;
    
    
    PlayerController _playerController = null;

    Dictionary<int, CameraInfo> _cameraInfoDict = null;

    class MapInfo
    {
        public float Depth;
        public float Height;
        public float Width;
        public float StartPosX;
        public float EndPosX;
        public float NearStartPosX;
        public float NearEndPosX;
    }
    MapInfo _mapInfo = null;



    public void SetCameraDelta(float x, float y, float z)
    {
        CameraDeltaX = x;
        CameraDeltaY = y;
        CameraDeltaZ = z;
    }
    public void SetCameraMode(Define.CameraMode mode)
    {
        switch (mode)
        {
            case Define.CameraMode.VerticalQuaterView:
                _playerController.PosCamera = PlayerController.CameraPos.minusZ;
                break;
            case Define.CameraMode.VerticalHumanView:
                _playerController.PosCamera = PlayerController.CameraPos.minusZ;
                break;
            case Define.CameraMode.HorizontalHallView:
                _playerController.PosCamera = PlayerController.CameraPos.minusX;
                break;
            default:
                break;
        }


        _cameraMode = mode;
    }

    public void StoreMapInfo(float d,float h, float w, float s)
    {

        _mapInfo = new MapInfo()
        {
            Depth=d,
            Width = w,
            Height = h,
            StartPosX = s,
            EndPosX = s + w,
            NearStartPosX = s + 3.75f,
            NearEndPosX = s + w - 3.75f
        };

        Debug.Log($"{h},{w},{s},{_mapInfo.EndPosX}");
        //use height
    }
    public void StoreMapInfo()
    {
        _mapInfo = null;
    }




    private void Start()
    {
        _player = GameObject.Find("unitychan");


        _playerController = _player.GetComponent<PlayerController>();


       _cameraInfoDict = Managers.Data.CameraDict;


        //basic settings
        SetCameraDelta(0, 2.5f, 7.75f);

        gameObject.GetComponent<Camera>().fieldOfView = 50;
        gameObject.GetComponent<Camera>().nearClipPlane = 0.3f;
        gameObject.GetComponent<Camera>().farClipPlane = 1000f;


    }

    private void FixedUpdate()
    { 
        switch (_cameraMode)
        {
            case Define.CameraMode.VerticalQuaterView:
                DoVerticalQuaterView();
                break;
            case Define.CameraMode.VerticalHumanView:
                DoVerticalHumanView();
                break;
            case Define.CameraMode.HorizontalHallView:
                DoHorizontalHallView();
                break;
            default:
                break;
        }
    }



    private void DoVerticalQuaterView()
    {
        transform.position = _player.transform.position + Vector3.up * CameraDeltaY
          + Vector3.back * CameraDeltaZ
            + Vector3.right * CameraDeltaX;


        Vector3 temp = _player.transform.position + Vector3.up * _cameraInfoDict[0].stoodUp; 

        transform.LookAt(temp);
    }

    //select between humanzoom1 or humanzoom2 by mapinfo exists
    private void DoVerticalHumanView()
    {
        //map height => cam rot x
        //map width => cam pos partial y

        Vector3 temp = _player.transform.position + Vector3.up * 2.0f;

        if (_mapInfo == null)
        {
            VerticalHumanMoveAndZoomInOut1();
            temp = _player.transform.position + Vector3.up * _cameraInfoDict[1].stoodUp;
        }
        else
        {
            VerticalHumanMoveAndZoomInOut2();
            temp = _player.transform.position + Vector3.up * _cameraInfoDict[2].stoodUp;
        }

        
        transform.LookAt(temp);
    }


    Vector3 hallViewTarget=Vector3.zero;
    private void DoHorizontalHallView()
    {
        HorizontalHallViewMove();

        if(hallViewTarget== Vector3.zero)
        {
            hallViewTarget = _player.transform.position;
            hallViewTarget = hallViewTarget + Vector3.up * 2.0f;
        }
        else
        {
            Vector3 temp = _player.transform.position + Vector3.up * 2.0f;
            hallViewTarget = Vector3.Slerp(hallViewTarget, temp, 0.025f);
        }

        //Vector3 temp = _player.transform.position+Vector3.up * 2.0f;
        //transform.LookAt(temp);

        transform.LookAt(hallViewTarget);
    }


    private void VerticalHumanMoveAndZoomInOut1()
    {
        Vector3 newPos = _player.transform.position + Vector3.up * CameraDeltaY
              + Vector3.back * CameraDeltaZ
              + Vector3.right * CameraDeltaX;

        if (_playerController.StatePlayer == PlayerController.PlayerState.Idle)
        {
            newPos = newPos - Vector3.up * CameraDeltaY * (1.0f - _cameraInfoDict[1].zoomInY)
                -Vector3.back*CameraDeltaZ * (1.0f- _cameraInfoDict[1].zoomInZ);

            transform.position = Vector3.Slerp(transform.position, newPos, _cameraInfoDict[1].zoomInSpeed);
        }
        else
        {
            if (_playerController.PlayerHorizonMove==PlayerController.PlayerHorizontalMovement.Right)
                //Input.GetKey(KeyCode.D))
            {
                newPos = newPos - Vector3.right * CameraDeltaX + Vector3.right * 4f;

                transform.position = Vector3.Slerp(transform.position, newPos, _cameraInfoDict[1].zoomOutSpeed);
            }
            if (_playerController.PlayerHorizonMove ==PlayerController.PlayerHorizontalMovement.Left)
                //Input.GetKey(KeyCode.A))
            {
                newPos = newPos - Vector3.right * CameraDeltaX + Vector3.right * -4f;

                transform.position = Vector3.Slerp(transform.position, newPos, _cameraInfoDict[1].zoomOutSpeed);
            }


            //if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            if(_playerController.PlayerVertMove ==PlayerController.PlayerVerticalMovement.Forward||
                _playerController.PlayerVertMove ==PlayerController.PlayerVerticalMovement.Back)
            {
                //!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
                if (_playerController.PlayerHorizonMove ==PlayerController.PlayerHorizontalMovement.Idle)
                    transform.position = Vector3.Slerp(transform.position, newPos, _cameraInfoDict[1].zoomOutSpeed);
            }
        }
    }


    //Cameara moves with mapinfo, trigger, 
    private void VerticalHumanMoveAndZoomInOut2()
    {
        Vector3 newPos = _player.transform.position + Vector3.up * CameraDeltaY
              + Vector3.back * CameraDeltaZ
              + Vector3.right * CameraDeltaX;

        if (_playerController.StatePlayer == PlayerController.PlayerState.Idle)
        {
            newPos=newPos-Vector3.up*CameraDeltaY*(1.0f- _cameraInfoDict[2].zoomInY) -Vector3.back*CameraDeltaZ*(1.0f- _cameraInfoDict[2].zoomInZ);

            if(transform.position.x>=_mapInfo.NearStartPosX&& transform.position.x<=_mapInfo.NearEndPosX)
                //맵 가운데 쪽
                transform.position = Vector3.Slerp(transform.position, newPos, _cameraInfoDict[2].zoomInSpeed);
            else if(transform.position.x<= _mapInfo.NearStartPosX &&transform.position.x>=_mapInfo.StartPosX)
                //맵 시작지점 부근
                transform.position = Vector3.Slerp(transform.position, newPos, _cameraInfoDict[2].nearEdgeCameraSpeed);
            else if (transform.position.x >= _mapInfo.NearEndPosX && transform.position.x <= _mapInfo.EndPosX)
                //맵 끝지점 부근
                transform.position = Vector3.Slerp(transform.position, newPos, _cameraInfoDict[2].nearEdgeCameraSpeed);
            else//전 맵 끝시작 ~ 다음 맵 시작지점 (다음 맵 부분을 벗어난 부분에서)
                transform.position = Vector3.Slerp(transform.position, newPos, _cameraInfoDict[2].bridgeCameraSpeed);
        }
        else
        {
            if (_playerController.PlayerHorizonMove ==PlayerController.PlayerHorizontalMovement.Right)
                //Input.GetKey(KeyCode.D))
            {
                newPos = newPos - Vector3.right * CameraDeltaX + Vector3.right * 4f;

                if (transform.position.x >= _mapInfo.NearStartPosX && transform.position.x <= _mapInfo.NearEndPosX)
                    transform.position = Vector3.Slerp(transform.position, newPos, _cameraInfoDict[2].zoomOutSpeed);
                else if (transform.position.x <= _mapInfo.NearStartPosX && transform.position.x >= _mapInfo.StartPosX)
                    //맵 시작지점 부근
                    transform.position = Vector3.Slerp(transform.position, newPos, _cameraInfoDict[2].nearEdgeCameraSpeed);
                else if (transform.position.x >= _mapInfo.NearEndPosX && transform.position.x <= _mapInfo.EndPosX)
                    //맵 끝지점 부근
                    transform.position = Vector3.Slerp(transform.position, newPos, _cameraInfoDict[2].nearEdgeCameraSpeed);
                else//전 맵 끝시작 ~ 다음 맵 시작지점 (다음 맵 부분을 벗어난 부분에서)
                    transform.position = Vector3.Slerp(transform.position, newPos, _cameraInfoDict[2].bridgeCameraSpeed);
            }
            if (_playerController.PlayerHorizonMove ==PlayerController.PlayerHorizontalMovement.Left)
                //Input.GetKey(KeyCode.A))
            {
                newPos = newPos - Vector3.right * CameraDeltaX + Vector3.right * -4f;

                if (transform.position.x >= _mapInfo.NearStartPosX && transform.position.x <= _mapInfo.NearEndPosX)
                    transform.position = Vector3.Slerp(transform.position, newPos, _cameraInfoDict[2].zoomOutSpeed);
                else if (transform.position.x <= _mapInfo.NearStartPosX && transform.position.x >= _mapInfo.StartPosX)
                    //맵 시작지점 부근
                    transform.position = Vector3.Slerp(transform.position, newPos, _cameraInfoDict[2].nearEdgeCameraSpeed);
                else if (transform.position.x >= _mapInfo.NearEndPosX && transform.position.x <= _mapInfo.EndPosX)
                    //맵 끝지점 부근
                    transform.position = Vector3.Slerp(transform.position, newPos, _cameraInfoDict[2].nearEdgeCameraSpeed);
                else//전 맵 끝시작 ~ 다음 맵 시작지점 (다음 맵 부분을 벗어난 부분에서)
                    transform.position = Vector3.Slerp(transform.position, newPos, _cameraInfoDict[2].bridgeCameraSpeed);
            }


            //if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            if (_playerController.PlayerVertMove ==PlayerController.PlayerVerticalMovement.Forward ||
                _playerController.PlayerVertMove ==PlayerController.PlayerVerticalMovement.Back)
            {
                if (_playerController.PlayerHorizonMove ==PlayerController.PlayerHorizontalMovement.Idle)
                    //!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
                {
                    if (transform.position.x >= _mapInfo.NearStartPosX && transform.position.x <= _mapInfo.NearEndPosX)
                        transform.position = Vector3.Slerp(transform.position, newPos, _cameraInfoDict[2].zoomOutSpeed);
                    else if (transform.position.x <= _mapInfo.NearStartPosX && transform.position.x >= _mapInfo.StartPosX)
                        //맵 시작지점 부근
                        transform.position = Vector3.Slerp(transform.position, newPos, _cameraInfoDict[2].nearEdgeCameraSpeed);
                    else if (transform.position.x >= _mapInfo.NearEndPosX && transform.position.x <= _mapInfo.EndPosX)
                        //맵 끝지점 부근
                        transform.position = Vector3.Slerp(transform.position, newPos, _cameraInfoDict[2].nearEdgeCameraSpeed);
                    else//전 맵 끝시작 ~ 다음 맵 시작지점 (다음 맵 부분을 벗어난 부분에서)
                        transform.position = Vector3.Slerp(transform.position, newPos, _cameraInfoDict[2].bridgeCameraSpeed);
                }
            }
        }
    }


    private void HorizontalHallViewMove()
    {
        Vector3 newPos = Vector3.right * _player.transform.position.x
            + Vector3.up * _player.transform.position.y
            + Vector3.back*_mapInfo.Depth/2
            + Vector3.up * CameraDeltaY+ Vector3.back * CameraDeltaZ+ Vector3.right * CameraDeltaX;


        if (_playerController.StatePlayer == PlayerController.PlayerState.Idle) 
        {
            transform.position = Vector3.Slerp(transform.position, newPos, 0.035f);
        }
        else 
        {
            if (_playerController.PlayerHorizonMove == PlayerController.PlayerHorizontalMovement.Left) 
            {
                transform.position = Vector3.Slerp(transform.position, newPos, 0.08f);
            }
            else
            {
                transform.position = Vector3.Slerp(transform.position, newPos, 0.02f);
            }
        }

            


    }

}
