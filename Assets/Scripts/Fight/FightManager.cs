using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour//�˺�����������˺����㲿�־��ڷ������
{
    public static void fightLogic(string atkId, Attribute attacker, Attribute target)
    {
        Debug.Log(attacker);
        Debug.Log(target);
        if(attacker.hp <= 0 || target.hp <=0)
        {
            return;
        }
        Global.FightInfo fightInfo = new Global.FightInfo();//Fight�˺���Ϣ������Fight״̬
        caleDamage(atkId, attacker, target, ref fightInfo);
        caleState(atkId, attacker, target, ref fightInfo);//erf�ɽ��ɳ�
        
        IFight ifight = target as IFight;//�����ýӿڡ�  ǿת��IFight������behit����target����˺�
        ifight.beHit(fightInfo);
    }

   //״̬����
    static void caleState(string atkId, Attribute attacker, Attribute target, ref Global.FightInfo fightInfo)
    {
        SkillConfig.SkillData skillData = SkillConfig.getSkillInfo(atkId);
        fightInfo.fightState = skillData.fightState;
    }

    //�˺�����
    static void caleDamage(string atkId, Attribute attacker, Attribute target, ref Global.FightInfo fightInfo)
    {
        SkillConfig.SkillData skillData = SkillConfig.getSkillInfo(atkId);
        //���㱩�����˵Ļ������
        float damage = (attacker.atk + EquipConfig.getDropInfo(GameDataC.equipWeaponId).atk) * skillData.�˺��ӳ� + skillData.�˺�ֵ - target.def;
        if(damage <= 0)
        {
            damage = 1;
        }
        fightInfo.�˺� = damage;
    }
}