using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interacted : MonoBehaviour
{
    public float InteractRadius = 2f;
    protected bool isFocus = false;
    protected GameObject subject;
    private bool isInteract = false;

    public abstract void Interact(GameObject subject);

    protected virtual void Update()
    {
        if (isFocus && !isInteract)
        {
            float distance = Vector3.Distance(transform.position, subject.transform.position);
            if (distance <= InteractRadius) 
            {
                isInteract = true;
                Interact(subject);
            }
        }
    }


    public void OnFocus(GameObject newSubject)
    {
        isFocus = true;
        subject = newSubject;
        isInteract = false;
    }

    public void OnDeFocus()
    {
        isFocus = false;
        subject = null;
        isInteract= false;
    }

    public void OnDie()
    {
        OnDeFocus();
    }
}
