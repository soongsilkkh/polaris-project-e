using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_PicturePuzzle : UI_Scene
{
    enum InputFields
    {
        InputField,
    }

    TMP_InputField _inputField=null;

    public override void Init()
    {
        base.Init();

        transform.GetComponent<Canvas>().sortingOrder = 1;
        
        base.Bind<TMP_InputField>(typeof(InputFields));

        _inputField=base.Get<TMP_InputField>((int)InputFields.InputField);

        _inputField.onEndEdit.AddListener(OnEndEditFunc);
        _inputField.onSelect.AddListener(OnSelectFunc);
        _inputField.onDeselect.AddListener(OnDeSelectFunc);
    }

    void Start()
    {
        Init();
    }

    public void OnEndEditFunc(string text)
    {
        Debug.Log($"{text}");

        if (text == "green")
            _inputField.text = "correct!!";
        else
            _inputField.text = "wrong";
    }

    public void OnSelectFunc(string text)
    {
        Debug.Log("on select!");
    }

    public void OnDeSelectFunc(string text)
    {
        Debug.Log("on Deselect!");
    }
}
