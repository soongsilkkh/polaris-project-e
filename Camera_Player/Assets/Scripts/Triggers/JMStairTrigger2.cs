using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JMStairTrigger2 : MonoBehaviour
{
    [SerializeField]
    GameObject _target = null;




    private void Start()
    {
        _target = GameObject.Find("Stair2");
    }

    private void OnTriggerEnter(Collider other)
    {

        _target.SetActive(false);

    }

    private void OnTriggerExit(Collider other)
    {
        _target.SetActive(true);
    }



}
