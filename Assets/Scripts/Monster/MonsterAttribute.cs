using UnityEngine;

public class MonsterAttribute : Attribute
{
    protected float 攻击cd = 2;
    public string ID;
    protected float 攻击概率 = 65;
    protected float 追击概率 = 45;
    protected float 发呆时间 = 0.3f;
    protected float 前摇时间 = 1;
    protected float 普攻生效时间 = 0.5f;
    protected float 后摇时间 = 1;
    protected float 视觉范围 = 20;//视距120度
    protected float 听觉范围 = 13f;
    protected float 攻击距离 = 8f;
    protected float 失去目标距离 = 25;
    protected bool 发呆是否看向主角 = true;
    protected GameObject bullet;//子弹，这里指法术的粒子
    protected Vector3 atkPos = new Vector3(5f, 0f, 1f);//法球刚开始出现的位置
}