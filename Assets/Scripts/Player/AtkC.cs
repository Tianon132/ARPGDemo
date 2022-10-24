using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkC : MonoBehaviour
{
    public BoxCollider weapon;
    public static bool can;
    AtkTrigger atkTrigger;

    private void Start()
    {
        atkTrigger = weapon.GetComponent<AtkTrigger>();
    }

    public void AtkTriggerStart(string name)
    {
        weapon.enabled = true;
        atkTrigger.SkillName = name;
    }

    public void AtkTriggerEnd()
    {
        atkTrigger.name = "";
        weapon.enabled = false;
    }

    public void tx(GameObject tx)
    {
        var instance = Instantiate(tx, this.transform.position, Quaternion.identity);
        instance.transform.rotation = PlayerC.Instance.transform.rotation;
        Destroy(instance, 3);
    }
}
