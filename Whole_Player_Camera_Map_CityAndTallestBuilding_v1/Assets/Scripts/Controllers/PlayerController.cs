using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
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
        Reality, //����
        Virtuality, //����+����
        Puzzle, //�̴ϰ���

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
        //PrintPlayerState();
    }


    //�浹 �ÿ��� ��ġ�ľ� ����ĳ���� ����
    private void OnCollisionEnter(Collision collision)
    {
        //������ �÷��̾� ��ġ _at�� OnAir�϶�
        if(_at==PlayerAt.OnAir&&isPlayerOn())
            _at = PlayerAt.OnGround;

    }


    //test
    bool isEntered = false;
    float prevX;
    //�� ���۰� �� Ʈ����
    private void OnTriggerEnter(Collider other)
    {
        prevX=transform.position.x;
    }

    private void OnTriggerExit(Collider other)
    {
        //������ ����������
        //��� �ǵ�� �ǵ��ư�����
        //����
        float nowX = transform.position.x;
        float resultX=prevX-nowX;
        float standard = other.GetComponent<BoxCollider>().size.x;

        Debug.Log($"prevX - nowX {resultX}, wid {standard}");

        //_cameraController._cameraMode = Define.CameraMode.VerticalHumanView;

        //����� �̸����� ���߿� �±׷�?
        //������ �� ��(���)�� EndTrigger���� �޾ƾ���.

        if (resultX>1.0f||resultX<-1.0f)
        { 
            if (other.gameObject.name == "StartTrigger0")
            {
                //�ӽ� ���� �� ���� �ޱ�

                _cameraController.StoreMapInfo(h: 8, w: 75,
                    s: 20, e: 20 + 75);

                Debug.Log("store next map0 info");

                isEntered = true;

            }
            else if (other.gameObject.name == "EndTrigger0")
            {
                if (isEntered)
                {
                    _cameraController.StoreMapInfo(h: 8, w: 29,
                        e: -13 + 29, s: -13);
                    Debug.Log("store next map1 info");
                    isEntered = false;

                }
                else
                {
                    _cameraController.StoreMapInfo(h: 8, w: 29,
                    s: -48, e: -48 + 29);
                    Debug.Log("store next map0 info");
                    isEntered = true;
                }

            }
            else if (other.gameObject.name == "StartTrigger1")
            {
                if (isEntered)
                {
                    _cameraController.StoreMapInfo(h: 8, w: 29,
                        s: -48, e: -48 + 29);
                    Debug.Log("store next map0 info");
                    isEntered = false;
                }
                else
                {
                    _cameraController.StoreMapInfo(h: 8, w: 29,
                        e: -13 + 29, s: -13);
                    Debug.Log("store next map1 info");
                    isEntered = true;
                }

            }
            else if (other.gameObject.name == "EndTrigger1")
            {
                if (isEntered)
                {
                    _cameraController.StoreMapInfo(h: 8, w: 29,
                        s: 22, e: 22 + 29);
                    Debug.Log("store next map2 info");
                    isEntered = false;
                }
                else
                {
                    _cameraController.StoreMapInfo(h: 8, w: 29,
                        e: -13 + 29, s: -13);
                    Debug.Log("store next map1 info");
                    isEntered = true;
                }
            }
            else if (other.gameObject.name == "StartTrigger2")
            //������ �� ��(���)�� EndTrigger���� �޾ƾ���.
            {
                if (isEntered)
                {
                    _cameraController.StoreMapInfo(h: 8, w: 29,
                        e: -13 + 29, s: -13);
                    Debug.Log("store next map1 info");
                    isEntered = false;
                }
                else
                {
                    _cameraController.StoreMapInfo(h: 8, w: 29,
                        s: 22, e: 22 + 29);
                    Debug.Log("store next map2 info");
                    isEntered = true;
                }

            }
            else if (other.gameObject.name == "EndTrigger2")
            {

                if (isEntered)
                {
                    Debug.Log("test end");
                    isEntered = false;
                }
                else
                {
                    _cameraController.StoreMapInfo(h: 8, w: 29,
                        e: -13 + 29, s: -13);
                    Debug.Log("store next map2 info");
                    isEntered = true;
                }

            }
        }
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
