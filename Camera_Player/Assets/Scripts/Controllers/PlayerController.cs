using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        Moving,



        Jumping,
        Dead,
    }

    public enum PlayerAt
    {
        OnGround,
        OnAir,

    }

    
    public enum PlayerMode
    {
        Reality, //현실
        Virtuality, //현실+가상
        Puzzle, //미니게임

    }
    
    
    public PlayerState StatePlayer=PlayerState.Idle;
    PlayerAt _at=PlayerAt.OnGround;
    Rigidbody _rb =null;

    [SerializeField]
    float _speed = 4.0f;


    public enum PlayerVerticalMovement
    {
        Idle, Forward, Back
    }
    public enum PlayerHorizontalMovement
    {
        Idle, Left, Right
    }
    public PlayerVerticalMovement PlayerVertMove = PlayerVerticalMovement.Idle;
    public PlayerHorizontalMovement PlayerHorizonMove = PlayerHorizontalMovement.Idle;


    [SerializeField]
    Camera _camera = null;
    CameraController _cameraController = null;

    private void Start()
    {
        _rb=GetComponent<Rigidbody>();
        _cameraController=_camera.GetComponent<CameraController>();

        Managers.Input.KeyAction -= this.OnKeyBoardJump;
        Managers.Input.KeyAction += this.OnKeyBoardJump;

        Managers.Input.KeyAction += this.OnKeyBoardIdle;
        Managers.Input.KeyAction += this.OnKeyBoardIdle;

        Managers.Input.MoveKeyAction-= this.OnKeyBoardMove;
        Managers.Input.MoveKeyAction+= this.OnKeyBoardMove;

    }

    private void Update()
    {
        PrintPlayerState();
    }


    //충돌 시에만 위치파악 레이캐스팅 동작
    private void OnCollisionEnter(Collision collision)
    {
        //마지막 플레이어 위치 _at이 OnAir일때
        if(_at==PlayerAt.OnAir&&isPlayerOn())
            _at = PlayerAt.OnGround;

    }


    //test

    //맵 시작과 끝 트리거
    private void OnTriggerEnter(Collider other)
    {
        //현재는 이름으로 나중엔 태그로?
        //원래는 전 맵(장소)의 EndTrigger에서 받아야함.


        if (other.gameObject.name == "StartTrigger0")
        {
            //임시 현재 맵 정보 받기
            
            _cameraController.StoreMapInfo(h: 8, w: 29,
                s: -48, e:-48 + 29);


        }
        else if(other.gameObject.name == "EndTrigger0")
        {

            _cameraController.StoreMapInfo(h: 8, w: 29,
                e: -13 + 29, s: -13);
            Debug.Log("store next map info");
            
        }
        else if (other.gameObject.name == "StartTrigger1")
        {




        }
        else if (other.gameObject.name == "EndTrigger1")
        {

            _cameraController.StoreMapInfo(h: 8, w: 29,
                s: 22, e: 22 + 29);
            Debug.Log("store next map info");

        }
        else if (other.gameObject.name == "StartTrigger2")
        //원래는 전 맵(장소)의 EndTrigger에서 받아야함.
        {




        }
        else if (other.gameObject.name == "EndTrigger2")
        {


            Debug.Log("test end");

        }
    }

    private void OnTriggerExit(Collider other)
    {

        _cameraController._cameraMode = Define.CameraMode.VerticalHumanView;
    }

    
    

    void OnKeyBoardMove()
    {
        
        if (Input.GetKey(KeyCode.W))
        {
            if (PlayerVertMove != PlayerVerticalMovement.Back)
            {
                PlayerVertMove = PlayerVerticalMovement.Forward;

                PlayerAbsoluteRotate(Vector3.forward);

                PlayerAbsoluteMove(Vector3.forward, _speed);

                StatePlayer = PlayerState.Moving;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (PlayerVertMove != PlayerVerticalMovement.Forward)
            {
                PlayerVertMove = PlayerVerticalMovement.Back;

                PlayerAbsoluteRotate(Vector3.back);

                PlayerAbsoluteMove(Vector3.back, _speed);

                StatePlayer = PlayerState.Moving;
            }
        }


        if (Input.GetKey(KeyCode.A))
        {
            if (PlayerHorizonMove != PlayerHorizontalMovement.Right)
            {
                PlayerHorizonMove = PlayerHorizontalMovement.Left;
                PlayerAbsoluteRotate(Vector3.left);

                PlayerAbsoluteMove(Vector3.left, _speed);

                StatePlayer = PlayerState.Moving;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (PlayerHorizonMove != PlayerHorizontalMovement.Left)
            {
                PlayerHorizonMove = PlayerHorizontalMovement.Right;

                PlayerAbsoluteRotate(Vector3.right);

                PlayerAbsoluteMove(Vector3.right, _speed);

                StatePlayer = PlayerState.Moving;
            }
        }
        
    }

    void OnKeyBoardIdle()
    {
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)
            || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            PlayerVertMove = PlayerVerticalMovement.Idle;
            PlayerHorizonMove = PlayerHorizontalMovement.Idle;
            StatePlayer = PlayerState.Idle;
        }
    }

    void OnKeyBoardJump()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_at == PlayerAt.OnGround)
            {
                _at = PlayerAt.OnAir;
                _rb.AddForce(Vector3.up * _speed*1.5f, ForceMode.Impulse);
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if(isPlayerOn())
                _at = PlayerAt.OnGround;
        }

    }

    void PlayerAbsoluteRotate(Vector3 direct)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation,
               Quaternion.LookRotation(direct), 0.2f);
    }

    void PlayerAbsoluteMove(Vector3 direct, float speed)
    {
        transform.position += direct * Time.deltaTime * speed;
    }


    bool isPlayerOn()
    {

        bool result = Physics.Raycast(transform.position+Vector3.up*0.1f, Vector3.down, 0.2f);
        
        return result;
        
    }

    bool isPlayerOn(LayerMask layer)
    {
        return Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, 0.2f,layer);
    }

    string[] _names = Enum.GetNames(typeof(PlayerState));
    void PrintPlayerState()
    {
        Debug.Log($"player at {_names[(int)StatePlayer]}");
    }
}
