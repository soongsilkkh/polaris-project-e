using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    struct MapInfo
    {
        public float Height;
        public float Width;
        public float StartPositionX;
        public float EndPositionX;
    }
    MapInfo _mapInfo;

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
        _mapInfo.Height = h;
        _mapInfo.Width = w;
        _mapInfo.StartPositionX = s;
        _mapInfo.EndPositionX = e;

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
        //map width => cam rot partial y
        //player move direction right/left=> cam rot z plus/minus

        //VerticalHumanZoomInOut();

        transform.position = _player.transform.position + Vector3.up * CameraDeltaY
          + Vector3.back * CameraDeltaZ
            + Vector3.right * CameraDeltaX;


        Vector3 temp = _player.transform.position + Vector3.up * 2.0f;
        transform.LookAt(temp);
        VerticalHumanRotateZ();

    }

    private void VerticalHumanZoomInOut()
    {
        if (_playerController.StatePlayer == PlayerController.PlayerState.Idle)
        {
            Vector3 newPos = _player.transform.position + Vector3.up * CameraDeltaY * 0.85f
              + Vector3.back * CameraDeltaZ * 0.75f
                + Vector3.right * CameraDeltaX;

            transform.position = Vector3.Slerp(transform.position, newPos, 0.01f);
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
                transform.position = Vector3.Slerp(transform.position, newPos, 0.02f);
            }
            if (_playerController.PlayerHorizonMove ==
                PlayerController.PlayerHorizontalMovement.Left)//Input.GetKey(KeyCode.A))
            {
                newPos = _player.transform.position + Vector3.up * CameraDeltaY
              + Vector3.back * CameraDeltaZ
                + Vector3.right * -4f;
                transform.position = Vector3.Slerp(transform.position, newPos, 0.02f);
            }


            //if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            if(_playerController.PlayerVertMove ==
                PlayerController.PlayerVerticalMovement.Forward||
                _playerController.PlayerVertMove ==
                PlayerController.PlayerVerticalMovement.Back)
            {
                if (_playerController.PlayerHorizonMove ==
                PlayerController.PlayerHorizontalMovement.Idle)//!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
                    transform.position = Vector3.Slerp(transform.position, newPos, 0.04f);
            }
        }
    }

    private void VerticalHumanRotateZ()
    {
        //player move direction right/left=> cam rot z plus/minus
        
        if (_playerController.StatePlayer == PlayerController.PlayerState.Idle)
        {
            Quaternion newRot =
                Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, 0f));
                //new Quaternion(transform.rotation.x, transform.rotation.y, 0f,transform.rotation.w);

            transform.rotation = Quaternion.Slerp(transform.rotation, newRot, 0.1f);
        }
        else
        {
            Quaternion newRot =
                 Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, 0f));
            //new Quaternion(transform.rotation.x, transform.rotation.y, 0f, transform.rotation.w);

            if (Input.GetKey(KeyCode.D))
            {
                newRot =
                     Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, -12f));
                //new Quaternion(transform.rotation.x, transform.rotation.y, -120f, transform.rotation.w);
                transform.rotation = Quaternion.Slerp(transform.rotation, newRot, 0.2f);
            }
            if (Input.GetKey(KeyCode.A))
            {
                newRot =
                     Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, 12f));
                //new Quaternion(transform.rotation.x, transform.rotation.y, 120f, transform.rotation.w);
                transform.rotation = Quaternion.Slerp(transform.rotation, newRot, 0.2f);
            }
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
                    transform.rotation = Quaternion.Slerp(transform.rotation, newRot, 0.4f);
            }
        }


    }
}
