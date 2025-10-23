using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cutscene : MonoBehaviour
{
    public GameObject fadeScreenIn;
    public GameObject charRen;
    public GameObject textBox;
    [SerializeField] string textToSpeak;
    [SerializeField] int currentTextLength;
    [SerializeField] int textLength;
    [SerializeField] GameObject mainTextObject;
    [SerializeField] GameObject nextButton;
    [SerializeField] int eventPos = 0;
    [SerializeField] GameObject fadeScreenOut;

    void Update()
    {
        textLength = TextCreator.charCount;
    }

    void Start()
    {
        fadeScreenOut.SetActive(false);
        StartCoroutine(EventStarter());
    }

    IEnumerator EventStarter()
    {
        // event 0
        yield return new WaitForSeconds(2);
        
        fadeScreenIn.SetActive(false);
        charRen.SetActive(true);
        
        yield return new WaitForSeconds(2);
        
        // this is where our text function will go in future tutorial
        mainTextObject.SetActive(true);

        textToSpeak = "Oooo lala sample text.";
        textBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => textLength == currentTextLength);
        yield return new WaitForSeconds(0.5f);
        
        nextButton.SetActive(true);
        eventPos = 1;
    }

    IEnumerator EventOne()
    {
        yield return new WaitForSeconds(2);
        nextButton.SetActive(false);
        charRen.SetActive(true);
        textBox.SetActive(true);
        fadeScreenOut.SetActive(true);
        yield return new WaitForSeconds(2);
    }

    public void NextButton()
    {
        if (eventPos == 1)
        {
            StartCoroutine(EventOne());
        }
    }
}