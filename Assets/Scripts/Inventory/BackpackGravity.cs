using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackGravity : MonoBehaviour {

    public List<GameObject> itemsInBackpack;

    private void Awake()
    {
        itemsInBackpack = new List<GameObject>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<Rigidbody2D>().gravityScale = 0.8f;
        itemsInBackpack.Add(collision.gameObject);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponent<Rigidbody2D>().gravityScale = 0f;
        itemsInBackpack.Remove(collision.gameObject);
    }

    public bool IsObjectInBackpack(GameObject go)
    {

        for (int i = 0; i < itemsInBackpack.Count; i++)
        {
            if (itemsInBackpack[i].Equals(go))
            {
                return true;
            }
        }
        return false;
    }

   
}
