using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public GameObject questNotify;
    public TextMeshProUGUI questText;

    //Called by BasicInkExample when the player selects a choice with "quest_start"
    public void addQuest(Quest quest)
    {
        
    }

    //This can be called by any #advance tag in the BasicInkExample
    //or by an item with a Quest through a QuestEvent
    public void advanceQuest(Quest quest)
    {
        
    }

    //Called by BasicInkExample when the final choice is made (or by a choice with the #reward tag)
    public void completeQuest(Quest quest)
    {
       
    }

}
