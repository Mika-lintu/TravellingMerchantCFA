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


    public void LoadByIndex()
    {
        SceneManager.LoadScene(selectScene);
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
                    if (SceneManager.GetActiveScene().name == "Tavern")
                    {
                        playerController.UpdatePlayerStats(11);
                    }
                    else if (SceneManager.GetActiveScene().name == "StartScreen")
                    {
                        playerController.UpdatePlayerStats(6);
                    }

                    StartCoroutine(SceneChange());
                }
            }
        }
    }


    IEnumerator SceneChange()
    {
        itemHandler.SaveItemsToJSON();
        LoadByIndex();
        yield return null;
    }
}
