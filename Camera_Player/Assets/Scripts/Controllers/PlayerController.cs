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
        Reality, //����
        Virtuality, //����+����
        Puzzle, //�̴ϰ���

    }
    
    
    public PlayerState StatePlayer=PlayerState.Idle;
    PlayerAt _at=PlayerAt.OnGround;
    Rigidbody _rb =null;

    [SerializeField]
    float _speed = 4.0f;

    private void Start()
    {
        _rb=GetComponent<Rigidbody>();

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


    //�浹 �ÿ��� ��ġ�ľ� ����ĳ���� ����
    private void OnCollisionEnter(Collision collision)
    {
        //������ �÷��̾� ��ġ _at�� OnAir�϶�
        if(_at==PlayerAt.OnAir&&isPlayerOn())
        _at = PlayerAt.OnGround;
    }


    //�� ���۰� �� Ʈ����
    private void OnTriggerEnter(Collider other)
    {
        //����� �̸����� ���߿� �±׷�
        if (other.gameObject.name == "StartTrigger")
            //������ �� ��(���)�� EndTrigger���� �޾ƾ���.
        {
            //���� �� ���� �ޱ�
            
        }
        else if(other.gameObject.name == "EndTrigger")
        {

        }
    }


    void OnKeyBoardMove()
    {
        
        if (Input.GetKey(KeyCode.W))
        {
            PlayerAbsoluteRotate(Vector3.forward);

            PlayerAbsoluteMove(Vector3.forward, _speed);

            StatePlayer = PlayerState.Moving;
        }
        if (Input.GetKey(KeyCode.S))
        {
            PlayerAbsoluteRotate(Vector3.back);
            
            PlayerAbsoluteMove(Vector3.back, _speed);

            StatePlayer = PlayerState.Moving;
        }
        if (Input.GetKey(KeyCode.A))
        {
            PlayerAbsoluteRotate(Vector3.left);

            PlayerAbsoluteMove(Vector3.left, _speed);

            StatePlayer = PlayerState.Moving;
        }
        if (Input.GetKey(KeyCode.D))
        {
            PlayerAbsoluteRotate(Vector3.right);

            PlayerAbsoluteMove(Vector3.right, _speed);

            StatePlayer = PlayerState.Moving;
        }
        
    }

    void OnKeyBoardIdle()
    {
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)
            || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
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
                _rb.AddForce(Vector3.up * _speed, ForceMode.Impulse);
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
