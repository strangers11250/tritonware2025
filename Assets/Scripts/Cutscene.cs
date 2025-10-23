using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cutscene : MonoBehaviour
{
    public GameObject fadeScreenIn;
    public GameObject charRen;
    public GameObject textBox;

    void Start()
    {
        StartCoroutine(EventStarter());
    }

    IEnumerator EventStarter()
    {
        yield return new WaitForSeconds(2);
        fadeScreenIn.SetActive(false);
        charRen.SetActive(true);
        yield return new WaitForSeconds(2);
        // this is where our text function will go in future tutorial
        textBox.SetActive(true);
        yield return new WaitForSeconds(2);
        charRen.SetActive(true);
    }
}