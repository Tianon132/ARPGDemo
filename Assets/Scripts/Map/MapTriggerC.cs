using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapTriggerC : MonoBehaviour
{
    public bool isDelete;//有些触发器 触发一次就必须删除了
    public GameObject[] hideObjects;
    public GameObject[] showObjects;//接触过之后就会先隐藏，等返回之后再打开，防止反复触发
    public string EventName;//待发送的事件Name
    public string triggerTag;//控制触发对象的范围（这里只针对玩家发生反应）
    public static event UnityAction<string> MapEvent;//全局 通知 监听者

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.tag == triggerTag)//是不是目标对象引发的触发
        {
            Debug.Log(MapEvent);
            if(EventName != "" && MapEvent != null)//有 事件名称 和 事件监听者 才会 发送事件
            {
                Debug.Log(EventName);
                MapEvent(EventName);
            }
            //showObjects[0].SetActive(false);
            for (int i = 0; i < hideObjects.Length; i++)
            {
                hideObjects[i].SetActive(false);
            }
            for (int i = 0; i < showObjects.Length; i++)
            {
                showObjects[i].SetActive(true);
            }
            if(isDelete)
            {
                Destroy(this.gameObject);
            }

        }
    }
}
