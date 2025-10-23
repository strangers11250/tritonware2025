using UnityEngine;

public class NameTag : MonoBehaviour
{
    public GameObject nameTag;
    public Transform player;
    public Vector3 offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nameTag.SetActive(false);
    }

    public void ShowNameTag()
    {
        nameTag.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (nameTag.activeSelf) {
        transform.position = player.position + offset;
        }
    }
}