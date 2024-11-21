using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JMStairTrigger4 : MonoBehaviour
{
    [SerializeField]
    GameObject _target = null;

    Renderer _targetRenderer = null;

    Color _targetColor;


    private void Start()
    {
        _target = GameObject.Find("Stair4");

        _targetRenderer = _target.GetComponent<Renderer>();


        _targetColor=_targetRenderer.material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        Color newColor = new Color(_targetColor.r, _targetColor.g, _targetColor.b,0.1f);
        
        _targetRenderer.material.color = newColor;
    }

    private void OnTriggerExit(Collider other)
    {
        _targetRenderer.material.color = _targetColor;
    }



}
