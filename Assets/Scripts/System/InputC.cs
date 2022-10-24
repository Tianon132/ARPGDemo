using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputC : MonoBehaviour
{
    public static InputC Instance;
    private void Awake()
    {
        Instance = this;
    }

    public Vector2 m_Movement;
    public Vector3 m_Camera;

    public event UnityAction<Vector2> InputEventUpdate;//观察者模式

    public bool m_Attack;//是否可攻击
    public bool m_Block;//是否

    public bool m_Charge;//蓄力
    private int chargeFrmae = 0;//帧数，3帧后蓄力完成

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_Movement.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //InputEventUpdate(m_Movement);//观察m_Movement  每帧调用

        m_Camera.Set(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse ScrollWheel"));

        if(Input.GetButtonDown("Fire1"))//攻击触发
        {
            m_Attack = true;
            StartCoroutine(AttackWait());//下一帧调用这个函数，再改为false
        }
        else if(Input.GetMouseButtonDown(1))//隔档
        {
            Debug.Log("右键");
            m_Block = true;
            StartCoroutine(BlockWait());
        }

        if(Input.GetKey(KeyCode.Q))//蓄力技能
        {
            chargeFrmae++;
            if(chargeFrmae>3)
            {
                m_Charge = true;
            }
        }
        else
        {
            m_Charge = false;
        }

        if(Input.GetKeyDown(KeyCode.B))
        {
            UIC.Instance.OpenUI("backpack");
        }
    }

    IEnumerator AttackWait()
    {
        yield return 0;
        m_Attack = false;
    }

    IEnumerator BlockWait()
    {
        yield return 0;
        m_Block = false;
    }
}
