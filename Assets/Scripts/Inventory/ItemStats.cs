using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStats : MonoBehaviour {

    [HideInInspector]
    int itemID;

    public void SetState(int stateID)
    {
        /* 0 = NORMAL
         * 1 = SMELLY
         * 2 = GOLDEN
         * 3 = WET
         * 4 = ROTTEN
         * 5 = POISONED
         */

        switch (stateID)
        {
            case 0:
                SetStateToNormal();
                break;
            case 1:
                SetStateToSmelly();
                break;
            case 2:
                SetStateToGolden();
                break;
            case 3:
                SetStateToWet();
                break;
            case 4:
                SetStateToRotten();
                break;
            case 5:
                SetStateToPoisoned();
                break;
            default:
                break;
        }
    }

    void SetStateToNormal()
    {

    }

    void SetStateToSmelly()
    {

    }

    void SetStateToGolden()
    {

    }

    void SetStateToWet()
    {

    }

    void SetStateToRotten()
    {

    }

    void SetStateToPoisoned()
    {

    }



    // LOCATION STUFF HERE
    public void SetLocation(int locID)
    {
        /* 0 = FREE
         * 1 = IN STORE
         * 2 = INVENTORY
         */

        switch (locID)
        {
            case 0:
                SetLocationToFree();
                break;
            case 1:
                SetLocationToStore();
                break;
            case 2:
                SetLocationToInventory();
                break;
            default:
                break;
        }
    }

    void SetLocationToFree()
    {

    }

    void SetLocationToStore()
    {

    }

    void SetLocationToInventory()
    {

    }
}
