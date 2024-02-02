using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    [SerializeField] private Text FirstTextButton;
    [SerializeField] private Text SecondTextButton;
    [SerializeField] private Text ThirdTextButton;
    [SerializeField] private GameObject DialogScreen;
    [SerializeField] private Text QuestText;
    [SerializeField] private int LoadSceneIndex;
    private LevelLoader levelLoader;
    private int currentQuestIndex;

    private void Start()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
    }

    public void ChooseQuest(int indexQuest)
    {
        currentQuestIndex = indexQuest;
        var currentQuest = QuestManager.instance.GetQuestByNumber(indexQuest);
        FirstTextButton.text = currentQuest.FirstButton;
        SecondTextButton.text = currentQuest.SecondButton;
        ThirdTextButton.text = currentQuest.ActionThirdButton;
        QuestText.text = currentQuest.QuestDescription;
    }

    public void FirstButton()
    {
        QuestText.text = QuestManager.instance.GetQuestByNumber(currentQuestIndex).AnswerFirstButton;
    }

    public void SecondButton()
    {
        QuestText.text = QuestManager.instance.GetQuestByNumber(currentQuestIndex).AnswerSecondButton;
    }

    public void ThirdButtonAction()
    {
        var questIndex = (QuestNumber)PlayerPrefs.GetInt("NumberQuest");
        DialogScreen.SetActive(false);
        if (questIndex == QuestNumber.FifthQuest)
        {
            Player.instance.DeleteDemonHeadFromStatusBar();
            PlayerPrefs.DeleteKey("DemonHead");
            PlayerPrefs.SetInt("Crystal", 1);
            Player.instance.AddArtifact();

        }
        if (questIndex == QuestNumber.SixthQuest)
        {
            levelLoader.LoadLevel(LoadSceneIndex);
            return;
        }
        currentQuestIndex++;
        PlayerPrefs.SetInt("NumberQuest", currentQuestIndex);

    }
}
