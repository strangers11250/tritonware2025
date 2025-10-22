using UnityEngine;

public class CameraFollowXBounds : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public Vector2 xBounds = new Vector2 (0, 0);
    public Vector2 yBounds = new Vector2 (0, 0);
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
            Vector3 movement = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = new Vector3(
                Mathf.Clamp(movement.x, xBounds.x, xBounds.y),
                Mathf.Clamp(movement.y, yBounds.x, yBounds.y),
                transform.position.z
            );
        }
    }
}
