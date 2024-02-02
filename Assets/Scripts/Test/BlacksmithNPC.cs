using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlacksmithNPC : NPC
{
    private CapsuleCollider capsuleCollider;
    private Outline outline;

    private void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        outline = GetComponent<Outline>();
        CheckingQuest(NPCtype.Blacksmith);
    }
    public void CheckQuest()
    {
        base.CheckingQuest(NPCtype.Blacksmith);
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
