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

    TriggerPass _triggerPass = null;

    private void Start()
    {
        _player = GameObject.Find("unitychan");

        _camera = GameObject.Find("Main Camera").GetComponent<Camera>();

        _playerController = _player.GetComponent<PlayerController>();

        _cameraController = _camera.GetComponent<CameraController>();

    }


    private void OnTriggerEnter(Collider other)
    {
        _triggerPass = new TriggerPass(GetComponent<BoxCollider>().size.x);
        _triggerPass.Begin(other.gameObject.transform.position.x);
        _playerController.JumpLock = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _triggerPass.End(other.gameObject.transform.position.x);


        _playerController.JumpLock = false;


        if (!_triggerPass.IsPass())
        {
            //Debug.Log("no through");
            return;
        }

        _triggerPass = null;

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
