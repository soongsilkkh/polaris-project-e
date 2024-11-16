using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallViewTriggerTest : MonoBehaviour
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

        //테스트
        //_cameraController.SetCameraDelta(0f, 5f, 10f);

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
            return;

        
        StartCoroutine("COReleasePlayerMoveLock", 0.75f);


        //_cameraController.SetCameraMode(Define.CameraMode.VerticalQuaterView);
        _cameraController.SetCameraMode(Define.CameraMode.HorizontalHallView);
        _cameraController.SetCameraDelta(-3f, 2f, 0f);

        //_playerController.PosCamera = PlayerController.CameraPos.minusX;

    }


    IEnumerator COReleasePlayerMoveLock(float seconds)
    {
        _playerController.MoveLock = true;

        yield return new WaitForSeconds(seconds);

        _playerController.MoveLock = false;
    }
}
