using UnityEngine;
using System.Collections;
using System.Net.NetworkInformation;

public class BackgroundTiling : MonoBehaviour
{
    private float spriteWidth;
    private Vector3 startPos;
    public GameObject player;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        startPos = transform.position;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x - 1f;
    }

    void Update()
    {
        // When fully off screen, reposition to the right
        if (transform.position.x < player.transform.position.x - spriteWidth)
        {
            transform.position += new Vector3(spriteWidth * 2, 0, 0);
        }
    }
}