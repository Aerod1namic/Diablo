using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private AudioSource soundsVolume, musicVolume;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Button buttonContinue;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("soundVolume"))
            PlayerPrefs.SetFloat("soundVolume", 7f);
        if (!PlayerPrefs.HasKey("musicSound"))
            PlayerPrefs.SetFloat("musicSound", 7f);

        soundSlider.value = PlayerPrefs.GetFloat("soundVolume");
        CheckingButtom();
       
    }
    private void Update()
    {
        PlayerPrefs.SetFloat("soundVolume", soundSlider.value);
        PlayerPrefs.SetFloat("musicSound", soundSlider.value);
        soundsVolume.volume= PlayerPrefs.GetFloat("soundVolume");
        musicVolume.volume = PlayerPrefs.GetFloat("musicSound");
    }
    private void CheckingButtom()
    {
        if (!PlayerPrefs.HasKey("BeginGame"))
        {
            print("Сохранение не найдено");
            buttonContinue.interactable = false;
        }
        else if (PlayerPrefs.HasKey("BeginGame"))
        {
            print("Сохранение найдено");
            buttonContinue.interactable = true;
        }
    }
    public void DeleteSaves()
    {
        PlayerPrefs.DeleteAll();
        CheckingButtom ();
    }
    public void NewGame()
    {
        PlayerPrefs.SetInt("BeginGame", 0);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
