using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{

    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;


    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
    }


    void Update()
    {
        
    }
}
