using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCreator : MonoBehaviour
{
    GameObject[] creators;//���ɵ�
    GameObject[] monsterPrefabs;
    string lastMap = "";

    private void Awake()
    {
        if(creators == null)
        {
            creators = GameObject.FindGameObjectsWithTag("SpawnPoint");
        }
        this.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        if(lastMap == GameDataC.nowMap)
        {
            return;//�ǲ�����һ�� ����ǾͲ���
        }
        lastMap = GameDataC.nowMap;
        List<GameDataConfig.MapCreateMonsData> mData = GameDataConfig.GetMapMonster(lastMap);
        monsterPrefabs = new GameObject[mData.Count];
        for(int i = 0; i<mData.Count; i++)
        {
            monsterPrefabs[i] = Resources.Load<GameObject>("monster/" + mData[i].monsname);
        }
        StartCoroutine(makeMonsters());
    }

    public IEnumerator makeMonsters()//ʵ���������ĺܴ�ģ���Я�̱Ƚ��ʺ�
    {
        yield return 0;
        for(int i = 0; i < creators.Length; i++)
        {
            yield return 5;
            int n = Random.Range(0, monsterPrefabs.Length);//������Int��������int��������Сֵ�������������ֵ������
                                                           //������float����������float����С��󶼰���
            //Debug.Log(n);
            Instantiate(monsterPrefabs[n], creators[i].transform.position, Quaternion.identity, creators[i].transform); //Quaternion.identity ����û����ת
        }
    }
}
