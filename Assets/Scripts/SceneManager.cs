using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour {

    public int selectScene;
    ItemHandler itemHandler;

    void Awake()
    {
        itemHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemHandler>();
    }

	public void LoadByIndex(int sceneIndex)
    {
        Application.LoadLevel(sceneIndex);
    }

    void Update()
    {
        LoadOnClick();
    }

    void LoadOnClick()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(hit.collider.tag == "SceneExit")
            {
                Debug.Log(selectScene);
                StartCoroutine(SceneChange());
                //LoadByIndex(selectScene);
            }
        }
        else
        {

        }
    }


	IEnumerator SceneChange()
    {
        Debug.Log("I did happen");
        itemHandler.SaveItemsToJSON();
        LoadByIndex(selectScene);
        yield return null;
    }
}
