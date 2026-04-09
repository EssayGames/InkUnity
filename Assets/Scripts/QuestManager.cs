using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> currentQuests = new List<Quest>();
    public GameObject questNotify;
    public TextMeshProUGUI questText;

    public void addQuest(Quest quest)
    {
        currentQuests.Add(quest);
        quest.currentPhase = 0;
        StartCoroutine(ShowQuest(quest.questPhases[quest.currentPhase]));
    }

    public void advanceQuest(Quest quest)
    {
        for(int i = 0; i < currentQuests.Count; i++)
        {
            if (currentQuests[i] == quest)
            {
                currentQuests[i].currentPhase++;
                StartCoroutine(ShowQuest(currentQuests[i].questPhases[currentQuests[i].currentPhase]));

            }
            ;
        }
    }

    public void completeQuest(Quest quest)
    {
        for(int i = 0; i< currentQuests.Count; i++)
        {
            if (currentQuests[i] == quest)
            {
                currentQuests[i].currentPhase = currentQuests[i].questPhases.Count - 1;
                StartCoroutine(ShowQuest(currentQuests[i].questPhases[currentQuests[i].currentPhase]));
            }
        }
    }

    private IEnumerator ShowQuest(string text)
    {
        questText.text = text;
        questNotify.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        questNotify.SetActive(false);
        
    }
}
