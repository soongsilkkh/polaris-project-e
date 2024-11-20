
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_PannelPuzzlePannel : UI_Scene
{
    public bool[] PannelStatus = new bool[(int)GameObjects.Count];

    enum GameObjects
    {
        Image0, Image1, Image2, Image3,
        Image4, Image5, Image6, Image7,
        Image8, Image9, Image10, Image11,
        Image12, Image13, Image14, Image15,
        Count
    }

    GameObject[] pannels = new GameObject[(int)GameObjects.Count];

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        transform.GetComponent<Canvas>().sortingOrder = 1;

        base.Bind<GameObject>(typeof(GameObjects));

        

        for (int i = 0; i < (int)GameObjects.Count; i++)
        {
            PannelStatus[i] = false;
            pannels[i] = base.Get<GameObject>(i);
            UI_Base.AddUIEvent(pannels[i], OnPannelClicked, Define.UIEvent.Click);
        }
        
    }

    void OnPannelClicked(PointerEventData data)
    {

        GameObject target = data.pointerClick.gameObject;
        if (target == null)
            return;

        int index=-1;

        for(int i = 0; i < (int)GameObjects.Count; i++)
        {
            if (target == pannels[i])
            {
                index = i;
                break;
            }

        }

        if (index == -1)
            return;


        Image image=target.GetComponent<Image>();


        if (!PannelStatus[index])
        {
            PannelStatus[index]=true;
            image.color = Color.red;
        }
        else
        {
            PannelStatus[index] = false;
            image.color = Color.white;
        }

    }
}
