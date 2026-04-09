using System;
using Ink.Runtime;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Events;

// This is a super bare bones example of how to play and display a ink story in Unity.
public class BasicInkExample : MonoBehaviour {
    public static event Action<Story> OnCreateStory;
	
    void Awake () {
		// Remove the default message
		RemoveChildren();
		//StartStory();
	}

	// Creates a new Story object with the compiled story which we can then play!
	public void StartStory () {

		playerInput.cursorLocked = false;
		playerInput.cursorInputForLook = false;
		if (canvas.activeSelf == false)
		{
			canvas.SetActive(true);
		}

		story = new Story (inkJSONAsset.text);
		if(OnCreateStory != null) OnCreateStory(story);
		story.variablesState["got_key"] = hasKey;
		story.variablesState["visited"] = visited;
		story.variablesState["taken_quest"] = takenQuest;
		RefreshView();
	}
	
	// This is the main function called every time the story changes. It does a few things:
	// Destroys all the old content and choices.
	// Continues over all the lines of text, then displays all the choices. If there are no choices, the story is finished!
	void RefreshView () {
		// Remove all the UI on screen
		RemoveChildren ();
		
		// Read all the content until we can't continue any more
		while (story.canContinue) {
			// Continue gets the next line of the story
			string text = story.Continue ();
			// This removes any white space from the text.
			text = text.Trim();
			// Display the text on screen!
			CreateContentView(text);

			visited = (bool) story.variablesState["visited"];
			takenQuest = (bool)story.variablesState["taken_quest"];

		}

		// Display all the choices, if there are any!
		if(story.currentChoices.Count > 0) {
			for (int i = 0; i < story.currentChoices.Count; i++) {
				Choice choice = story.currentChoices [i];
				Button button = CreateChoiceView (choice.text.Trim ());
				
				// Tell the button what to do when we press it
                button.onClick.AddListener (delegate {
					OnClickChoiceButton (choice);
				});
			}
		}
		// If we've read all the content and there's no choices, the story is finished!
		else {
			closeStory();
		}
	}

	// When we click the choice button, tell the story to choose that choice!
	void OnClickChoiceButton (Choice choice) {
		if (choice.tags != null) { 
			for (int i = 0; i < choice.tags.Count; i++)
			{
				if (choice.tags[i] == "quest_start")
				{
					questEvent.Invoke(quest);
				}
				if (choice.tags[i] == "reward")
				{
					questComplete.Invoke(quest);
				}
			}
        }
        story.ChooseChoiceIndex (choice.index);
		RefreshView();
	}

	// Creates a textbox showing the the line of text
	void CreateContentView (string text) {
		TextMeshProUGUI storyText = Instantiate (textPrefab) as TextMeshProUGUI;
		storyText.text = text;
		storyText.transform.SetParent (canvas.transform, false);
	}

	// Creates a button showing the choice text
	Button CreateChoiceView (string text) {
		// Creates the button from a prefab
		Button choice = Instantiate (buttonPrefab) as Button;
		choice.transform.SetParent (canvas.transform, false);
		
		// Gets the text from the button prefab
		Text choiceText = choice.GetComponentInChildren<Text> ();
		choiceText.text = text;

		// Make the button expand to fit the text
		HorizontalLayoutGroup layoutGroup = choice.GetComponent <HorizontalLayoutGroup> ();
		layoutGroup.childForceExpandHeight = false;

		return choice;
	}

	// Destroys all the children of this gameobject (all the UI)
	void RemoveChildren () {
		int childCount = canvas.transform.childCount;
		for (int i = childCount - 1; i >= 0; --i) {
			Destroy (canvas.transform.GetChild (i).gameObject);
		}
	}

	void closeStory()
	{
		canvas.SetActive (false);
        playerInput.cursorLocked = true;
        playerInput.cursorInputForLook = true;
		Cursor.lockState = CursorLockMode.Locked;
	}

	public void gotKey()
	{
		hasKey = true;
	}

	[SerializeField]
	private TextAsset inkJSONAsset = null;
	public Story story;
    public StarterAssetsInputs playerInput;
	public Quest quest;
	public QuestEvent questEvent;
	public QuestEvent questComplete;
	public bool hasKey;
	public bool visited;
	public bool takenQuest;

	[SerializeField]
	private GameObject canvas = null;

	// UI Prefabs
	[SerializeField]
	private TextMeshProUGUI textPrefab = null;
	[SerializeField]
	private Button buttonPrefab = null;


	
}

[Serializable]
public class QuestEvent : UnityEvent<Quest>
{

}
