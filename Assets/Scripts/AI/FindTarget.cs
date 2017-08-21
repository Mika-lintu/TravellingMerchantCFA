using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/FindTarget")]
public class FindTarget : Decision {

    public override bool Decide(StateController controller)
    {
        if (controller.FindTarget())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
