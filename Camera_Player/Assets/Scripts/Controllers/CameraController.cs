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
        public float StartPositionX;
        public float EndPositionX;
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
    
    public void StoreMapInfo(float h,float w, float s,float e)
    {
        Debug.Log($"{h},{w},{s},{e}");
        _mapInfo = new MapInfo() { Height = h , Width=w, StartPositionX=s, EndPositionX = e };
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

        /*transform.position = _player.transform.position + Vector3.up * CameraDeltaY
          + Vector3.back * CameraDeltaZ
            + Vector3.right * CameraDeltaX;*/




        Vector3 temp = _player.transform.position + Vector3.up * 1.75f;
                
        transform.LookAt(temp);
        

    }

    private void VerticalHumanMoveAndZoomInOut()
    {
        if (_playerController.StatePlayer == PlayerController.PlayerState.Idle)
        {
            Vector3 newPos = _player.transform.position + Vector3.up * CameraDeltaY * 0.8f
              + Vector3.back * CameraDeltaZ * 0.7f
                + Vector3.right * CameraDeltaX;

            transform.position = Vector3.Slerp(transform.position, newPos, 0.03f);
        }
        else
        {
            Vector3 newPos = _player.transform.position + Vector3.up * CameraDeltaY
              + Vector3.back * CameraDeltaZ
              + Vector3.right * CameraDeltaX; ;

            if (_playerController.PlayerHorizonMove==
                PlayerController.PlayerHorizontalMovement.Right)//Input.GetKey(KeyCode.D))
            {
                newPos = _player.transform.position + Vector3.up * CameraDeltaY
              + Vector3.back * CameraDeltaZ
                + Vector3.right * 4f;
                transform.position = Vector3.Slerp(transform.position, newPos, 0.01f);
            }
            if (_playerController.PlayerHorizonMove ==
                PlayerController.PlayerHorizontalMovement.Left)//Input.GetKey(KeyCode.A))
            {
                newPos = _player.transform.position + Vector3.up * CameraDeltaY
              + Vector3.back * CameraDeltaZ
                + Vector3.right * -4f;
                transform.position = Vector3.Slerp(transform.position, newPos, 0.01f);
            }


            //if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            if(_playerController.PlayerVertMove ==
                PlayerController.PlayerVerticalMovement.Forward||
                _playerController.PlayerVertMove ==
                PlayerController.PlayerVerticalMovement.Back)
            {
                if (_playerController.PlayerHorizonMove ==
                PlayerController.PlayerHorizontalMovement.Idle)//!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
                    transform.position = Vector3.Slerp(transform.position, newPos, 0.02f);
            }
        }
    }


    private void VerticalHumanMoveAndZoomInOut2()
    {
        Debug.Log("move zoom in out 2");
        if (_playerController.StatePlayer == PlayerController.PlayerState.Idle)
        {
            Vector3 newPos = _player.transform.position + Vector3.up * CameraDeltaY * 0.8f
              + Vector3.back * CameraDeltaZ * 0.7f
                + Vector3.right * CameraDeltaX;

            if(transform.position.x>=_mapInfo.StartPositionX+_mapInfo.Width*0.1f//맵 가운데 쪽
                && transform.position.x<=_mapInfo.EndPositionX-_mapInfo.Width*0.1f)
                transform.position = Vector3.Slerp(transform.position, newPos, 0.01f);
            else if(transform.position.x<= _mapInfo.StartPositionX + _mapInfo.Width * 0.1f&&
                transform.position.x>=_mapInfo.StartPositionX)//맵 시작지점 부근
                transform.position = Vector3.Slerp(transform.position, newPos, 0.008f);
            else if (transform.position.x >= _mapInfo.EndPositionX - _mapInfo.Width * 0.1f &&
                transform.position.x <= _mapInfo.EndPositionX)//맵 끝지점 부근
                transform.position = Vector3.Slerp(transform.position, newPos, 0.008f);
            else//전 맵 끝시작 ~ 다음 맵 시작지점 (다음 맵 부분을 벗어난 부분에서)
                transform.position = Vector3.Slerp(transform.position, newPos, 0.07f);
        }
        else
        {
            Vector3 newPos = _player.transform.position + Vector3.up * CameraDeltaY
              + Vector3.back * CameraDeltaZ
              + Vector3.right * CameraDeltaX; ;

            if (_playerController.PlayerHorizonMove ==
                PlayerController.PlayerHorizontalMovement.Right)//Input.GetKey(KeyCode.D))
            {
                newPos = _player.transform.position + Vector3.up * CameraDeltaY
              + Vector3.back * CameraDeltaZ
                + Vector3.right * 4f;

                if (transform.position.x >= _mapInfo.StartPositionX + _mapInfo.Width * 0.1f
                && transform.position.x <= _mapInfo.EndPositionX - _mapInfo.Width * 0.1f)
                    transform.position = Vector3.Slerp(transform.position, newPos, 0.02f);
                else if (transform.position.x <= _mapInfo.StartPositionX + _mapInfo.Width * 0.1f &&
                transform.position.x >= _mapInfo.StartPositionX)
                    transform.position = Vector3.Slerp(transform.position, newPos, 0.008f);
                else if (transform.position.x >= _mapInfo.EndPositionX - _mapInfo.Width * 0.1f &&
                    transform.position.x <= _mapInfo.EndPositionX)
                    transform.position = Vector3.Slerp(transform.position, newPos, 0.008f);
                else
                    transform.position = Vector3.Slerp(transform.position, newPos, 0.07f);
            }
            if (_playerController.PlayerHorizonMove ==
                PlayerController.PlayerHorizontalMovement.Left)//Input.GetKey(KeyCode.A))
            {
                newPos = _player.transform.position + Vector3.up * CameraDeltaY
              + Vector3.back * CameraDeltaZ
                + Vector3.right * -4f;

                if (transform.position.x >= _mapInfo.StartPositionX + _mapInfo.Width * 0.1f
                && transform.position.x <= _mapInfo.EndPositionX - _mapInfo.Width * 0.1f)
                    transform.position = Vector3.Slerp(transform.position, newPos, 0.02f);
                else if (transform.position.x <= _mapInfo.StartPositionX + _mapInfo.Width * 0.1f &&
                transform.position.x >= _mapInfo.StartPositionX)
                    transform.position = Vector3.Slerp(transform.position, newPos, 0.008f);
                else if (transform.position.x >= _mapInfo.EndPositionX - _mapInfo.Width * 0.1f &&
                    transform.position.x <= _mapInfo.EndPositionX)
                    transform.position = Vector3.Slerp(transform.position, newPos, 0.008f);
                else
                    transform.position = Vector3.Slerp(transform.position, newPos, 0.07f);
            }


            //if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            if (_playerController.PlayerVertMove ==
                PlayerController.PlayerVerticalMovement.Forward ||
                _playerController.PlayerVertMove ==
                PlayerController.PlayerVerticalMovement.Back)
            {
                if (_playerController.PlayerHorizonMove ==
                PlayerController.PlayerHorizontalMovement.Idle)//!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
                {
                    if (transform.position.x >= _mapInfo.StartPositionX + _mapInfo.Width * 0.1f
                && transform.position.x <= _mapInfo.EndPositionX - _mapInfo.Width * 0.1f)
                        transform.position = Vector3.Slerp(transform.position, newPos, 0.04f);
                    else if (transform.position.x <= _mapInfo.StartPositionX + _mapInfo.Width * 0.1f &&
                transform.position.x >= _mapInfo.StartPositionX)
                        transform.position = Vector3.Slerp(transform.position, newPos, 0.008f);
                    else if (transform.position.x >= _mapInfo.EndPositionX - _mapInfo.Width * 0.1f &&
                        transform.position.x <= _mapInfo.EndPositionX)
                        transform.position = Vector3.Slerp(transform.position, newPos, 0.008f);
                    else
                        transform.position = Vector3.Slerp(transform.position, newPos, 0.07f);
                }
            }
        }
    }
}
