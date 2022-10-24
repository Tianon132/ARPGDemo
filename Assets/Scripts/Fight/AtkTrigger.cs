//#define TEST
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkTrigger : MonoBehaviour//ս������1
{
    public Attribute attribute;//��������ԣ�������������

    string skillName;
    public string SkillName
    {
        set
        {
            skillName = value;
        }
    }
    

    public RoleBase roleBase;
    public enum RoleType
    {
        Player,
        Monster
    }
    public RoleType roleType;

    private void OnTriggerEnter(Collider other)//ս�����㲻�ܷŵ������ϣ���ÿ�����ﶼ������ű�������������
    {
        if(other.tag == "Monster")
        {
            Debug.Log("����atk�ɹ�");
#if TEST
            other.GetComponent<TestHit>().beHit();
#else
            FightManager.fightLogic(skillName, attribute, other.gameObject.GetComponent<Attribute>());
#endif
        }//#if���ڱ༭֮ǰ�Ϳ�ʼ������룬��if��֮��
    }
}
