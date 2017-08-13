using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStats : MonoBehaviour
{

    public string id;
    public string itemName;
    public float xOffset;
    public float yOffset;
    public float rotation;
    public float scale;
    public int rarity;
    public enum Type { Healing, Damaging, Weapon };
    public Type itemType;
    public float effectValue;
    [Range(0.1f, 3000f)]
    public float value;
    [Range(0.1f, 50f)]
    public float weight;
    [Range(0, 15)]
    public int quantity;
    public string itemLocation;
    [Range(1, 15)]
    public int minQuantity;
    [Range(1, 15)]
    public int maxQuantity;
    public GameObject quantUI;

    [HideInInspector]
    SpringJoint2D spring;
    Rigidbody2D rig;

    private void Awake()
    {
        spring = GetComponent<SpringJoint2D>();
        rig = GetComponent<Rigidbody2D>();
    }

    public void SetStats(Item newStats)
    {
        xOffset = newStats.xOffset;
        yOffset = newStats.yOffset;
        rotation = newStats.rotation;
        quantity = newStats.quantity;
    }

    public Item GetStats()
    {
        Item newItem = new Item();
        newItem.id = id;
        newItem.xOffset = xOffset;
        newItem.yOffset = yOffset;
        newItem.rotation = rotation;
        newItem.quantity = quantity;
        newItem.itemLocation = itemLocation;
        return newItem;
    }

    public void UpdateQuantity(int newQuantity)
    {
        quantity += newQuantity;

        if (quantity == 1)
        {
            quantUI.SetActive(false);
            quantUI = null;
        }
        else if (quantity <= 0)
        {
            if (quantUI != null)
            {
                quantUI.SetActive(false);
                quantUI = null;
            }

            gameObject.SetActive(false);
        }
        else
        {
            quantUI.GetComponent<QuantUI>().SetQuantityText(gameObject);
        }

    }

    public void BuyItem(Vector2 spawnPosition)
    {
        StartCoroutine(BuyItemCoroutine(spawnPosition));
    }

    public IEnumerator BuyItemCoroutine(Vector2 spawnPosition)
    {
        spring.connectedAnchor = spawnPosition;
        spring.enabled = true;
        rig.isKinematic = false;
        rig.simulated = true;
        rig.gravityScale = 0f;
        rig.drag = 3f;

        while(Vector2.Distance(transform.position, spawnPosition) > 0.1f)
        {
            Debug.Log(Vector2.Distance(transform.position, spawnPosition));
            yield return null;
        }
        spring.enabled = false;
        rig.drag = 10f;
        gameObject.layer = LayerMask.NameToLayer("PlayerItem");

    }


}
