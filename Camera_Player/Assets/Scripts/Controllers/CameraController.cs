using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    class MapInfo
    {
        public float Height;
        public float Width;
        public float StartPosX;
        public float EndPosX;
        public float NearStartPosX;
        public float NearEndPosX;
    }
    MapInfo _mapInfo = null;

    [SerializeField]
    public Define.CameraMode _cameraMode=Define.CameraMode.VerticalQuaterView;

    [SerializeField]
    float CameraDeltaX = 0;
    [SerializeField]
    float CameraDeltaY = 3f;
    [SerializeField]
    float CameraDeltaZ = 6f;

    [SerializeField]
    GameObject _player = null;
    PlayerController _playerController = null;

    private void Start()
    {
       _playerController = _player.GetComponent<PlayerController>();

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
    
    public void StoreMapInfo(float h,float w, float s,float e,float nearRange=0.12f)
    {
        //Debug.Log($"{h},{w},{s},{e}");
        _mapInfo = new MapInfo() { Height = h , Width=w, StartPosX=s, EndPosX = e,
        NearStartPosX=s+w*nearRange, NearEndPosX=e-w*nearRange };
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
        //map height => cam rot x
        //map width => cam pos partial y


        if (_mapInfo == null)
            VerticalHumanMoveAndZoomInOut();
        else
            VerticalHumanMoveAndZoomInOut2();


        Vector3 temp = _player.transform.position + Vector3.up * 1.75f;
                
        transform.LookAt(temp);
        

    }

    private void VerticalHumanMoveAndZoomInOut()
    {
        float zoomInSpeed = 0.01f;
        float zoomInY = 0.8f;
        float zoomInZ = 0.7f;

        float zoomOutSpeed = 0.02f;

        Vector3 newPos = _player.transform.position + Vector3.up * CameraDeltaY
              + Vector3.back * CameraDeltaZ
              + Vector3.right * CameraDeltaX;

        if (_playerController.StatePlayer == PlayerController.PlayerState.Idle)
        {
            newPos = newPos - Vector3.up * CameraDeltaY * (1.0f - zoomInY)
                -Vector3.back*CameraDeltaZ * (1.0f-zoomInZ);

            transform.position = Vector3.Slerp(transform.position, newPos, zoomInSpeed);
        }
        else
        {
            if (_playerController.PlayerHorizonMove==PlayerController.PlayerHorizontalMovement.Right)
                //Input.GetKey(KeyCode.D))
            {
                newPos = newPos - Vector3.right * CameraDeltaX + Vector3.right * 4f;

                transform.position = Vector3.Slerp(transform.position, newPos, zoomOutSpeed);
            }
            if (_playerController.PlayerHorizonMove ==PlayerController.PlayerHorizontalMovement.Left)
                //Input.GetKey(KeyCode.A))
            {
                newPos = newPos - Vector3.right * CameraDeltaX + Vector3.right * -4f;

                transform.position = Vector3.Slerp(transform.position, newPos, zoomOutSpeed);
            }


            //if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            if(_playerController.PlayerVertMove ==PlayerController.PlayerVerticalMovement.Forward||
                _playerController.PlayerVertMove ==PlayerController.PlayerVerticalMovement.Back)
            {
                //!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
                if (_playerController.PlayerHorizonMove ==PlayerController.PlayerHorizontalMovement.Idle)
                    transform.position = Vector3.Slerp(transform.position, newPos, zoomOutSpeed);
            }
        }
    }


    private void VerticalHumanMoveAndZoomInOut2()
    {
        float zoomInSpeed = 0.009f;
        float zoomInY = 0.8f;
        float zoomInZ = 0.7f;

        float zoomOutSpeed = 0.02f;

        float nearEdgeCamSpeed = 0.008f;
        float bridgeCamSpeed = 0.05f;

        Vector3 newPos = _player.transform.position + Vector3.up * CameraDeltaY
              + Vector3.back * CameraDeltaZ
              + Vector3.right * CameraDeltaX;

        if (_playerController.StatePlayer == PlayerController.PlayerState.Idle)
        {
            newPos=newPos-Vector3.up*CameraDeltaY*(1.0f-zoomInY)-Vector3.back*CameraDeltaZ*(1.0f-zoomInZ);

            if(transform.position.x>=_mapInfo.NearStartPosX&& transform.position.x<=_mapInfo.NearEndPosX)
                //맵 가운데 쪽
                transform.position = Vector3.Slerp(transform.position, newPos, zoomInSpeed);
            else if(transform.position.x<= _mapInfo.NearStartPosX &&transform.position.x>=_mapInfo.StartPosX)
                //맵 시작지점 부근
                transform.position = Vector3.Slerp(transform.position, newPos, nearEdgeCamSpeed);
            else if (transform.position.x >= _mapInfo.NearEndPosX && transform.position.x <= _mapInfo.EndPosX)
                //맵 끝지점 부근
                transform.position = Vector3.Slerp(transform.position, newPos, nearEdgeCamSpeed);
            else//전 맵 끝시작 ~ 다음 맵 시작지점 (다음 맵 부분을 벗어난 부분에서)
                transform.position = Vector3.Slerp(transform.position, newPos, bridgeCamSpeed);
        }
        else
        {
            if (_playerController.PlayerHorizonMove ==PlayerController.PlayerHorizontalMovement.Right)
                //Input.GetKey(KeyCode.D))
            {
                newPos = newPos - Vector3.right * CameraDeltaX + Vector3.right * 4f;

                if (transform.position.x >= _mapInfo.NearStartPosX && transform.position.x <= _mapInfo.NearEndPosX)
                    transform.position = Vector3.Slerp(transform.position, newPos, zoomOutSpeed);
                else if (transform.position.x <= _mapInfo.NearStartPosX && transform.position.x >= _mapInfo.StartPosX)
                    //맵 시작지점 부근
                    transform.position = Vector3.Slerp(transform.position, newPos, nearEdgeCamSpeed);
                else if (transform.position.x >= _mapInfo.NearEndPosX && transform.position.x <= _mapInfo.EndPosX)
                    //맵 끝지점 부근
                    transform.position = Vector3.Slerp(transform.position, newPos, nearEdgeCamSpeed);
                else//전 맵 끝시작 ~ 다음 맵 시작지점 (다음 맵 부분을 벗어난 부분에서)
                    transform.position = Vector3.Slerp(transform.position, newPos, bridgeCamSpeed);
            }
            if (_playerController.PlayerHorizonMove ==PlayerController.PlayerHorizontalMovement.Left)
                //Input.GetKey(KeyCode.A))
            {
                newPos = newPos - Vector3.right * CameraDeltaX + Vector3.right * -4f;

                if (transform.position.x >= _mapInfo.NearStartPosX && transform.position.x <= _mapInfo.NearEndPosX)
                    transform.position = Vector3.Slerp(transform.position, newPos, zoomOutSpeed);
                else if (transform.position.x <= _mapInfo.NearStartPosX && transform.position.x >= _mapInfo.StartPosX)
                    //맵 시작지점 부근
                    transform.position = Vector3.Slerp(transform.position, newPos, nearEdgeCamSpeed);
                else if (transform.position.x >= _mapInfo.NearEndPosX && transform.position.x <= _mapInfo.EndPosX)
                    //맵 끝지점 부근
                    transform.position = Vector3.Slerp(transform.position, newPos, nearEdgeCamSpeed);
                else//전 맵 끝시작 ~ 다음 맵 시작지점 (다음 맵 부분을 벗어난 부분에서)
                    transform.position = Vector3.Slerp(transform.position, newPos, bridgeCamSpeed);
            }


            //if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            if (_playerController.PlayerVertMove ==PlayerController.PlayerVerticalMovement.Forward ||
                _playerController.PlayerVertMove ==PlayerController.PlayerVerticalMovement.Back)
            {
                if (_playerController.PlayerHorizonMove ==PlayerController.PlayerHorizontalMovement.Idle)
                    //!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
                {
                    if (transform.position.x >= _mapInfo.NearStartPosX && transform.position.x <= _mapInfo.NearEndPosX)
                        transform.position = Vector3.Slerp(transform.position, newPos, zoomOutSpeed);
                    else if (transform.position.x <= _mapInfo.NearStartPosX && transform.position.x >= _mapInfo.StartPosX)
                        //맵 시작지점 부근
                        transform.position = Vector3.Slerp(transform.position, newPos, nearEdgeCamSpeed);
                    else if (transform.position.x >= _mapInfo.NearEndPosX && transform.position.x <= _mapInfo.EndPosX)
                        //맵 끝지점 부근
                        transform.position = Vector3.Slerp(transform.position, newPos, nearEdgeCamSpeed);
                    else//전 맵 끝시작 ~ 다음 맵 시작지점 (다음 맵 부분을 벗어난 부분에서)
                        transform.position = Vector3.Slerp(transform.position, newPos, bridgeCamSpeed);
                }
            }
        }
    }


}
