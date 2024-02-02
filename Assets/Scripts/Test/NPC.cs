using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public virtual void CheckingQuest(NPCtype type)
    {
        var saveDataQuest = (QuestNumber)PlayerPrefs.GetInt("NumberQuest");
        switch (saveDataQuest)
        {
            case QuestNumber.FirstQuest:
                if (type == NPCtype.Witcher)
                    ShowQuest();
                else
                    HideQuest();
                break;
            case QuestNumber.SecondQuest:
                if (type == NPCtype.Blacksmith)
                    ShowQuest();
                else
                    HideQuest();
                break;
            case QuestNumber.ThirdQuest:
                if (type == NPCtype.Witcher)
                    ShowQuest();
                else
                    HideQuest();
                break;
            case QuestNumber.FourthQuest:
                HideQuest();
                break;
            case QuestNumber.FifthQuest:
            case QuestNumber.SixthQuest:
                if (type == NPCtype.Witcher)
                    ShowQuest();
                else
                    HideQuest();
                break;
            default:
                Debug.Log("Incorrect Number (NPC)");
                    break;
        }
    }
    
    protected virtual void ShowQuest()
    {
        Debug.Log("ShowQuest");
    }

    protected virtual void HideQuest()
    {
        Debug.Log("Hide");
    } 
}

public enum NPCtype
{
    Witcher,Blacksmith
}

public enum QuestNumber
{
    FirstQuest = 0,
    SecondQuest = 1,
    ThirdQuest = 2,
    FourthQuest = 3,
    FifthQuest = 4,
    SixthQuest = 5,

}
