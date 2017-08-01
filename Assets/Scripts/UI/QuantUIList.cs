using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantUIList : MonoBehaviour {

    public List<GameObject> quantText;

    void SetQuant()
    {

        for (int i = 0; i < quantText.Count; i++)
        {
            if (!quantText[i].activeInHierarchy)
            {
                quantText[i].SetActive(true);
                //quantText[i].GetComponent<QuantUI>().SetQuantityText(???ITEM???);
            }
        }
    }

}
