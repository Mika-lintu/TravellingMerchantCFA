using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/DisableCharacter")]
public class DisableCharacter : AIAction {

    public override void Act(StateController controller)
    {
        controller.gameObject.SetActive(false);
    }

}
