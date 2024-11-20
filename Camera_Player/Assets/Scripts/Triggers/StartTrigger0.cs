using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StartTrigger0 : MonoBehaviour
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
        //Å×½ºÆ®
        //_cameraController.SetCameraDelta(0f, 5f, 10f);
        _triggerPass = new TriggerPass(GetComponent<BoxCollider>().size.x);
        _triggerPass.Begin(other.gameObject.transform.position.x);

        _playerController.JumpLock = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _triggerPass.End(other.gameObject.transform.position.x);


        _playerController.JumpLock = false;


        if (!_triggerPass.IsPass())
            return;

        _triggerPass = null;

        _cameraController.SetCameraMode(Define.CameraMode.VerticalHumanView);
        _cameraController.SetCameraDelta(0f, 2.5f, 7.75f);


        if (_playerController.IsEntered)
        {
            _cameraController.StoreMapInfo();

            Debug.Log("store none");

            _playerController.IsEntered = false;
        }
        else
        {

            _cameraController.StoreMapInfo(d: Managers.Data.MapDict[0].depth,s: Managers.Data.MapDict[0].start, 
                w: Managers.Data.MapDict[0].width, h: Managers.Data.MapDict[0].height);

            Debug.Log("store next map0 info");

            _playerController.IsEntered = true;
        }
    }
}
