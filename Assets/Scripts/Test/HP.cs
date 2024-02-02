using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : Interacted
{
    public override void Interact(GameObject subject)
    {
        Player.instance.Healing();
    }
}
