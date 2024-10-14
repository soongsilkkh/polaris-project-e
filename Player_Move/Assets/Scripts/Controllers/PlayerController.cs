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
    
    
    PlayerState _state=PlayerState.Idle;
    PlayerAt _at=PlayerAt.OnGround;
    Rigidbody _rb =null;

    [SerializeField]
    float _speed = 4.0f;

    private void Start()
    {
        _rb=GetComponent<Rigidbody>();

        Managers.Input.KeyAction -= this.OnKeyBoardJump;
        Managers.Input.KeyAction += this.OnKeyBoardJump;

        Managers.Input.MoveKeyAction-= this.OnKeyBoardMove;
        Managers.Input.MoveKeyAction+= this.OnKeyBoardMove;

    }

    private void Update()
    {
        //리소스 너무 낭비됨.
        /*
        if (isPlayerOn())
        {
            _at=PlayerAt.OnGround;
        }
        else
        {
            _at = PlayerAt.OnAir;
        }
        */

        /*
        if (_at == PlayerAt.OnAir)
        {
            if(isPlayerOn())
                _at=PlayerAt.OnGround;
        }
        */



        PrintPlayerState();
    }


    //충돌 시에만 위치파악 레이캐스팅 동작
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"collision {collision.transform.name}");


        //마지막 플레이어 위치 _at이 OnAir일때
        if(_at==PlayerAt.OnAir&&isPlayerOn())
        _at = PlayerAt.OnGround;
    }



    void OnKeyBoardMove()
    {

        if (Input.GetKey(KeyCode.W))
        {
            PlayerAbsoluteRotate(Vector3.forward);

            PlayerAbsoluteMove(Vector3.forward, _speed);

                _state = PlayerState.Moving;
        }
        if (Input.GetKey(KeyCode.S))
        {
            PlayerAbsoluteRotate(Vector3.back);
            
            PlayerAbsoluteMove(Vector3.back, _speed);

                _state = PlayerState.Moving;
        }
        if (Input.GetKey(KeyCode.A))
        {
            PlayerAbsoluteRotate(Vector3.left);

            PlayerAbsoluteMove(Vector3.left, _speed);

                _state = PlayerState.Moving;
        }
        if (Input.GetKey(KeyCode.D))
        {
            PlayerAbsoluteRotate(Vector3.right);

            PlayerAbsoluteMove(Vector3.right, _speed);

                _state = PlayerState.Moving;
        }

        


        if (Input.GetKeyUp(KeyCode.A)||Input.GetKeyUp(KeyCode.D)
            ||Input.GetKeyUp(KeyCode.W)||Input.GetKeyUp(KeyCode.S))
        {
                _state = PlayerState.Idle;
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
        //Debug.DrawRay(transform.position + Vector3.up * 10f, Vector3.down,Color.red);


        bool result = Physics.Raycast(transform.position+Vector3.up*0.1f, Vector3.down, 0.2f);
        
        return result;
        
    }

    bool isPlayerOn(LayerMask layer)
    {
        return Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, 0.2f,layer);
    }


    //test
    string[] _names = Enum.GetNames(typeof(PlayerAt));
    void PrintPlayerState()
    {
        Debug.Log($"player at {_names[(int)_at]}");
    }
}
