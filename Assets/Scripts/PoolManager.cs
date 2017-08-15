using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{


    Dictionary<string, Queue<ItemObjectInstance>> itemDictionary = new Dictionary<string, Queue<ItemObjectInstance>>();
    Dictionary<int, Queue<ObjectInstance>> poolDictionary = new Dictionary<int, Queue<ObjectInstance>>();
    QuantUIList itemUIList;
    GameObject backpack;
    ItemHandler itemHandler;
    static PoolManager _instance;
    public GameObject[] itemPrefabs;
    public GameObject inventory;
    public GameObject sceneItems;


    private void Awake()
    {
        itemUIList = GameObject.FindGameObjectWithTag("UIItemQuantity").GetComponent<QuantUIList>();
        backpack = GameObject.FindGameObjectWithTag("Backpack").transform.GetChild(0).gameObject;
        itemHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemHandler>();
    }

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
            //poolHolder.transform.parent = transform;
        }

        if (!itemDictionary.ContainsKey(poolKey)) itemDictionary.Add(poolKey, new Queue<ItemObjectInstance>());

        for (int i = 0; i < poolSize; i++)
        {
            ItemObjectInstance newObject = new ItemObjectInstance(Instantiate(prefab) as GameObject, location);
            itemDictionary[poolKey].Enqueue(newObject);
            newObject.SetParent(poolHolder.transform);
        }

        #region
        /*
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
        */
        #endregion
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

    public GameObject ReuseItem(string id, Vector3 position, Quaternion rotation, GameObject parent)
    {
        string poolKey = id;

        if (itemDictionary.ContainsKey(poolKey))
        {
            ItemObjectInstance objectToReuse = itemDictionary[poolKey].Dequeue();
            itemDictionary[poolKey].Enqueue(objectToReuse);
            objectToReuse.SetParent(parent.transform);
            objectToReuse.Reuse(position, rotation);
            return objectToReuse.gameObject;
        }
        return null;
    }


    public void PoolItemsToInventory(GameObject go, int quantity, Vector2 position, GameObject parent)
    {
        Vector2 oldPosition = (Vector2)go.transform.position;
        string poolKey = go.GetComponent<ItemStats>().id;

        for (int i = 0; i < quantity; i++)
        {
            if (itemDictionary.ContainsKey(poolKey))
            {
                ItemObjectInstance objectToReuse = itemDictionary[poolKey].Dequeue();
                itemDictionary[poolKey].Enqueue(objectToReuse);
                objectToReuse.Reuse(position, Quaternion.identity);

                if (i == 0)
                {
                    ItemStats stats = objectToReuse.gameObject.GetComponent<ItemStats>();
                    
                    stats.quantity = quantity;
                    stats.itemLocation = "inventory";
                    objectToReuse.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                    objectToReuse.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
                    objectToReuse.SetParent(backpack.transform);
                    objectToReuse.gameObject.transform.position = new Vector3(oldPosition.x, oldPosition.y, -1f);
                    stats.BuyItem(position);
                    itemHandler.AddItem(objectToReuse.gameObject);
                    objectToReuse.gameObject.tag = "Item";
                }
                else
                {
                    objectToReuse.gameObject.SetActive(false);
                    objectToReuse.SetParent(parent.transform);
                    objectToReuse.gameObject.tag = "Item";
                }
                

            }
        }
        itemUIList.MakeItemList();
    }


    public void RemoveItemsFromInventory(GameObject go, int quantity)
    {
        string poolKey = go.GetComponent<ItemStats>().id;

        for (int i = 0; i < quantity; i++)
        {
            ItemObjectInstance objectToRemove = itemDictionary[poolKey].Dequeue();
            itemDictionary[poolKey].Enqueue(objectToRemove);
            objectToRemove.SetParent(sceneItems.transform);
        }
        itemUIList.MakeItemList();
    }

    

    public void ReuseProp(GameObject prefab, Vector3 position, Quaternion rotation, GameObject parent)
    {
        int poolKey = prefab.GetInstanceID();

        if (poolDictionary.ContainsKey(poolKey))
        {
            ObjectInstance objectToReuse = poolDictionary[poolKey].Dequeue();
            poolDictionary[poolKey].Dequeue();
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


            if (parent.name == "Player" || parent.name == "Backpack")
            {
                gameObject.layer = LayerMask.NameToLayer("PlayerItem");
                Vector3 newPosition = transform.position;
                newPosition.z = -1f;
                transform.position = newPosition;
                gameObject.tag = "Item";
            }
            else if (parent.name == "SceneItems")
            {
                gameObject.layer = LayerMask.NameToLayer("ShelfItem");
                Vector3 newPosition = transform.position;
                newPosition.z = -1f;
                transform.position = newPosition;
                gameObject.tag = "ShopItem";
            }
        }
    }
}
