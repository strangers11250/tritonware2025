using UnityEngine;

public class NameTag : MonoBehaviour
{
    public GameObject nameTag;
    // public Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nameTag.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    public void ShowNameTag()
    {
        nameTag.SetActive(true);
    }
}