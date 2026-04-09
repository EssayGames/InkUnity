using UnityEngine;
using UnityEngine.Events;

public class NPC_Talk : MonoBehaviour
{
    public UnityEvent startTalk;
    public bool canTalk;
    public GameObject talkInstructions;

    //Base functionality using an event to trigger the BasicInkExample StartStory() method

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canTalk = true;
            talkInstructions.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")){
            canTalk = false;
            talkInstructions.SetActive(false);
        }
    }

    public void Update()
    {
        if (canTalk)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                startTalk.Invoke();
                talkInstructions.SetActive(false);
                canTalk = false;
            }
        }
    }


}
