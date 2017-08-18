using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{

    public int selectScene;
    ItemHandler itemHandler;
    PlayerController playerController;

    void Awake()
    {
        playerController = Camera.main.GetComponent<PlayerController>();
        itemHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemHandler>();
    }


    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
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
            if (hit.collider != null)
            {
                if (hit.collider.tag == "SceneExit")
                {
                    Debug.Log(selectScene);
                    if (itemHandler.tavernMode)
                    {
                        playerController.UpdatePlayerStats(11);
                    }
                    StartCoroutine(SceneChange());
                }

            }
            else
            {

            }
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
