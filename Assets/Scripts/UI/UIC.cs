using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIC : MonoBehaviour
{
    private static UIC instance;
    public static UIC Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<UIC>();
            }
            return instance;
        }
    }

    public Dictionary<string, GameObject> module = new Dictionary<string, GameObject>();
    public GameObject lastOpened;

    private void Awake()
    {
        Transform canvas = this.transform.Find("Canvas");
        foreach(Transform tr in canvas)
        {
            module.Add(tr.name, tr.gameObject);//遍历得到Canvas的每个子信息
        }
    }

    public void OpenUI(string name)
    {
        if (lastOpened != null && lastOpened.name == name && lastOpened.activeSelf)
        {//已经打开过了，再点击就应该是关了
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            module[name].SetActive(false);
            lastOpened = null;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (lastOpened != null) lastOpened.SetActive(false);//先把上次的给关了
            module[name].SetActive(true);//再打开这次的
            lastOpened = module[name];
        }
    }
}
