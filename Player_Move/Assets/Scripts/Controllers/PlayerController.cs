using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        Moving,
    }

    PlayerState _state=PlayerState.Idle;

    [SerializeField]
    float _speed = 7.0f;

    private void Start()
    {
        Managers.Input.KeyAction -= this.OnKeyBoard;
        Managers.Input.KeyAction += this.OnKeyBoard;
    }

    private void Update()
    {
        PrintPlayerState();
    }


    //test
    //bool _isPlayerOnAir = false;
    //아마 바닥 게임오브젝트의 레이어를 지정하고
    //해당 레이어 마스크와 충돌했을 때 바닥이라고 인지시켜야할듯


    void OnKeyBoard()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, 
                Quaternion.LookRotation(Vector3.forward), 0.2f);

            transform.position += Vector3.forward * Time.deltaTime * _speed;

            _state = PlayerState.Moving;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(Vector3.back), 0.2f);
            
            transform.position += Vector3.back * Time.deltaTime * _speed;

            _state = PlayerState.Moving;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, 
                Quaternion.LookRotation(Vector3.left), 0.2f);
            
            transform.position += Vector3.left * Time.deltaTime * _speed;

            _state = PlayerState.Moving;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, 
                Quaternion.LookRotation(Vector3.right), 0.2f);
            
            transform.position += Vector3.right * Time.deltaTime * _speed;

            _state = PlayerState.Moving;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            //test
            if (transform.position.y < 1.5)
            {
                transform.position += Vector3.up*Time.deltaTime*12;

                _state = PlayerState.Moving;
            }
                
        }




        if (Input.GetKeyUp(KeyCode.A)||Input.GetKeyUp(KeyCode.D)
            ||Input.GetKeyUp(KeyCode.W)||Input.GetKeyUp(KeyCode.S)
            ||Input.GetKeyUp(KeyCode.Space))
        {
            _state = PlayerState.Idle;
        }
    }

    void PrintPlayerState()
    {
        if (_state == PlayerState.Moving)
        {
            Debug.Log("player state moving");
        }
        else if (_state == PlayerState.Idle)
        {
            Debug.Log("player state idle");
        }
        return;
    }
}
