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

    public event UnityAction<Vector2> InputEventUpdate;//�۲���ģʽ

    public bool m_Attack;//�Ƿ�ɹ���
    public bool m_Block;//�Ƿ�

    public bool m_Charge;//����
    private int chargeFrmae = 0;//֡����3֡���������

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_Movement.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //InputEventUpdate(m_Movement);//�۲�m_Movement  ÿ֡����

        m_Camera.Set(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse ScrollWheel"));

        if(Input.GetButtonDown("Fire1"))//��������
        {
            m_Attack = true;
            StartCoroutine(AttackWait());//��һ֡��������������ٸ�Ϊfalse
        }
        else if(Input.GetMouseButtonDown(1))//����
        {
            Debug.Log("�Ҽ�");
            m_Block = true;
            StartCoroutine(BlockWait());
        }

        if(Input.GetKey(KeyCode.Q))//��������
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
