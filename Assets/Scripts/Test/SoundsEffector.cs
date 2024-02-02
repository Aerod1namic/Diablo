using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsEffector : MonoBehaviour
{
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private AudioClip attacksound, deathsound, inventory;

    public void PlayAttackSound()
    {
        AudioSource.PlayOneShot(attacksound);
    }

    public void PlayDeathSound()
    {
        AudioSource.PlayOneShot(deathsound);
    }

    public void PlayInventorySound()
    {
        AudioSource.PlayOneShot(inventory);
    }
}
