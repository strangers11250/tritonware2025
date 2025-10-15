using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro; // For TextMeshPro

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameTest;
    public GameObject choicePanel; // Assign your choice panel here
    public Button choice1Button;
    public Button choice2Button;

    private Queue<string> sentences;
    private List<string> currentChoices;

    void Start()
    {
        sentences = new Queue<string>();
        currentChoices = new List<string>();
        choicePanel.SetActive(false); // Hides choices initially
    }

    public void StartDialogue(DialogueManager dialogue)
    {
        //nameText.text = dialogue.nameTest;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f); // Typing speed
        }    
    }

    public void ShowChoices(List<string> choices)
    {
        choicePanel.SetActive(true);
        currentChoices = choices; // Store choices for button assignment

        // Assign choices to buttons (example for two choices)
        choice1Button.GetComponentInChildren<TextMeshProUGUI>().text = choices[0];
        choice2Button.GetComponentInChildren<TextMeshProUGUI>().text = choices[1];

        choice1Button.onClick.RemoveAllListeners();
        choice2Button.onClick.RemoveAllListeners();

        choice1Button.onClick.AddListener(() => OnChoiceSelected(0));
        choice2Button.onClick.AddListener(() => OnChoiceSelected(1));
    }

    public void OnChoiceSelected(int choiceIndex)
    {
        choicePanel.SetActive(false);
        // Implement logic based on choiceIndex (e.g., load a different dialogue, change scene)
        Debug.Log("Choice " + (choiceIndex + 1) + " selected: " + currentChoices[choiceIndex]);
    }

    void EndDialogue()
    {
        Debug.Log("End of Dialogue.");
        // Implement actions after dialogue ends (e.g., transition to gameplay, load next scene)
    }
}

// Simple Dialogue class to hold dialogue data
[System.Serializable]
public class Dialogue
{
    public string name;
    [TextArea(3, 10)]
    public string[] sentences;
}
