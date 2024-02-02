using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    [SerializeField] private List<QuestData> questData;

    public QuestData GetQuestByNumber(int number)
    {
        return questData[number];
    }

    private void Awake()
    {
        instance = this;
    }
}
