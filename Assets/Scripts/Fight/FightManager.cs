using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour//伤害：网友这个伤害结算部分就在服务端了
{
    public static void fightLogic(string atkId, Attribute attacker, Attribute target)
    {
        Debug.Log(attacker);
        Debug.Log(target);
        if(attacker.hp <= 0 || target.hp <=0)
        {
            return;
        }
        Global.FightInfo fightInfo = new Global.FightInfo();//Fight伤害信息，包括Fight状态
        caleDamage(atkId, attacker, target, ref fightInfo);
        caleState(atkId, attacker, target, ref fightInfo);//erf可进可出
        
        IFight ifight = target as IFight;//【调用接口】  强转换IFight，调用behit，对target造成伤害
        ifight.beHit(fightInfo);
    }

   //状态部分
    static void caleState(string atkId, Attribute attacker, Attribute target, ref Global.FightInfo fightInfo)
    {
        SkillConfig.SkillData skillData = SkillConfig.getSkillInfo(atkId);
        fightInfo.fightState = skillData.fightState;
    }

    //伤害部分
    static void caleDamage(string atkId, Attribute attacker, Attribute target, ref Global.FightInfo fightInfo)
    {
        SkillConfig.SkillData skillData = SkillConfig.getSkillInfo(atkId);
        //计算暴击减伤的混合区域
        float damage = (attacker.atk + EquipConfig.getDropInfo(GameDataC.equipWeaponId).atk) * skillData.伤害加成 + skillData.伤害值 - target.def;
        if(damage <= 0)
        {
            damage = 1;
        }
        fightInfo.伤害 = damage;
    }
}