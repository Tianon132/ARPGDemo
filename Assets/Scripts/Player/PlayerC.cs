using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerC : PlayerAttribute, IFight
{
    public static PlayerC Instance;
    private void Awake()
    {
        Instance = this;
    }

    private CharacterController controller;//��ɫ�ƶ�����
    public float walkSpeed = 4.5f;

    public InputC m_Input;
    public PlayerAni Ani;

    //��������
    public bool canAttack = true;
    public bool canBlock = true;
    public float blockTime = 0;

    //�ͷż���
    private float addSpeed = 1;//�ۼ��ٶ�
    private float skillAdd = 3;//������ʱ�䳤��
    private float skillAddMax = 3;
    private float skillAddMin = 1;

    private bool ���ܲ����ƶ� = false;
    private bool ���ܲ��ܹ��� = false;
    private bool ���ܲ��ܲ��� = false;

    private int ��ǰ�ͷż��� = 0;
    private float skillLastTime = 3.5f;

    private void OnEnable()//�ű���Ч��������
    {
        //InputC.Instance.InputEventUpdate += FuncUpdate;//�����¼�
    }

    private void OnDisable()//�ű�����Ч��������
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = this.gameObject.GetComponent<CharacterController>();
        m_Input = FindObjectOfType<InputC>();//ȫ����������ֻ����һ��
    }

    // Update is called once per frame
    void Update()
    {
        Ani.FuncUpdate();//�ƶ�������AniTime�ļ���
        Atk();//atk����
        Skill();
        Move();
    }

    void Move()//��ʹ����
    {
        //float H = Input.GetAxis("Horizontal");//GetKeyһֱ����GetKeyDown���µ���һ��
        //float V = Input.GetAxis("Vertical");
        Vector3 dir = transform.TransformDirection(new Vector3(m_Input.m_Movement.x, -1, m_Input.m_Movement.y));//-1��Ϊ�˱�������Ч��
        controller.Move(dir * walkSpeed * Time.deltaTime);//time.deltaTime��Ϊ��ÿһ֡�����ƶ�
    }

    private void FuncUpdate(Vector2 m_Movement)//�¼�����Ҫʵ�ֵĹ���
    {
        Vector3 dir = transform.TransformDirection(new Vector3(m_Movement.x, -1, m_Movement.y));//-1��Ϊ�˱�������Ч��
        controller.Move(dir * walkSpeed * Time.deltaTime);//time.deltaTime��Ϊ��ÿһ֡�����ƶ�
    }

    private void Atk()
    {
        if (���ܲ��ܹ���) return;

        if(m_Input.m_Attack && canAttack)//canAttack ��ɫӲֱʱ�䡢���˵ȸ���Ч��
        {
            PlayerAni.Instance.atk();
        }

        if(m_Input.m_Block && canBlock)//��
        {
            blockTime = Time.time;
            canBlock = false;
            PlayerAni.Instance.block();
            StartCoroutine(BlockWait());
        }
    }

    IEnumerator BlockWait()
    {
        yield return new WaitForSeconds(0.5f);
        canBlock = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "MonsAtkTrigger")
        {
            getHit(other.gameObject);//����
        }
        else if(other.tag == "DropItem")
        {
            Debug.Log("����" + other.gameObject.name);
            GameDataC.addItem(other.gameObject.name, 1);//��װ��
            Destroy(other.gameObject);
        }
    }

    public void getHit(GameObject obj)
    {
        float time_ = Time.time;
        if(time_ - blockTime <= blockBounceTime)
        {
            Destroy(obj);
            Debug.Log("�񵲳ɹ�");
        }
    }

    void Skill()//����Q
    {
        if (���ܲ��ܲ��� && ��ǰ�ͷż��� != 1001) return;//1001���ܸ��������ܲ���
        if(m_Input.m_Charge)//������
        {
            Skill1();
        }else if(skillAdd > 0)//���������󣬸���������ǿ���������ͷż��ܵĴ�С
        {
            SkillUse();
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    public void Skill1()
    {
        ��ǰ�ͷż��� = 1001;
        ���ܲ��ܹ��� = true;
        ���ܲ����ƶ� = true;
        ���ܲ��ܲ��� = true;
        skillAdd += Time.deltaTime * addSpeed;//����ʱ��ǿ��
        skillAdd = Mathf.Clamp(skillAdd, skillAddMin, skillAddMax);
    }

    /// <summary>
    /// �ͷ�
    /// </summary>
    public void SkillUse()
    {
        if (skillAdd <= 0) return;
        skillAdd = 0;
        StartCoroutine(skillLast());
    }

    IEnumerator skillLast()
    {
        yield return new WaitForSeconds(skillLastTime);
        ���ܲ����ƶ� = false;
        ���ܲ��ܹ��� = false;
        ���ܲ��ܲ��� = false;
    }

    public void beHit(Global.FightInfo fightInfo)//�յ��˺�
    {
        if (this.hp <= 0) return;
        this.hp -= fightInfo.�˺�;
        checkDie();
    }
    void checkDie()
    {
        if (this.hp <= 0)
        {
            playerState = ����״̬.����;

        }
    }
}
