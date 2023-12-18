using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadowpool : MonoBehaviour
{
    public static shadowpool instance;
    public GameObject shadowPrefab;
    public int shadowCount;
    //private Queue availableObjects = new Queue();
    private Queue<GameObject> availableObjects = new Queue<GameObject>();

    void Awake()
    {
        instance = this;

        //初始化對象持
        FillPool();
    }

    public void FillPool()
    {
        for(int i = 0; i<shadowCount; i++)
        {
            var newshadow = Instantiate(shadowPrefab);
            newshadow.transform.SetParent(transform);

            //取消啟用,返回對象持
            ReturnPool(newshadow);
        }
    }

    public void ReturnPool(GameObject gameObject)
    {
        gameObject.SetActive(false);

        availableObjects.Enqueue(gameObject);
    }

    public GameObject GetFormPool()
    {
        if(availableObjects.Count == 0)
        {
            FillPool();
        }
        var outShadow = availableObjects.Dequeue();

        outShadow.SetActive(true);

        return outShadow;
    }
}
