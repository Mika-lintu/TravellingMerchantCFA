using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/AreThereTargets")]
public class AreThereTargets : Decision {

    public override bool Decide(StateController controller)
    {
        if (controller.GetNumberOfTargets() <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}
