using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{

    Dictionary<int, Queue<ObjectInstance>> poolDictionary = new Dictionary<int, Queue<ObjectInstance>>();

    static PoolManager _instance;
    public GameObject[] itemPrefabs;
    public GameObject inventory;
    List<Transform> poolHolders = new List<Transform>();

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

    public void CreateItemPool(GameObject prefab, int poolSize)
    {
        int poolKey = prefab.GetInstanceID();

        GameObject poolHolder = inventory;

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
        else
        {
            for (int i = 0; i < poolSize; i++)
            {
                ObjectInstance newObject = new ObjectInstance(Instantiate(prefab) as GameObject);

                poolDictionary[poolKey].Enqueue(newObject);
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
}


/*      HULLUN HIENO VIRITELMÄ JOTA VOI KÄYTTÄÄ JOS HALUAA TEHDÄ JOKAISELLE ITEMILLE OMAN PARENTIN INVENTARIOON, MUTTA VIE VÄHÄN ENEMMÄN LASKENTATEHOA
 *      
 *          int poolKey = prefab.GetInstanceID();
            string poolHolderName = prefab.name + " pool";

            if (!poolDictionary.ContainsKey(poolKey))
            {
                GameObject poolHolder = new GameObject(poolHolderName);
                poolHolder.transform.parent = inventory.transform;
                poolDictionary.Add(poolKey, new Queue<ObjectInstance>());
                poolHolders.Add(poolHolder.transform);

                for (int i = 0; i < poolSize; i++)
                {
                    ObjectInstance newObject = new ObjectInstance(Instantiate(prefab) as GameObject);
                    poolDictionary[poolKey].Enqueue(newObject);
                    newObject.SetParent(poolHolder.transform);
                }
            }
            else
            {
                Transform holderRef = null;

                foreach (Transform item in poolHolders)
                {
                    if (item.name == poolHolderName) holderRef = item;
                }

                for (int i = 0; i < poolSize; i++)
                {
                    ObjectInstance newObject = new ObjectInstance(Instantiate(prefab) as GameObject);
                    poolDictionary[poolKey].Enqueue(newObject);
                    newObject.SetParent(holderRef);
                }
            }
        }*/
