using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScreenLevel : MonoBehaviour
{
    [SerializeField] private GameObject LoadCaveLevel;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
            LoadCaveLevel.SetActive(true);
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
            LoadCaveLevel.SetActive(false);
    }
}
