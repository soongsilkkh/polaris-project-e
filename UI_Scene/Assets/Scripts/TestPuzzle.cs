using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPuzzle : MonoBehaviour
{
    bool[] _answer = null;

    UI_Scene pic1UI=null;
    UI_Scene pic2UI=null;
    UI_Scene pic3UI=null;
    UI_Scene pannelUI=null;

    UI_Scene picturePuzzleUI=null;

    GameObject _pannel = null;
    UI_PannelPuzzlePannel _pannelScript = null;

    enum PannelPuzzleStatus
    {
        None,
        Pic1, Pic2, Pic3,
        Pannel,
        PicturePuzzle
    }

    PannelPuzzleStatus status = PannelPuzzleStatus.None;

    private void Init()
    {
        /*pic1UI = Managers.UI.GenerateSceneUI<UI_PannelPuzzlePic>("UI_PannelPuzzlePic1");
        pic2UI = Managers.UI.GenerateSceneUI<UI_PannelPuzzlePic>("UI_PannelPuzzlePic2");
        pic3UI = Managers.UI.GenerateSceneUI<UI_PannelPuzzlePic>("UI_PannelPuzzlePic3");*/

        pannelUI = Managers.UI.GenerateSceneUI<UI_PannelPuzzlePannel>("UI_PannelPuzzlePannel");
        

        _pannel = GameObject.Find("UI_PannelPuzzlePannel");
        _pannelScript = _pannel.GetComponent<UI_PannelPuzzlePannel>();

        Managers.UI.HideSceneUI(pannelUI);



        
              


        _answer = new bool[] 
        {
            false,false,false, true,
            true, true, false, true,
            false,false,false,false,
            true, false,false, true
        };


        Managers.Input.KeyAction -= OnKeyBoard;
        Managers.Input.KeyAction += OnKeyBoard;
    }

    void Start()
    {
        Init();
    }

    void OnKeyBoard()
    {
        if (Managers.Puzzle.St1PuzzlePannel)
            return;

        switch (status)
        {
            case PannelPuzzleStatus.PicturePuzzle:
                {
                    if (Input.GetKeyDown(KeyCode.Alpha5))
                    {
                        Managers.UI.DestroySceneUI(picturePuzzleUI);
                        status = PannelPuzzleStatus.None;
                    }
                }
                break;
            case PannelPuzzleStatus.Pannel:
                {
                    if (Input.GetKeyDown(KeyCode.Alpha4))
                    {
                        if (CheckAnswer()) 
                        {
                            Managers.UI.DestroySceneUI(pannelUI);
                            Debug.Log("you got it!!!!!!!!!");
                            status = PannelPuzzleStatus.None;
                            Managers.Puzzle.St1PuzzlePannel = true;
                        }
                        else
                        {
                            Managers.UI.HideSceneUI(pannelUI);
                            status = PannelPuzzleStatus.None;
                        }
                        
                    }
                }
                break;
            case PannelPuzzleStatus.Pic1:
                {
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        Managers.UI.DestroySceneUI(pic1UI);
                        status = PannelPuzzleStatus.None;
                    }
                }
                break;
            case PannelPuzzleStatus.Pic2:
                {
                    if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        Managers.UI.DestroySceneUI(pic2UI);
                        status = PannelPuzzleStatus.None;
                    }
                }
                break;
            case PannelPuzzleStatus.Pic3:
                {
                    if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        Managers.UI.DestroySceneUI(pic3UI);
                        status = PannelPuzzleStatus.None;
                    }
                }
                break;
            case PannelPuzzleStatus.None:
                {
                    if (Input.GetKeyDown(KeyCode.Alpha5))
                    {
                        picturePuzzleUI = Managers.UI.GenerateSceneUI<UI_PicturePuzzle>("UI_PicturePuzzle");
                        status = PannelPuzzleStatus.PicturePuzzle;
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha4))
                    {
                        Managers.UI.ShowSceneUI(pannelUI);
                        status = PannelPuzzleStatus.Pannel;
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        pic1UI = Managers.UI.GenerateSceneUI<UI_PannelPuzzlePic>("UI_PannelPuzzlePic1");
                        status = PannelPuzzleStatus.Pic1;
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        pic2UI = Managers.UI.GenerateSceneUI<UI_PannelPuzzlePic>("UI_PannelPuzzlePic2");
                        status = PannelPuzzleStatus.Pic2;
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        pic3UI = Managers.UI.GenerateSceneUI<UI_PannelPuzzlePic>("UI_PannelPuzzlePic3");
                        status = PannelPuzzleStatus.Pic3;
                    }
                }
                break;
            default:
                break;
        }

    }

    bool CheckAnswer()
    {
        bool result = true;
        for (int i = 0; i < _pannelScript.PannelStatus.Length; i++)
        {
            if (_answer[i] != _pannelScript.PannelStatus[i])
            {
                result = false;
                break;
            }
        }

        return result;
    }
}
