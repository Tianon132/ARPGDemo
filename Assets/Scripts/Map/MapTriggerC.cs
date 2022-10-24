using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapTriggerC : MonoBehaviour
{
    public bool isDelete;//��Щ������ ����һ�ξͱ���ɾ����
    public GameObject[] hideObjects;
    public GameObject[] showObjects;//�Ӵ���֮��ͻ������أ��ȷ���֮���ٴ򿪣���ֹ��������
    public string EventName;//�����͵��¼�Name
    public string triggerTag;//���ƴ�������ķ�Χ������ֻ�����ҷ�����Ӧ��
    public static event UnityAction<string> MapEvent;//ȫ�� ֪ͨ ������

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.tag == triggerTag)//�ǲ���Ŀ����������Ĵ���
        {
            Debug.Log(MapEvent);
            if(EventName != "" && MapEvent != null)//�� �¼����� �� �¼������� �Ż� �����¼�
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
