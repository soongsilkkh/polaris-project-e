using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerController : MonoBehaviour
{
    public enum CameraPos
    {
        minusZ,
        plusZ,
        minusX,
        plusX,
    }


    KeyCode[] relForward = { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D};
    KeyCode[] relBackward = { KeyCode.S, KeyCode.W, KeyCode.D, KeyCode.A};
    KeyCode[] relLeft = { KeyCode.A, KeyCode.D, KeyCode.S, KeyCode.W};
    KeyCode[] relRight = { KeyCode.D, KeyCode.A, KeyCode.W, KeyCode.S};


    public enum PlayerState
    {
        Idle,
        Moving,
        Running,


        Jumping,
        Dead,
    }

    public enum PlayerAt
    {
        OnGround,
        OnAir,

    }
    public enum PlayerVerticalMovement
    {
        Idle, Forward, Back
    }

    public enum PlayerHorizontalMovement
    {
        Idle, Left, Right
    }


    public CameraPos PosCamera = CameraPos.minusZ;
   
    public PlayerState StatePlayer = PlayerState.Idle;
    public PlayerAt AtPlayer = PlayerAt.OnGround;

    public PlayerVerticalMovement PlayerVertMove = PlayerVerticalMovement.Idle;
    public PlayerHorizontalMovement PlayerHorizonMove = PlayerHorizontalMovement.Idle;

    public bool MoveLock = false;
    public bool JumpLock = false;

    public bool IsEntered = false;


    [SerializeField]
    float _speed = 2.5f;


    Rigidbody _rb = null;

    Dictionary<int, PlayerMove> _playerMoveDataDict = null;

    PlayerMove _playerMove = null;

    private void Start()
    {
        _rb=GetComponent<Rigidbody>();

        Managers.Input.KeyAction -= this.OnKeyBoardJump;
        Managers.Input.KeyAction += this.OnKeyBoardJump;

        Managers.Input.KeyAction += this.OnKeyBoardIdle;
        Managers.Input.KeyAction += this.OnKeyBoardIdle;

        Managers.Input.KeyAction -= this.OnKeyBoardAccelerate;
        Managers.Input.KeyAction += this.OnKeyBoardAccelerate;

        Managers.Input.MoveKeyAction-= this.OnKeyBoardMove;
        Managers.Input.MoveKeyAction+= this.OnKeyBoardMove;

        _playerMoveDataDict =  Managers.Data.PlayerMoveDict;
        
        _playerMove=_playerMoveDataDict[0];

        GetComponent<CapsuleCollider>().material = Managers.Resource.Load<PhysicMaterial>("Art/nofriction");
    }

    private void Update()
    {
        //PrintPlayerState();
    }


    //충돌 시에만 위치파악 레이캐스팅 동작
    private void OnCollisionEnter(Collision collision)
    {
        //마지막 플레이어 위치 _at이 OnAir일때
        if(AtPlayer == PlayerAt.OnAir&&isPlayerOn())
            AtPlayer = PlayerAt.OnGround;

    }

    

    void OnKeyBoardMove()
    {
        if (!MoveLock) 
        {
            if (Input.GetKey(relForward[(int)PosCamera]))
            {
                if (PlayerVertMove != PlayerVerticalMovement.Back)
                {
                    PlayerVertMove = PlayerVerticalMovement.Forward;
                    PlayerAbsoluteRotate(Vector3.forward);

                    if (Input.GetKey(KeyCode.LeftShift))
                        StatePlayer = PlayerState.Running;
                    else
                        StatePlayer = PlayerState.Moving;


                    PlayerAbsoluteMove(Vector3.forward, _speed);
                }
            }
            if (Input.GetKey(relBackward[(int)PosCamera]))
            {
                if (PlayerVertMove != PlayerVerticalMovement.Forward)
                {
                    PlayerVertMove = PlayerVerticalMovement.Back;
                    PlayerAbsoluteRotate(Vector3.back);

                    if (Input.GetKey(KeyCode.LeftShift))
                        StatePlayer = PlayerState.Running;
                    else
                        StatePlayer = PlayerState.Moving;


                    PlayerAbsoluteMove(Vector3.back, _speed);
                }
            }


            if (Input.GetKey(relLeft[(int)PosCamera]))
            {
                if (PlayerHorizonMove != PlayerHorizontalMovement.Right)
                {
                    PlayerHorizonMove = PlayerHorizontalMovement.Left;
                    PlayerAbsoluteRotate(Vector3.left);

                    if (Input.GetKey(KeyCode.LeftShift))
                        StatePlayer = PlayerState.Running;
                    else
                        StatePlayer = PlayerState.Moving;


                    PlayerAbsoluteMove(Vector3.left, _speed);
                }
            }
            if (Input.GetKey(relRight[(int)PosCamera]))
            {
                if (PlayerHorizonMove != PlayerHorizontalMovement.Left)
                {
                    PlayerHorizonMove = PlayerHorizontalMovement.Right;
                    PlayerAbsoluteRotate(Vector3.right);

                    if (Input.GetKey(KeyCode.LeftShift))
                        StatePlayer = PlayerState.Running;
                    else
                        StatePlayer = PlayerState.Moving;

                    PlayerAbsoluteMove(Vector3.right, _speed);
                }
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
        if (!JumpLock)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (AtPlayer == PlayerAt.OnGround)
                {
                    AtPlayer = PlayerAt.OnAir;

                    if( StatePlayer==PlayerState.Running )
                        _rb.AddForce(Vector3.up * _playerMove.acceljump, ForceMode.Impulse);
                    else
                        _rb.AddForce(Vector3.up * _playerMove.jump, ForceMode.Impulse);
                }
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (isPlayerOn())
                    AtPlayer = PlayerAt.OnGround;
            }
        }

    }

    void OnKeyBoardAccelerate()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _speed = _playerMove.accelspeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _speed = _playerMove.speed;
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
