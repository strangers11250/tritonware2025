using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float smoothSpeed = .125f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player != null)
        {
            offset = transform.position - player.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector3 desiredPosition = player.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }
    }
}
