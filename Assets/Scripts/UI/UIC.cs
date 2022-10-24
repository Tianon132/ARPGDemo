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
            module.Add(tr.name, tr.gameObject);//�����õ�Canvas��ÿ������Ϣ
        }
    }

    public void OpenUI(string name)
    {
        if (lastOpened != null && lastOpened.name == name && lastOpened.activeSelf)
        {//�Ѿ��򿪹��ˣ��ٵ����Ӧ���ǹ���
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            module[name].SetActive(false);
            lastOpened = null;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (lastOpened != null) lastOpened.SetActive(false);//�Ȱ��ϴεĸ�����
            module[name].SetActive(true);//�ٴ���ε�
            lastOpened = module[name];
        }
    }
}
