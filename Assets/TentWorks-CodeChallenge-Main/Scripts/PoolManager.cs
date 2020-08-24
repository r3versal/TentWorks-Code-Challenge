using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum PoolObjectType
    {
        Customer,
        Recipe
    }

    [Serializable]
    public class PoolInfo
    {
        public PoolObjectType type;
        public int amount = 0;
        public GameObject prefab;
        public GameObject container;

        public List<GameObject> pool = new List<GameObject>();
    }

    public class PoolManager : Singleton
    {
    [SerializeField]
    List<PoolInfo> listOfPool;

        void Start()
        {
        for(int i = 0; i < listOfPool.Count; i++)
        {
            FillPool(listOfPool[i]);
        }
        }

        void FillPool(PoolInfo info)
        {
            for(int i = 0; i < info.amount; i++)
            {
            GameObject obInstance = null;
           // obInstance = Instantiate(info.prefab, info.container.transform);
            obInstance.gameObject.SetActive(false);
           // obInstance.transform.position = defaultPos;
            info.pool.Add(obInstance);
            }
        }
    



}
