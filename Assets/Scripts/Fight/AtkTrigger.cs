//#define TEST
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkTrigger : MonoBehaviour//战斗结算1
{
    public Attribute attribute;//自身的属性（包括攻击力）

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

    private void OnTriggerEnter(Collider other)//战斗结算不能放到怪物上，那每个怪物都挂这个脚本，很拖累性能
    {
        if(other.tag == "Monster")
        {
            Debug.Log("攻击atk成功");
#if TEST
            other.GetComponent<TestHit>().beHit();
#else
            FightManager.fightLogic(skillName, attribute, other.gameObject.GetComponent<Attribute>());
#endif
        }//#if是在编辑之前就开始处理代码，而if是之后
    }
}
