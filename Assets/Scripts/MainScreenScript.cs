using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreenScript : MonoBehaviour
{

    public int selectScene;
    ItemHandler itemHandler;
    PlayerController playerController;

    private void Awake()
    {
        itemHandler = GetComponent<ItemHandler>();
        playerController = GetComponent<PlayerController>();
    }


    public void LoadOnClick()
    {
        playerController.UpdatePlayerStats(6);
        SceneManager.LoadScene(selectScene);
    }

}
