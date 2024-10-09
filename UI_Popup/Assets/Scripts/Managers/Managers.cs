using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance {  get { Init(); return s_instance; } }


    
    InputManager _input=new InputManager();
    public static InputManager Input { get { return Instance._input; } }

    ResourceManager _resource = new ResourceManager();
    public static ResourceManager Resource { get { return Instance._resource; } }

    UIManager _ui= new UIManager();
    public static UIManager UI { get {return Instance._ui; } }

    SceneManagerEx _scene=new SceneManagerEx();
    public static SceneManagerEx Scene { get { return Instance._scene; } }


    private void Start()
    {
        Init();
    }

    private void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject gameObject = GameObject.Find("@Managers");
            if (gameObject == null)
            {
                gameObject = new GameObject() { name = "@Managers" };
                gameObject.AddComponent<Managers>();
            }

            DontDestroyOnLoad(gameObject);
            s_instance = gameObject.GetComponent<Managers>();

        }
    }

    public static void Clear()
    {
        UI.Clear();
        Resource.Clear();
        Input.Clear();
        Scene.Clear();
    }
}
