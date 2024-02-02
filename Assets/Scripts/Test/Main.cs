using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    [SerializeField] private GameObject PauseScreen;
    [SerializeField] private AudioSource soundSource, musicSource;
    [SerializeField] private Slider soundSlider;
    private SoundsEffector soundEffector;

    private void Start()
    {
        soundSource.volume = PlayerPrefs.GetFloat("soundVolume");
        musicSource.volume = PlayerPrefs.GetFloat("musicSound");
        soundSlider.value = PlayerPrefs.GetFloat("soundVolume");
        soundEffector = GetComponent<SoundsEffector>();
    }

    private void Update()
    {
        PlayerPrefs.SetFloat("soundVolume", soundSlider.value);
        PlayerPrefs.SetFloat("musicSound", soundSlider.value);
        soundSource.volume = PlayerPrefs.GetFloat("soundVolume");
        musicSource.volume = PlayerPrefs.GetFloat("musicSound");

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseOn();
            soundEffector.PlayInventorySound();
        }
            
    }

    public void PauseOn()
    {
        Time.timeScale = 0f;
        Player.instance.enabled = false;
        PauseScreen.SetActive(true);
    }

    public void PauseOff()
    {
        Time.timeScale = 1f;
        Player.instance.enabled = true;
        PauseScreen.SetActive(false);
    }

    public void AddPlayerHealth()
    {
        Player.instance.Healing();
    }

    public void FirstQuest()
    {
        PlayerPrefs.SetInt("NumberQuest", 0);
    }
    public void SecondQuest()
    {
        PlayerPrefs.SetInt("NumberQuest", 1);
    }
    public void ThirdQuest()
    {
        PlayerPrefs.SetInt("NumberQuest", 2);
    }
    public void ForthQuest()
    {
        PlayerPrefs.SetInt("NumberQuest", 3);
    }
    public void FithQuest()
    {
        PlayerPrefs.SetInt("NumberQuest", 4);
    }
    public void SixQuest()
    {
        PlayerPrefs.SetInt("NumberQuest", 5);
    }
}
