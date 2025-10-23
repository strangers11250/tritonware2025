using UnityEngine;

public class AnyaInteract : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AnyaChase isStartingCombat;
    public Animator animator;
    public GameObject player;
    public float interactDistance = 3f;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if (isStartingCombat == null)
        {
            isStartingCombat = GetComponent<AnyaChase>();
        }
        isStartingCombat.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(player.transform.position, this.transform.position) < interactDistance)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isStartingCombat.enabled = true;
                animator.SetBool("startCombat", true);
            }
        }
    }
}
