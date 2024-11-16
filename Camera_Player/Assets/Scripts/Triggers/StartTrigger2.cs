using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrigger2 : MonoBehaviour
{
    [SerializeField]
    Camera _camera = null;

    [SerializeField]
    GameObject _player = null;

    PlayerController _playerController = null;
    CameraController _cameraController = null;

    private void Start()
    {
        _player = GameObject.Find("unitychan");

        _camera = GameObject.Find("Main Camera").GetComponent<Camera>();

        _playerController = _player.GetComponent<PlayerController>();

        _cameraController = _camera.GetComponent<CameraController>();

    }

    //bool _isEntered = false;//플레이어로 ㄱㄱ
    float _prevX;

    private void OnTriggerEnter(Collider other)
    {
        _prevX = other.gameObject.transform.position.x;
        _playerController.JumpLock = true;
    }

    private void OnTriggerExit(Collider other)
    {
        float nowX = other.gameObject.transform.position.x;


        float standard = GetComponent<BoxCollider>().size.x;
        standard = standard > 0 ? standard : -standard;

        bool isThrough = (_prevX - nowX >= standard) || (_prevX - nowX <= -standard);


        _playerController.JumpLock = false;

        Debug.Log($"_prevX {_prevX} nowX {nowX} standard {standard} isThrough {isThrough}");

        if (!isThrough)
        {
            //Debug.Log("no through");
            return;
        }

        _cameraController.SetCameraMode(Define.CameraMode.VerticalHumanView);
        _cameraController.SetCameraDelta(0f, 2.5f, 7.75f);


        if (_playerController.IsEntered)
        {
            _cameraController.StoreMapInfo(d: Managers.Data.MapDict[1].depth, s: Managers.Data.MapDict[1].start,
                w: Managers.Data.MapDict[1].width,
                h: Managers.Data.MapDict[1].height);

            Debug.Log("store prev map1 info");

            _playerController.IsEntered = false;
        }
        else
        {
            _cameraController.StoreMapInfo(d: Managers.Data.MapDict[2].depth, s: Managers.Data.MapDict[2].start, w: Managers.Data.MapDict[2].width,
                h: Managers.Data.MapDict[2].height);

            Debug.Log("store next map2 info");

            _playerController.IsEntered = true;
        }
    }
}
