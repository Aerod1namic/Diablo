using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(AgentAnimator))]

public class AgentMotor : MonoBehaviour
{
    private NavMeshAgent agent;
    private AgentAnimator animator;
    private Transform target;
    private bool isAttaking = false;
    private SoundsEffector soundEffector;
    private bool isDead = false;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<AgentAnimator>();
        soundEffector = GetComponent<SoundsEffector>();
    }


    private void Update()
    {
        if (target != null)
        {
            LookToObject();
            agent.SetDestination(target.position); 
        }
        if (!isAttaking && !isDead)
        {
            if (agent.velocity.magnitude == 0)
                animator.SetAnim(AgentAnimator.AnimState.idle);
            else
                animator.SetAnim(AgentAnimator.AnimState.run);
        }
    }
    public void StartAttack(float AttackCD)
    {
        if (!isDead)
        StartCoroutine(Attack(AttackCD));
    }

    private IEnumerator Attack(float AttackCD)
    {
        isAttaking = true;
        switch (Random.Range(3, 6))
        {
            case 3:
                animator.SetAnim(AgentAnimator.AnimState.attack);
                break;
            case 4:
                animator.SetAnim(AgentAnimator.AnimState.attack_2);
                break;
            case 5:
                animator.SetAnim(AgentAnimator.AnimState.attack_3);
                break;
            case 6:
                animator.SetAnim(AgentAnimator.AnimState.attack_4);
                break;
            default:
                print("Incorrect attack number");
                break;
        }
        StartCoroutine(soundattackCoolDown());
        yield return new WaitForSeconds(AttackCD - 0.05f);
        isAttaking = false;
    }
    private IEnumerator soundattackCoolDown()
    {
        yield return new WaitForSeconds(0.6f);
        soundEffector.PlayAttackSound();
    }

    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    public void FollowToObject(Interacted New_target)
    {
        if (!isDead)
        {
            agent.stoppingDistance = New_target.InteractRadius;
            target = New_target.transform;
            agent.updateRotation = false;
        }
    }

    public void StopFollowing()
    {
        agent.stoppingDistance = 0f;
        target = null;
        agent.updateRotation = true;
    }


    public void LookToObject()
    {
        if (!isDead)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotate = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotate, Time.deltaTime * 3f);
        }
    }

    public void Die()
    {
        StartCoroutine(DeadTime());
    }

    private IEnumerator DeadTime()
    {
        agent.enabled = false;
        print($"{gameObject.name} Умер!");
        isDead = true;
        animator.SetAnim(AgentAnimator.AnimState.dead);
        soundEffector.PlayDeathSound();
        yield return new WaitForSeconds(2f);
    }
}
