using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitcherNPC : NPC
{
    private CapsuleCollider capsuleCollider;
    private Outline outline;

    private void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>(); 
        outline = GetComponent<Outline>();
        CheckingQuest(NPCtype.Witcher);
    }

    public void CheckQuest()
    {
        base.CheckingQuest(NPCtype.Witcher);
    }
    protected override void ShowQuest()
    {
        capsuleCollider.enabled = true;
        outline.enabled = true;
    }

    protected override void HideQuest()
    {
        capsuleCollider.enabled = false;
        outline.enabled = false;
    }
}
