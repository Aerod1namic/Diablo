using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AgentMotor))]
public abstract class chract : Interacted
{
    public override void Interact(GameObject subject)
        {
         
        StartCoroutine(OnInteract(subject));
        }

        [SerializeField] protected float maxHealth = 100f;
        [SerializeField] protected float currentHealth;
        [SerializeField] private float damage = 5f;
        [SerializeField] private float attackCD = 1f;
        [SerializeField] private bool canAttack = true;
    [SerializeField] protected GameObject DieEffect;
    [SerializeField] protected ParticleSystem Hit_effect1;
    [SerializeField] protected ParticleSystem Hit_effect2;
    [SerializeField] protected ParticleSystem Hit_effect3;
    protected AgentMotor motor;
    protected Quest quest;

        private void Start()
        {
            motor = GetComponent<AgentMotor>();
            currentHealth = maxHealth;
            quest = GetComponent<Quest>();
        }

        private IEnumerator OnInteract(GameObject subject)
        {
            var character = subject.GetComponent<chract>();
            if (character != null)
                if (character.canAttack)
                {
                    while (isFocus && subject != null)
                    {
                        if (Vector3.Distance(transform.position, subject.transform.position) <= InteractRadius)
                        {
                            character.Attack();
                            yield return new WaitForSeconds(0.7f);
                            TakeDamage(character.damage);
                            Hit_effect1.Play();
                            Hit_effect2.Play();
                            Hit_effect3.Play();
                        yield return new WaitForSeconds(character.attackCD);
                    }
                        yield return null;
                    }


                }
        }

        private void TakeDamage(float damage)
        {
            currentHealth -= damage;
            print($" Здоровье персонажа {gameObject.name}: {currentHealth} ");
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        public void Attack()
        {
            motor.StartAttack(attackCD);
        }



        protected abstract void Die();
    
}
