using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;//Dotween���

public class MonsterBaseC : MonsterAttribute, IFight
{
    protected float hp = 10;
    private Image hpUI;//ͷ��Ѫ��
    protected Animator ani;
    private GameObject ������ײ��;
    protected CharacterController cc;

    protected float ����cd_ = 0;
    protected float Ӳֱʱ��_ = 0.3f;
    //protected float ����ʱ��_ = 1;
    protected float ����ʱ��_ = 1;
    protected float ��ҡʱ��_ = 1;
    protected float ǰҡʱ��_ = 1;

    protected ����״̬ ״̬;
    protected Transform Ŀ��;
    GameObject ͷ��ͼ��;

    EffectConfig.EffectData eData;
    float �����ٶ� = 15;
    float ���˼��ٶ�;
    float �������� = 5;
    float �������ʱ�� = -1;

    public enum ����״̬
    {
        û��Ŀ��,
        ����Ŀ��,
        ����,
        ����,
        ׷��,
        ��������,
        ����ǰҡ,
        ������ʼ,
        ������,
        ������ҡ,
        ������,
        Ӳֱ
    }

    public virtual void Awake()
    {
        binding();//���ݰ�
    }

    public virtual void Update()
    {
        if (!״̬���()) return;
        if(״̬== ����״̬.û��Ŀ��)
        {
            ���Է���Ŀ��();
        }
        else if(״̬ == ����״̬.����Ŀ��)
        {
            ���Ŀ�����();
        }
        else if (״̬ == ����״̬.׷��)
        {
            ׷��();
        }
        else if (״̬ == ����״̬.��������)
        {
            ��Թ�������();
        }
        else if (״̬ == ����״̬.������ʼ)
        {
            ��������();
        }
        else if (״̬ == ����״̬.����)
        {
            ����();
        }
        else if (״̬ == ����״̬.������ҡ)
        {
            ��ҡ();
        }
        else if (״̬ == ����״̬.����ǰҡ)
        {
            ǰҡ();
        }
        else if(״̬ == ����״̬.������)
        {
            ����();
        }

    }

    void ����()
    {
        Debug.Log("����***");
        if (�����ٶ� > 2f)
        {
            ���˼��ٶ� += Time.deltaTime * 20;
            �����ٶ� -= ���˼��ٶ�;//���˵ĳ�ʼ�ٶ�15��ֱ������2��ֹͣ
            cc.Move(this.transform.forward * (-�����ٶ�) * Time.deltaTime);
            return;
        }
        else
        {
            ״̬ = ����״̬.û��Ŀ��;
            eData.�Ƿ���� = 0;
            �����ٶ� = 15;
            ���˼��ٶ� = 0;
        }
    }

    bool ״̬���()
    {
        if(״̬ == ����״̬.����)
        {
            return false;
        }

        //Debug.Log("eData.��ͣ���� " + eData.��ͣ����);
        if(eData.��ͣ���� > 0)
        {
            ani.speed = 0;//1Ϊ�����ٶ�
        }
        else
        {
            ani.speed = 1;
        }

        if(�������ʱ�� != -1 && Time.time > �������ʱ��)
        {
            //�����������״̬

            //��ȡ�������ݣ�����Ӧ�ļ�������������ʡ����
            eData.�����ƶ� -= 1;
            eData.����ʩ�� -= 1;
            eData.���ƹ��� -= 1;
            eData.��ͣ���� -= 1;
        }

        if(eData.�Ƿ���� >0)
        {
            ״̬ = ����״̬.������;
        }

        if (Ӳֱʱ��_ > 0)
        {
            Ӳֱʱ��_ -= Time.deltaTime;
            return false;
        }
        else
        {
            if(״̬ == ����״̬.Ӳֱ)
            {
                ״̬ = ����״̬.û��Ŀ��;
            }
        }

        return true;
    }

    void ���Է���Ŀ��()
    {
        float distance = Vector3.Distance(this.transform.position, PlayerC.Instance.transform.position);
        if(distance <= ������Χ)
        {
            ����Ŀ��();
        }
        else
        {
            if(distance <= �Ӿ���Χ && ToolMethod.��ȡ��Ŀ��ļн�(PlayerC.Instance.transform.position, this.transform) < GameConfig.������Ұ�Ƕ� / 2)
            {
                ����Ŀ��();
            }
            else
            {
                ���Ŀ��();
            }
        }
    }

    void ���Ŀ�����()//����Ŀ���Ҫ����
    {
        int R = Random.Range(0, 100);
        if(R <= ׷������)
        {
            this.transform.DOLookAt(new Vector3(Ŀ��.position.x, this.transform.position.y, Ŀ��.position.z), 1.3f);//�߶�ֵ�ù����Լ��ģ�Ŀ�������
            ״̬ = ����״̬.׷��;
        }
        else
        {
            ״̬ = ����״̬.����;
        }
    }

    void ׷��()
    {
        if(����Ƿ�ɹ���())
        {
            ״̬ = ����״̬.��������;//���빥����Χ
            ani.SetBool("walking", false);
            return;
        }
        if(����Ƿ�ʧȥĿ��())
        {
            ״̬ = ����״̬.û��Ŀ��;
            ani.SetBool("walking", false);
            return;
        }
        //debug.Log("�ƶ�");
        this.transform.LookAt(new Vector3(Ŀ��.position.x, this.transform.position.y, Ŀ��.position.z));
        cc.Move(this.transform.forward * Time.deltaTime * moveSpeed);
        ani.SetBool("walking", true);
    }

    bool ����Ƿ�ɹ���()
    {
        float distance = Vector3.Distance(this.transform.position, PlayerC.Instance.transform.position);
        if(distance <= ��������)
        {
            return true;
        }
        return false;
    }

    bool ����Ƿ�ʧȥĿ��()
    {
        float distance = Vector3.Distance(this.transform.position, PlayerC.Instance.transform.position);
        //���ж�����
        if(distance >= ʧȥĿ�����)
        {
            return true;
        }
        return false;
    }

    void ��Թ�������()
    {
        int R = Random.Range(0, 100);
        if (R <= ��������)
        {
            ani.SetBool("beforeAtk", true);
            this.transform.DOLookAt(new Vector3(Ŀ��.position.x, this.transform.position.y, Ŀ��.position.z), 0.6f);
            ״̬ = ����״̬.����ǰҡ;
        }
        else
        {
            ״̬ = ����״̬.����;
        }
    }

    void ǰҡ()
    {
        ǰҡʱ��_ -= Time.deltaTime;
        if(ǰҡʱ��_ <= 0)
        {
            ani.SetTrigger("atk");//û��ǰҡ�������ù�����������
            ״̬ = ����״̬.������ʼ;
            ǰҡʱ��_ = ǰҡʱ��;
        }
    }

    void ��ҡ()
    {
        ��ҡʱ��_ -= Time.deltaTime;
        if(��ҡʱ��_ <= 0)
        {
            ani.SetBool("beforeAtk", false);
            ״̬ = ����״̬.û��Ŀ��;
            ��ҡʱ��_ = ��ҡʱ��;
        }
    }

    void ��������()
    {
        ״̬ = ����״̬.������;
        Debug.Log("��������" + Time.realtimeSinceStartup);
        StartCoroutine(����());
    }

    IEnumerator ����()
    {
        yield return 0;
        this.transform.LookAt(new Vector3(Ŀ��.position.x, this.transform.position.y, Ŀ��.position.z));
        Vector3 dir = this.transform.forward;
        yield return new WaitForSeconds(�չ���Чʱ��);
        Debug.Log("����" + Time.realtimeSinceStartup);
        GameObject bullet_ = Instantiate(bullet);
        bullet_.transform.LookAt(bullet_.transform.position + new Vector3(dir.x, 0, dir.z));//�ӵ�����
        bullet_.transform.position = this.transform.position + this.transform.forward;//�ӵ�λ��
        bullet_.GetComponent<Rigidbody>().velocity = bullet_.transform.forward * 5;
        ״̬ = ����״̬.������ҡ;
    }

    void ����()
    {
        if(�����Ƿ�������)
        {
            this.transform.LookAt(new Vector3(Ŀ��.position.x, this.transform.position.y, Ŀ��.position.z));
        }
        ����ʱ��_ -= Time.deltaTime;
        if(����ʱ��_ <= 0)
        {
            ״̬ = ����״̬.û��Ŀ��;
            ����ʱ��_ = ����ʱ��;
        }
    }

    void ����Ŀ��()
    {
        ״̬ = ����״̬.����Ŀ��;
        Ŀ�� = PlayerC.Instance.transform;
        //���ַ�Ӧͼ��
        //if(!!ͷ��ͼ��)
        //{
        //    ͷ��ͼ��.SetActive(true);
        //    Invoke("�ر�ͼ��", 0.4f);
        //}
    }

    void ���Ŀ��()
    {
        ״̬ = ����״̬.û��Ŀ��;
        Ŀ�� = null;
    }

    public void binding()//���ݰ�
    {
        cc = this.GetComponent<CharacterController>();
        bullet = Resources.Load<GameObject>("tx/TX1001");
        ani = this.GetComponent<Animator>();
        if(ani == null)
        {
            ani = this.transform.GetChild(0).GetComponent<Animator>();
        }
        Debug.Log("ani" + ani);
    }

    /*
    public virtual void OnCollisionEnter(Collision coll)//������ײ����
    {
        if(coll.transform.tag == "���е���")
        {
            hp -= 5;
            hpUI.fillAmount = hp / hpmax;
            this.transform.DOPunchRotation(Vector3.one * 15f, 0.1f, 1, 1).OnComplete(delegate()
            {
                if(hp <= 0)
                {
                    die();
                }
            })
        }
    }*/

    public virtual void OnCollisionEnter(Collision coll)
    {
        if (coll.transform.tag == "���е���")
        {
            hp -= 5;
            hpUI.fillAmount = hp / hpmax;
            this.transform.DOPunchRotation(Vector3.one * 15f, 0.1f, 1, 1).OnComplete(delegate ()
            {
                checkDie();
            });
        }
    }

    public virtual void OnTriggerEnter(Collider other)//��⹥����ײ�壬�������Ϊ����
    {
        if (other.tag == "������ײ��")
        {
            //    beHit(PlayerInputControllerC.instance.get�������(name, other.name));
        }
    }
    // Update is called once per frame
    public virtual IEnumerator Die()
    {
        yield return new WaitForSeconds(2f);
        drop();
        Destroy(this.gameObject);
    }

    public void beHit(Global.FightInfo fightInfo)//�����յ��˺�
    {
        if (this.hp <= 0) return;
        this.hp -= fightInfo.�˺�;

        eData = ToolFight.ConvertState(fightInfo.fightState);//�õ���ǰ��״̬����
        Debug.Log("eData.�Ƿ���� " + eData.�Ƿ����);
        if (!checkDie())
        {
            foreach (var fs in fightInfo.fightState)
            {
                if (fs == Global.FightState.����)
                {
                    // ���ű�������
                    �������ʱ�� = Time.time + ��������;
                }
            }
            //if (�չ���϶���)
            //{ 
            //    ״̬ = ����״̬.Ӳֱ;
            //    Ӳֱʱ��_ = Ӳֱʱ��;
            //    ani.SetTrigger("damage"); 
            //}

        }
    }

    bool checkDie()
    {
        if (this.hp <= 0)
        {
            ״̬ = ����״̬.����;
            Debug.Log("��������");
            StartCoroutine(Die());
            return true;
        }
        return false;
    }

    public void drop() // ����
    {
        DropC.doDrop(this.ID, this.transform.position);
    }
}
