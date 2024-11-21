using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JMStairTrigger0 : MonoBehaviour
{
    [SerializeField]
    GameObject _target=null;

    Renderer _targetRenderer=null;

    Color _targetColor;


    private void Start()
    {
        _target = GameObject.Find("Stair0");
        _targetRenderer = _target.GetComponent<Renderer>();

        _targetColor = Color.white;
    }



    float r = 1;
    float g = 1;
    float b = 0;
    private void OnTriggerStay(Collider other)
    {
        r = Mathf.Lerp(r,0f, Time.deltaTime * 1f);
        g = Mathf.Lerp(g, 0f, Time.deltaTime * 2.5f);
        b = Mathf.Lerp(b, 1f, Time.deltaTime * 3f);
        //_targetColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        _targetColor=new Color(r,g,b);
        _targetRenderer.material.color = _targetColor;
    }

    //IEnumerator COColorChange(float)
}
