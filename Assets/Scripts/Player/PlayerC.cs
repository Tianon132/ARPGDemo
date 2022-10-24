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

    private CharacterController controller;//角色移动控制
    public float walkSpeed = 4.5f;

    public InputC m_Input;
    public PlayerAni Ani;

    //攻击部分
    public bool canAttack = true;
    public bool canBlock = true;
    public float blockTime = 0;

    //释放技能
    private float addSpeed = 1;//累加速度
    private float skillAdd = 3;//蓄力的时间长度
    private float skillAddMax = 3;
    private float skillAddMin = 1;

    private bool 技能不能移动 = false;
    private bool 技能不能攻击 = false;
    private bool 技能不能并发 = false;

    private int 当前释放技能 = 0;
    private float skillLastTime = 3.5f;

    private void OnEnable()//脚本生效会调用这个
    {
        //InputC.Instance.InputEventUpdate += FuncUpdate;//订阅事件
    }

    private void OnDisable()//脚本不生效会调用这个
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = this.gameObject.GetComponent<CharacterController>();
        m_Input = FindObjectOfType<InputC>();//全局搜索，当只出现一次
    }

    // Update is called once per frame
    void Update()
    {
        Ani.FuncUpdate();//移动动画和AniTime的计算
        Atk();//atk动画
        Skill();
        Move();
    }

    void Move()//不使用了
    {
        //float H = Input.GetAxis("Horizontal");//GetKey一直按，GetKeyDown按下的那一下
        //float V = Input.GetAxis("Vertical");
        Vector3 dir = transform.TransformDirection(new Vector3(m_Input.m_Movement.x, -1, m_Input.m_Movement.y));//-1是为了保持重力效果
        controller.Move(dir * walkSpeed * Time.deltaTime);//time.deltaTime是为了每一帧均匀移动
    }

    private void FuncUpdate(Vector2 m_Movement)//事件发生要实现的功能
    {
        Vector3 dir = transform.TransformDirection(new Vector3(m_Movement.x, -1, m_Movement.y));//-1是为了保持重力效果
        controller.Move(dir * walkSpeed * Time.deltaTime);//time.deltaTime是为了每一帧均匀移动
    }

    private void Atk()
    {
        if (技能不能攻击) return;

        if(m_Input.m_Attack && canAttack)//canAttack 角色硬直时间、击退等负面效果
        {
            PlayerAni.Instance.atk();
        }

        if(m_Input.m_Block && canBlock)//格挡
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
            getHit(other.gameObject);//隔档
        }
        else if(other.tag == "DropItem")
        {
            Debug.Log("捡起" + other.gameObject.name);
            GameDataC.addItem(other.gameObject.name, 1);//捡装备
            Destroy(other.gameObject);
        }
    }

    public void getHit(GameObject obj)
    {
        float time_ = Time.time;
        if(time_ - blockTime <= blockBounceTime)
        {
            Destroy(obj);
            Debug.Log("格挡成功");
        }
    }

    void Skill()//按下Q
    {
        if (技能不能并发 && 当前释放技能 != 1001) return;//1001不能跟其他技能并发
        if(m_Input.m_Charge)//在蓄力
        {
            Skill1();
        }else if(skillAdd > 0)//蓄力结束后，根据蓄力的强度来决定释放技能的大小
        {
            SkillUse();
        }
    }

    /// <summary>
    /// 蓄力
    /// </summary>
    public void Skill1()
    {
        当前释放技能 = 1001;
        技能不能攻击 = true;
        技能不能移动 = true;
        技能不能并发 = true;
        skillAdd += Time.deltaTime * addSpeed;//蓄力时间强度
        skillAdd = Mathf.Clamp(skillAdd, skillAddMin, skillAddMax);
    }

    /// <summary>
    /// 释放
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
        技能不能移动 = false;
        技能不能攻击 = false;
        技能不能并发 = false;
    }

    public void beHit(Global.FightInfo fightInfo)//收到伤害
    {
        if (this.hp <= 0) return;
        this.hp -= fightInfo.伤害;
        checkDie();
    }
    void checkDie()
    {
        if (this.hp <= 0)
        {
            playerState = 主角状态.死亡;

        }
    }
}
