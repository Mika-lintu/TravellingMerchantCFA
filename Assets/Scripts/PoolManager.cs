using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{

    Dictionary<string, Queue<ItemObjectInstance>> itemDictionary = new Dictionary<string, Queue<ItemObjectInstance>>();
    Dictionary<int, Queue<ObjectInstance>> poolDictionary = new Dictionary<int, Queue<ObjectInstance>>();

    static PoolManager _instance;
    public GameObject[] itemPrefabs;
    public GameObject inventory;
    public GameObject sceneItems;


    public static PoolManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PoolManager>();
            }
            return _instance;
        }
    }


    /* CREATE THE OBJECT POOL HERE:
     * - Set GameObject prefab and wanted item quantity
     * - All pool object instances will be in "<prefab name> pool" -folder
     * - Dictionary key is integer, which is the prefab instance ID
     */
    public void CreatePool(GameObject prefab, int poolSize)
    {
        int poolKey = prefab.GetInstanceID();
        List<GameObject> poolList = new List<GameObject>();
        GameObject poolHolder = new GameObject(prefab.name + " pool");
        poolHolder.transform.parent = transform;

        if (!poolDictionary.ContainsKey(poolKey))
        {
            poolDictionary.Add(poolKey, new Queue<ObjectInstance>());

            for (int i = 0; i < poolSize; i++)
            {
                ObjectInstance newObject = new ObjectInstance(Instantiate(prefab) as GameObject);
                poolDictionary[poolKey].Enqueue(newObject);
                newObject.SetParent(poolHolder.transform);
            }
        }
    }

    public void CreateItemPool(GameObject prefab, int poolSize, string location)
    {
        string poolKey = prefab.name;
        GameObject poolHolder = null;
        if (location == "inventory")
        {
            poolHolder = inventory;
        }
        else
        {
            poolHolder = sceneItems;
            poolHolder.transform.parent = transform;
        }


        if (!itemDictionary.ContainsKey(poolKey))
        {
            itemDictionary.Add(poolKey, new Queue<ItemObjectInstance>());

            for (int i = 0; i < poolSize; i++)
            {
                    ItemObjectInstance newObject = new ItemObjectInstance(Instantiate(prefab) as GameObject, location);
                    itemDictionary[poolKey].Enqueue(newObject);
                    newObject.SetParent(poolHolder.transform);
            }
        }
        else
        {
            for (int i = 0; i < poolSize; i++)
            {
                ItemObjectInstance newObject = new ItemObjectInstance(Instantiate(prefab) as GameObject, location);

                itemDictionary[poolKey].Enqueue(newObject);
                newObject.SetParent(poolHolder.transform);
            }
        }
    }


    public void ReuseObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        int poolKey = prefab.GetInstanceID();

        if (poolDictionary.ContainsKey(poolKey))
        {
            ObjectInstance objectToReuse = poolDictionary[poolKey].Dequeue();
            poolDictionary[poolKey].Enqueue(objectToReuse);
            objectToReuse.Reuse(position, rotation);
        }
    }

    public GameObject ReuseItem(string id, Vector3 position, Quaternion rotation, float scale, GameObject parent)
    {
        string poolKey = id;

        if (itemDictionary.ContainsKey(poolKey))
        {
            ItemObjectInstance objectToReuse = itemDictionary[poolKey].Dequeue();
            itemDictionary[poolKey].Enqueue(objectToReuse);
            objectToReuse.Reuse(position, rotation, scale);
            objectToReuse.SetParent(parent.transform);
            return objectToReuse.gameObject;
        }
        return null;
    }



    public void ReuseProp(GameObject prefab, Vector3 position, Quaternion rotation, GameObject parent)
    {
        int poolKey = prefab.GetInstanceID();

        if (poolDictionary.ContainsKey(poolKey))
        {
            ObjectInstance objectToReuse = poolDictionary[poolKey].Dequeue();
            poolDictionary[poolKey].Enqueue(objectToReuse);
            objectToReuse.Reuse(position, rotation);
            objectToReuse.SetParent(parent.transform);
        }
    }



    public class ObjectInstance
    {
        GameObject gameObject;
        Transform transform;

        bool hasPoolObjectComponent;
        PoolObject poolObjectScript;

        public ObjectInstance(GameObject objectInstance)
        {
            gameObject = objectInstance;
            transform = gameObject.transform;
            gameObject.SetActive(false);

            if (gameObject.GetComponent<PoolObject>())
            {
                hasPoolObjectComponent = true;
                poolObjectScript = gameObject.GetComponent<PoolObject>();
            }
        }

        public void Reuse(Vector3 position, Quaternion rotation)
        {
            if (hasPoolObjectComponent)
            {
                poolObjectScript.OnObjectReuse();
            }

            gameObject.SetActive(true);
            transform.position = position;
            transform.rotation = rotation;
        }

        public void SetParent(Transform parent)
        {
            transform.parent = parent;
        }
    }


    public class ItemObjectInstance
    {
        public GameObject gameObject;
        Transform transform;

        bool hasPoolObjectComponent;
        PoolObject poolObjectScript;

        public ItemObjectInstance(GameObject objectInstance, string location)
        {
            gameObject = objectInstance;
            transform = gameObject.transform;
            gameObject.SetActive(false);

            if (gameObject.GetComponent<PoolObject>())
            {
                hasPoolObjectComponent = true;
                poolObjectScript = gameObject.GetComponent<PoolObject>();
            }

            if (gameObject.GetComponent<ItemStats>())
            {
                gameObject.GetComponent<ItemStats>().itemLocation = location;
            }
        }

        public void Reuse(Vector3 position, Quaternion rotation, float scale)
        {
            if (hasPoolObjectComponent)
            {
                poolObjectScript.OnObjectReuse();
            }

            gameObject.SetActive(true);
            transform.position = position;
            transform.rotation = rotation;
            transform.localScale = new Vector3(scale, scale, 1);
        }

        public void SetParent(Transform parent)
        {
            transform.parent = parent;
        }
    }
}
