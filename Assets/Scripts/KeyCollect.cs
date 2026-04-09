using UnityEngine;
using UnityEngine.Events;

public class KeyCollect : MonoBehaviour
{
    public QuestEvent gotKey;
    public Quest quest;
    public UnityEvent hasKey;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<SphereCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            gotKey.Invoke(quest);
            hasKey.Invoke();
            Debug.Log("You Got the Key!");
        }
    }
}

