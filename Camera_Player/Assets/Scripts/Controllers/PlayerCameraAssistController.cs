using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraAssistController : MonoBehaviour
{
    [SerializeField]
    Camera _camera = null;

    PlayerController _playerController=null;
    CameraController _cameraController = null;


    bool _isEntered = false;
    float _prevX;
    private void OnTriggerEnter(Collider other)
    {
        _prevX= transform.position.x;
        _playerController.JumpLock = true;
    }

    private void OnTriggerExit(Collider other)
    {
        float nowX=transform.position.x;
        float standard=other.GetComponent<BoxCollider>().size.x;
        standard = standard > 0 ? standard : -standard;
        bool isThrough = (_prevX-nowX  >= standard)||(_prevX - nowX <= -standard);
        _playerController.JumpLock = false;


        Debug.Log($"_prevX {_prevX} nowX {nowX} standard {standard} isThrough {isThrough}");

        if (isThrough)
        {
            if (other.gameObject.name == "StartTrigger0")
            {
                //임시 현재 맵 정보 받기

                _cameraController.StoreMapInfo(h: 8, w: 75, s: 20);

                Debug.Log("store next map0 info");

                _isEntered = true;

            }
            else if (other.gameObject.name == "EndTrigger0")
            {
                if (_isEntered)
                {
                    _isEntered = false;

                }
                else
                {
                    Debug.Log("store next map0 info");
                    _isEntered = true;
                }

            }
            else if (other.gameObject.name == "StartTrigger1")
            {
                if (_isEntered)
                {
                    Debug.Log("store next map0 info");
                    _isEntered = false;
                }
                else
                {
                    Debug.Log("store next map1 info");
                    _isEntered = true;
                }

            }
        }
    }

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _cameraController = _camera.GetComponent<CameraController>();
    }


    

}
