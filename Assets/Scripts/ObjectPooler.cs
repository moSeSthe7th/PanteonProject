﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPooler : MonoBehaviour
{
    //singleton
    public static ObjectPooler instance;

    private void Awake()
    {
        instance = this;
    }

    //Get the pooled object from the list
    public GameObject GetPooledObject(List<GameObject> pooledObjects)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    //Pool the objects according to the given parameters
    public List<GameObject> PooltheObjects(GameObject objectToPool, int amountToPool)
    {
        List<GameObject> pooledObjects;
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
        return pooledObjects;
    }

    public List<GameObject> PooltheObjects(GameObject objectToPool, int amountToPool, Transform parent)
    {
        List<GameObject> pooledObjects;
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectToPool, parent);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
        return pooledObjects;
    }

    public List<Transform> PooltheObjects(Transform objectToPool, int amountToPool)
    {
        List<Transform> pooledObjects;
        pooledObjects = new List<Transform>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject tmpobj = (GameObject)Instantiate(objectToPool.gameObject);
            Transform obj = tmpobj.transform;
            obj.gameObject.SetActive(false);
            pooledObjects.Add(obj);
        }
        return pooledObjects;
    }

    public List<Transform> PooltheObjects(Transform objectToPool, int amountToPool, Transform parent)
    {
        List<Transform> pooledObjects;
        pooledObjects = new List<Transform>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject  tmpobj = (GameObject)Instantiate(objectToPool.gameObject, parent);
            Transform obj = tmpobj.transform;
            obj.gameObject.SetActive(false);
            pooledObjects.Add(obj);
        }
        return pooledObjects;
    }


    public List<GameObject> PooltheObjects(GameObject objectToPool, int amountToPool, bool isActive)
    {
        List<GameObject> pooledObjects;
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectToPool);
            if(!isActive)
                obj.SetActive(false);
            pooledObjects.Add(obj);
        }
        return pooledObjects;
    }

    public List<GameObject> PooltheObjects(GameObject objectToPool, int amountToPool, Transform parent, bool isActive)
    {
        List<GameObject> pooledObjects;
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectToPool, parent);
            if (!isActive)
                obj.SetActive(false);
            pooledObjects.Add(obj);
        }
        return pooledObjects;
    }

    public List<Transform> PooltheObjects(Transform objectToPool, int amountToPool, bool isActive)
    {
        List<Transform> pooledObjects;
        pooledObjects = new List<Transform>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject tmpobj = (GameObject)Instantiate(objectToPool.gameObject);
            Transform obj = tmpobj.transform;
            if (!isActive)
                obj.gameObject.SetActive(false);
            pooledObjects.Add(obj);
        }
        return pooledObjects;
    }

    public List<Transform> PooltheObjects(Transform objectToPool, int amountToPool, Transform parent, bool isActive)
    {
        List<Transform> pooledObjects;
        pooledObjects = new List<Transform>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject tmpobj = (GameObject)Instantiate(objectToPool.gameObject, parent);
            Transform obj = tmpobj.transform;
            if (!isActive)
                obj.gameObject.SetActive(false);
            pooledObjects.Add(obj);
        }
        return pooledObjects;
    }


}
