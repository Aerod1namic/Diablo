using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New QuestData", menuName = "QuestData", order = 51)]

public class QuestData : ScriptableObject
{
    [SerializeField] private int NumberQuest;
    [TextArea(10 ,10)]
    [SerializeField] private string DescriptionQuest;
    [SerializeField] private string TextOfFirstButton;
    [TextArea(10, 10)]
    [SerializeField] private string AnswerOfFirstButton;
    [SerializeField] private string TextOfSecondButton;
    [TextArea(10, 10)]
    [SerializeField] private string AnswerOfSecondButton;
    [SerializeField] private string TextOfThirdButton_Action;

    public int QuestNumber => NumberQuest;
    public string QuestDescription => DescriptionQuest;
    public string FirstButton => TextOfFirstButton;
    public string AnswerFirstButton => AnswerOfFirstButton;
    public string SecondButton => TextOfSecondButton;
    public string AnswerSecondButton => AnswerOfSecondButton;
    public string ActionThirdButton => TextOfThirdButton_Action;


}
