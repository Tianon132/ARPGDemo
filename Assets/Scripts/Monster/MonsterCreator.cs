using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCreator : MonoBehaviour
{
    GameObject[] creators;//生成点
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
            return;//是不是上一张 如果是就不变
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

    public IEnumerator makeMonsters()//实例化是消耗很大的，故携程比较适合
    {
        yield return 0;
        for(int i = 0; i < creators.Length; i++)
        {
            yield return 5;
            int n = Random.Range(0, monsterPrefabs.Length);//参数是Int，返回是int，包含最小值，但不包含最大值！！！
                                                           //参数是float，返回则是float，最小最大都包括
            //Debug.Log(n);
            Instantiate(monsterPrefabs[n], creators[i].transform.position, Quaternion.identity, creators[i].transform); //Quaternion.identity 代表没有旋转
        }
    }
}
