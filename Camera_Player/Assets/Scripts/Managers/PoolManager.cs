using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Diagnostics;

public class PoolManager
{
    //사운드 매니저처럼 자신이 로드한 것들을 
    //계속 들고 있게(풀링)(캐싱)하게 할것
    Transform _root;
    //Transform이 아닌 GameObject 자료형으로도 들고 있어도 된다.

    //_root 밑에 모오든 풀링된 오브젝트들을 무분별하게 갖지 않고
    //분류하여 관리하자.

    //Hierarchy탭에서는 
    //@Pool_Root
    //ㄴTank_Root
    //  ㄴTank1
    //  ㄴTank2
    //ㄴUnityChan_Root
    //  ㄴUnityChan1
    // ... push pop이 @Pool_Root go를 대상으로 진행
    // @Pool_Root go는 풀매니저가 된다는 뜻.


    //위에 계층을 표현할 자료구조(Dictionary)를 선택
    //그리고 각 카테고리(Tank_Root,UnityChan_Root,,,)를 나타낼
    //중간 단위 필요.

    class Pool//카테고리화 할 중간 단위 == Pool 클래스
    {
        //본인의 카테고리에 들어있는 것들만을 관리

        public GameObject Original { get; private set; }
        //찍어내는 게임 오브젝트를 만들기 위한 프리팹 '틀'

        public Transform Root { get; set; }
        //Tank_Root, UnityChan_Root처럼 각 카테고리의 루트 GO 저장 변수.

        //본인 카테고리의
        //찍어낸 게임 오브젝트들을 관리할 자료구조
        Stack<Poolable> _poolStack = new Stack<Poolable>();

        public void Init(GameObject original, int count)
        {
            Original = original;//외부에 이닛할때 변수에 저장.
            Root = new GameObject().transform;
            Root.name = $"{original.name}_Root";

            for (int i = 0; i < count; i++)
            {//정한 갯수만큼 미리 만들고 산하에 배치
                //만들고 "끈다음" stack에 푸쉬

                Push(Create());
                //생성하고 세팅하고 푸쉬
            }
        }

        Poolable Create()
        //산하에 들어갈 실제 객체 만들고 반환
        {
            GameObject go = Object.Instantiate<GameObject>(Original);
            go.name = Original.name;

            //생성된 객체에 poolable 컴포넌트 추가
            return Util.GetOrAddComponent<Poolable>(go);
        }

        public void Push(Poolable poolable)//인터페이스 같게
        {
            if (poolable == null)
                return;

            poolable.transform.parent = Root;
            //Root 산하에 배치 먼저
            poolable.gameObject.SetActive(false);
            poolable.IsUsing = false;
            //끄고 상태도 기록

            _poolStack.Push(poolable);
            //관리 구조에 추가
        }

        //기존 Root에서 보관되는 (스택에서 보관되는)
        //poolable을 꺼내고 액티브시키고
        //새로운 parent에 배치하는 동작
        public Poolable Pop(Transform parent)//인터페이스 비슷하게
        {
            Poolable poolable = null;

            if (_poolStack.Count > 0)
                poolable = _poolStack.Pop();
            else
                poolable = Create();

            poolable.gameObject.SetActive(true);

            //parent 설정.
            //parent가 DontDestroyOnLoad면 스스로 못 빠져 나온다
            //단 아닌 쪽에서 한번이라도 parent가 된다면 빠져나옴.
            //DontDestroyOnLoad해제 용도로 
            //항상 씬에 있는 @Scene 을 parent로 지정해주자 
            if (parent == null)
                poolable.transform.parent = Managers.Scene.CurrentScene.transform;

            poolable.transform.parent = parent;
            poolable.IsUsing = true;

            return poolable;
        }
    }
    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();
    //<카테고리 이름(문자열), 중간 단위 pool 타입> 구조 사용.

    public void Init()
    {
        if (_root == null)
        {
            _root = new GameObject { name = "@Pool_Root" }.transform;

            Object.DontDestroyOnLoad(_root);
            //동적으로 자동 삭제 안됨

            //풀링된 오브젝트는 _root밑으로 이동 후 관리.
        }
    }

    //pool(카테고리)자체 만들고 추가하기
    public void CreatePool(GameObject original, int count)
    {//pool 클래스의 함수들 활용

        Pool pool = new Pool();
        pool.Init(original, count);

        pool.Root.parent = _root;
        //카테고리 ~~_Root를 맨위root 산하에 배치

        _pool.Add(original.name, pool);
    }



    //보통 풀링하는 매니저는 push pop이라는 용어를 많이 사용한다.
    //큐처럼 구현할 예정.

    //씬에서 다 사용한 다음에
    //다시 집어 넣는 동작하는 함수.
    public void Push(Poolable poolable)//인터페이스 같게
    {//poolable을 가진 애들만 통하게 인자 자료형 설정.

        //컴포넌트에서 오브젝트로 접근후 이름 추출
        string name = poolable.gameObject.name;

        //애초에 dict 구조에 등록이 안돼 있을 경우
        if (_pool.ContainsKey(name) == false)
        {
            GameObject.Destroy(poolable.gameObject);
            return;
        }

        _pool[name].Push(poolable);
        //세팅되고 카테고리에 맞는 스택에 저장됨.
    }


    //사용하기 위해서 오브젝트를 꺼내는 함수
    //리소스 매니저의 instantiate함수의 객체화 부분을 지원할 함수
    //go=Object.Instantiate(프리팹 원본,연결할 부모);부분
    //dict 구조에 등록돼 있지 않은 경우에도 자동으로 등록하고 ***
    //그다음에 팝하는 동작을 한다. 
    public Poolable Pop(GameObject original, Transform parent = null)
    {//poolable을 가진 애들만 통하게 인자 자료형 설정.
     //인터페이스 같게

        //pool 클래스의 함수 래핑

        //없으면 Pool(카테고리)자체를 만들고 추가.
        if (_pool.ContainsKey(original.name) == false)
            CreatePool(original, 3);

        Pool pool = _pool[original.name];
        //orignal이름에 해당하는 pool 형 반환
        Poolable poolable = pool.Pop(parent);
        //반환된 객체의 stack에서 보관한 객체하나
        //새로운 부모에 연결 후 꺼내기.

        return poolable;
    }


    //리소스 매니저의 Instantiate함수에 사용되는
    //Load함수를 지원하는 함수
    //본격적인 로드 동작 전에 호출될 함수.
    public GameObject GetOriginal(string name)
    {
        if (_pool.ContainsKey(name) == false)
            return null;
        //없으니까 null반환

        return _pool[name].Original;
        //보관장소에서 접근하여 반환
    }

    //클리어하는 타이밍을 정해야함,
    //씬이 넘어가도 유지할지 아니면
    //캐싱한 것을 날려야할지.
    public void Clear()
    {
        //대부분의 게임에서는 사실 날릴 필요 없지만
        //큰 규모 게임인 경우, 지역마다 쓰는 오브젝트가 드를 경우,

        //모든 카테고리가 pool root 산하에 있으니까
        //pool root에 접근하고 카테고리 순회하며 해제해주자.

        foreach (Transform child in _root)
        {
            //카테고리 자체를 삭제
            GameObject.Destroy(child.gameObject);
            //카테고리 안에 들어있는 poolable컴포넌트를 가진 go한번에 삭제됨.
        }

        _pool.Clear();
        //dict 구조 클리어
        //dict 구조 안에 들어있는 stack들도 삭제됨.
    }
}
