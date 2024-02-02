using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witcher : Interacted
{
    public override void Interact(GameObject subject)
    {
        Player.instance.DialogOnPlayer();
    }
}
