using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private GameObject LoadingScreen;
    [SerializeField] private Slider slider;

    public void LoadLevel(int IndexScene)
    {
        StartCoroutine(LoadAsynScene(IndexScene));
    }

    private IEnumerator LoadAsynScene(int IndexScene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(IndexScene);
        LoadingScreen.SetActive(true);
        while (operation.isDone == false)
        {
            float progress = operation.progress;
            slider.value = progress;
            yield return null;
        }
    }
}
