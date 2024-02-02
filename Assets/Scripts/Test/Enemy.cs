using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Enemy : chract
{
    [SerializeField] private Interacted focus;
    [SerializeField] private float agrRadius = 5f;
    private bool die = false;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, agrRadius);

    }

   

    protected override void Update()
    {
        if (!die)
        {
            base.Update();
            float distance = Vector3.Distance(transform.position, Player.instance.transform.position);
            if (distance <= agrRadius)
            {
                if (focus == null)
                    SetFocus(Player.instance.GetComponent<Interacted>());
            }
            else if (focus != null)
            {
                DeleteFocus();
            }
        }
    }


    public void SetFocus(Interacted Newfocus)
    {
        if (Newfocus != focus)
        {
            if (focus != null)
                focus.OnDeFocus();
        }
        focus = Newfocus;
        motor.FollowToObject(Newfocus);
        Newfocus.OnFocus(gameObject);
    }

    public void DeleteFocus()
    {
        if (focus != null)
            focus.OnDeFocus();
        focus = null;
        motor.StopFollowing();
    }

    protected override void Die()
    {
       die = true;
        DieEffect.SetActive(true);
        Player.instance.DeleteFocus();
        GetComponent<Enemy>().enabled = false;
       DeleteFocus();
       motor.Die();
        Player.instance.AddCoins(Random.Range(20, 30));
    }
}
