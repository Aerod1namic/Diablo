using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class AgentAnimator : MonoBehaviour
{
    private Animator animator;

    public enum AnimState { idle, run, dead, attack, attack_2, attack_3, attack_4 }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetAnim(AnimState animstate)
    {
        animator.SetInteger("StateAnim", (int)animstate);
    }
}
