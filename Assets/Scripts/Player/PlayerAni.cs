using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAni : MonoBehaviour
{
    public static PlayerAni Instance;
    private void Awake()
    {
        Instance = this;
        Debug.Log("Player Ani Awake");
    }

    public Animator ani;
    InputC m_Input;

    // Start is called before the first frame update
    void Start()
    {
        ani = this.transform.GetChild(0).gameObject.GetComponent<Animator>();
        m_Input = FindObjectOfType<InputC>();
    }

    public void FuncUpdate()
    {
        ani.SetFloat("AniTime", Mathf.Repeat(ani.GetCurrentAnimatorStateInfo(0).normalizedTime, 1f));

        ani.SetFloat("horizontal", Mathf.Abs(m_Input.m_Movement.x));//���Ǿ���ֵ�Ļ�ֻ�ܵ�����
        ani.SetFloat("vertical", Mathf.Abs(m_Input.m_Movement.y));

        ani.ResetTrigger("atk");//����atk��������ֹ������ʱ������bug
        ani.SetBool("����", m_Input.m_Charge);
    }

    public void atk()
    {
        ani.SetTrigger("atk");
        Debug.Log(ani.GetFloat("AniTime"));
    }

    public void block()
    {
        ani.SetTrigger("block");
    }

}
