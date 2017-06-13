using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour {

    public List<GameObject> items;

    void Awake()
    {
        foreach (Transform child in transform)
        {
            items.Add(child.gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(SetupItems());
    }

    public void ShowItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].SetActive(true);
        }
    }

    public void HideItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].SetActive(false);
        }
    }

    IEnumerator SetupItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].GetComponent<SpriteRenderer>().enabled = false;
            items[i].SetActive(true);
        }
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < items.Count; i++)
        {
            items[i].GetComponent<SpriteRenderer>().enabled = true;
            items[i].SetActive(false);
        }
    }

    public void AddItem(GameObject go)
    {
        items.Add(go);
    }
}
